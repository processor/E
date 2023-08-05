using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace E.Parsing;

using static TokenKind;

public sealed class Tokenizer
{
    private readonly SourceReader reader;
    private readonly Node env;
    private readonly Stack<Mode> modes;

    public Tokenizer(string text)
        : this(new SourceReader(text), new Node()) { }

    public Tokenizer(string text, Node env)
        : this(new SourceReader(text), env) { }

    private Tokenizer(SourceReader reader, Node env)
    {
        this.reader = reader;
        this.env = env;
        this.modes = new Stack<Mode>(4);

        modes.Push(Mode.Default);

        ReadTrivia(); // read the trivia
    }

    private Location loc;

    [SkipLocalsInit]
    public Token Next()
    {
        start:
        if (reader.IsEof) return new Token(EOF, reader.Location);

        switch (modes.Peek())
        {
            case Mode.Apostrophe:
                if (reader.Peek() is '\'')
                {
                    return Read(Character);
                }

                modes.Pop();

                if (reader.Current == '\'')
                {
                    return Read(Apostrophe); // Closing
                }

                break;

            case Mode.Quotes:
                return ReadQuotedString();
        }
       
        loc = reader.Location;

        switch (reader.Current)
        {
            case '@': return Read(At);
            case '↺': return Read(Repeats);
            case '∎': return Read(Tombstone);

            case '~': return Read(Op); // bitwise compliment

            case '|':
                return reader.Peek() switch
                {
                    '|' => Read(Op, 2),            // ||
                    '>' => Read(PipeForward, 2),   // |>
                    _   => Read(Bar)               // |
                };

            case '<':
                char peek = reader.Peek();

                switch (peek)
                {
                    case '<' : return Read(Op, 2); // <<
                    case '=' : return Read(Op, 2); // <= 
                    case '/' :
                        EnterMode(Mode.Tag);

                        return Read(TagCloseStart, 2); // </  
                }

                if (peek is '_' || char.IsLetter(peek)) // tag
                {
                    EnterMode(Mode.Tag);

                    return Read(TagStart);
                }

                return Read(Op); // <
                    
            case '>' when InMode(Mode.Tag): // >
                modes.Pop();

                return Read(TagEnd);

            case '=' when reader.Peek() == '>': // => 
                return Read(LambdaOperator, 2);     

            case '-':
                return (reader.Peek()) switch
                {
                    '>'               => Read(ReturnArrow, 2), // ->
                    >= '0' and <= '9' => ReadNumber(),         // -{number}
                    _                 => Read(Op),             // -
                };
            case '[': return Read(BracketOpen);
            case ']': return Read(BracketClose);

            case '{':
                return Read(BraceOpen);

            case '}':
                if (InMode(Mode.Expression))
                {
                    ExitMode();
                }

                return Read(BraceClose);

            case ':': // ::, :
                return reader.Peek() switch
                {
                    ':' => Read(ColonColon, 2), // ::
                    _   => Read(Colon)          // :
                };

            case ',': return Read(Comma);
            case '$':
                switch (reader.Peek())
                {
                    case '"':
                        EnterMode(Mode.InterpolatedString);

                        return Read(InterpolatedStringOpen, 2);
                    default     : return Read(Dollar);
                }

            case '.': // ., .., ...
                if (char.IsDigit(reader.Peek())) // .{0-9}
                {
                    return Read(DecimalPoint);
                }

                int dots = 0;

                while (reader.Current is '.' && dots < 3)
                {
                    reader.Advance();

                    dots++;
                }

                if (dots is 2 && reader.Current is '<') // ..<
                {
                    reader.Advance();

                    return new Token(HalfOpenRange, loc, "..<", ReadTrivia());
                }

                switch (dots)
                {
                    case 1: return new Token(Dot,        loc, ".",   ReadTrivia());
                    case 2: return new Token(DotDot,     loc, "..",  ReadTrivia());
                    case 3: return new Token(DotDotDot,  loc, "...", ReadTrivia());
                }

                break;
                    
            case '\'':
                EnterMode(Mode.Apostrophe);

                // Return token directly so we don't consume whitespace...

                reader.Advance();

                return new Token(Apostrophe, reader.Location, "'", null);

            case '`':
                return Read(Backtick);

            case '"':
                if (InMode(Mode.InterpolatedString))
                {
                    modes.Pop();
                }
                else
                {
                    EnterMode(Mode.Quotes);
                }

                return Read(Quote);
                    
            case '(': return Read(ParenthesisOpen);
            case ')': return Read(ParenthesisClose);

            // case '#': return Read(Pound);

            case '_': return Read(Underscore);
            case '?': return Read(Question);
            case ';': return Read(Semicolon);

            case >= '0' and <= '9': return ReadNumber(); // 0-9

            // Superscript (non-continuous code points)
            case '⁰' or '¹' or '²' or '³' or '⁴' or '⁵' or '⁶' or '⁷' or '⁸' or '⁹':
                return ReadSuperscript();

            case '/':
                switch (reader.Peek())
                {
                    case '/': // //
                        SkipComment();

                        goto start;
                    case '>': // />
                        if (InMode(Mode.Tag)) modes.Pop();

                        return Read(TagSelfClosed, 2);
                }

                break;

            case '\n' or '\r' or '\t' or ' ': 
                ReadTrivia(); 
                goto start;
        }

        // Operators 
        if (!char.IsLetter(reader.Current))
        {
            if (env.Operators.Maybe(OperatorType.Infix, reader.Current, out var node))
            {
                Location start = reader.Location;

                var sb = new ValueStringBuilder(stackalloc char[4]);

                sb.Append(reader.Consume());

                while (node.Contains(reader.Current) && node.TryGetNode(reader.Current, out node) && !reader.IsEof)
                {
                    sb.Append(reader.Consume());
                }

                return new Token(Op, start, sb.ToString(), ReadTrivia());
            }
        }

        return ReadIdentifierOrKeyword();
    }

    // Operator
    private Token Read(TokenKind kind, int count = 1)
    {
        return new Token(kind, loc, reader.Consume(count), ReadTrivia());
    }

    [SkipLocalsInit]
    public Token ReadIdentifierOrKeyword()
    {
        var sb = new ValueStringBuilder(stackalloc char[32]);

        var start = reader.Location;

        do
        {
            sb.Append(reader.Consume());
        }
        while ((char.IsLetterOrDigit(reader.Current) || reader.Current is '_') && !reader.IsEof);
            
        var text = sb.ToString();

        if (!InMode(Mode.Tag) && Keywords.Map.TryGetValue(text, out TokenKind kind))
        {
            return new Token(kind, start, text, ReadTrivia());
        }

        return new Token(Identifier, start, text, ReadTrivia());
    }

    [SkipLocalsInit]
    public Token ReadQuotedString()
    {
        if (reader.Current is '"')
        {
            ExitMode();

            return Read(Quote);
        }

        var start = reader.Location;
        var sb = new ValueStringBuilder(stackalloc char[32]);

        while (!reader.IsEof && reader.Current != '"')
        {
            sb.Append(reader.Consume());
        }

        return new Token(String, start, sb.ToString());
    }

    // 1
    // 1.1
    // 1.2e-03
    // 1_000
    // 1__000.1__00000___0_0

    [SkipLocalsInit]
    public Token ReadNumber()
    {
        var sb = new ValueStringBuilder(stackalloc char[32]);

        var start = reader.Location;

        ReadDigits(ref sb);

        if (reader.Current is 'e' && s_signOrDigitChars.Contains(reader.Peek()))
        {
            ReadExponent(ref sb);
        }

        if (reader.Current is '.' && char.IsAsciiDigit(reader.Peek()))
        {
            sb.Append(reader.Consume()); // .

            ReadDigits(ref sb);

            if (reader.Current is 'e')
            {
                ReadExponent(ref sb);
            }
        }

        return new Token(Number, start, sb.ToString(), ReadTrivia());
    }

    private void ReadExponent(ref ValueStringBuilder sb)
    {
        sb.Append(reader.Consume()); // read e

        if (reader.Current is '-' or '+')
        {
            sb.Append(reader.Consume());
        }

        ReadDigits(ref sb);
    }

    private static readonly SearchValues<char> s_signOrDigitChars       = SearchValues.Create("0123456789-+");
    private static readonly SearchValues<char> s_digitOrUnderscoreChars = SearchValues.Create("0123456789_");
    private static readonly SearchValues<char> s_superscriptChars       = SearchValues.Create("⁰¹²³⁴⁵⁶⁷⁸⁹");

    private void ReadDigits(ref ValueStringBuilder sb)
    {
        if (reader.Current is '-')
        {
            sb.Append(reader.Consume());
        }

        sb.Append(reader.Consume(s_digitOrUnderscoreChars));
    }

    
    public Token ReadSuperscript()
    {
        var start = reader.Location;

        var superscript = reader.Consume(s_superscriptChars).ToString();

        return new Token(Superscript, start, superscript, ReadTrivia());
    }
    
    #region Whitespace

    [SkipLocalsInit]
    private string? ReadTrivia()
    {
        var sb = new ValueStringBuilder(stackalloc char[8]);

        while (char.IsWhiteSpace(reader.Current))
        {
            sb.Append(reader.Current);

            reader.Next();
        }

        if (reader.Current is '/' && reader.Peek() is '/')
        {
            ReadComment(ref sb);
        }

        if (sb.Length is 0)
        {
            sb.Dispose();

            return null;
        }

        return sb.ToString();
    }

    internal void SkipComment()
    {
        reader.Advance();
        reader.Advance();

        int line = reader.Line;

        while (line == reader.Line && !reader.IsEof)
        {
            reader.Advance();
        }
    }

    internal void ReadComment(ref ValueStringBuilder sb)
    {
        sb.Append(reader.Consume()); // /
        sb.Append(reader.Consume()); // /

        int line = reader.Line;

        while (line == reader.Line && !reader.IsEof)
        {
            sb.Append(reader.Consume());
        }
    }

    #endregion

    // Modes --- 

    public void EnterMode(Mode mode) => modes.Push(mode);

    public void ExitMode() => modes.Pop();

    public bool InMode(Mode mode) => modes.Peek() == mode;

    public enum Mode
    {
        Default            = 1,
        Apostrophe         = 2,
        Quotes             = 3,
        InterpolatedString = 4,
        Expression         = 5,
        Tag                = 6 // <tag
    }
}