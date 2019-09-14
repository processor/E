using System;
using System.Collections.Generic;
using System.Text;

namespace D.Parsing
{
    using static TokenKind;

    public sealed class Tokenizer : IDisposable
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
            this.modes = new Stack<Mode>();

            modes.Push(Mode.Default);

            ReadTrivia(); // read the trivia

            this.reader.Next(); // start things off
        }

        private Location loc;

        public Token Next()
        {
            start:
            if (reader.IsEof) return new Token(EOF, reader.Location);

            switch (modes.Peek())
            {
                case Mode.Apostrophe:
                    if (reader.Peek() == '\'')
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

                    if (peek == '_' || char.IsLetter(peek)) // tag
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
                    switch (reader.Peek())
                    {
                        case '>': return Read(ReturnArrow, 2);  // ->

                        case '0': case '1': case '2': case '3': case '4':
                        case '5': case '6': case '7': case '8': case '9':
                            return ReadNumber(); // -{number}

                        default : return Read(Op); // -
                    }

 
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

                    while (reader.Current == '.' && dots < 3)
                    {
                        reader.Consume();

                        dots++;

                        if (reader.IsEof) break;
                    }

                    if (dots == 2 && reader.Current == '<') // ..<
                    {
                        reader.Consume();

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

                    return new Token(Apostrophe, reader.Location, reader.Consume().ToString(), null);

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
        
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return ReadNumber();

                // Superscript
                case '⁰': 
                case '¹': 
                case '²': 
                case '³': 
                case '⁴': 
                case '⁵': 
                case '⁶': 
                case '⁷': 
                case '⁸': 
                case '⁹':
                    return ReadSuperscript();

                case '/' when reader.Peek() == '/': // //
                    ReadComment();

                    goto start;

                case '/' when reader.Peek() == '>': // />
                    if (InMode(Mode.Tag))
                    {
                        modes.Pop();
                    }

                    return Read(TagSelfClosed, 2);

                case '\n':
                case '\r':
                case '\t':
                case ' ': ReadTrivia(); goto start;
            }

            // Operators 
            if (!char.IsLetter(reader.Current))
            {
                if (env.Operators.Maybe(OperatorType.Infix, reader.Current, out var node))
                {
                    Location start = reader.Location;

                    sb.Append(reader.Consume());

                    while (node.Contains(reader.Current) && node.TryGetNode(reader.Current, out node) && !reader.IsEof)
                    {
                        sb.Append(reader.Consume());
                    }

                    return new Token(Op, start, sb.Extract(), ReadTrivia());
                }
            }

            return ReadIdentifierOrKeyword();
        }

        // Operator
        private Token Read(TokenKind kind, int count = 1) => 
            new Token(kind, loc, reader.Consume(count), ReadTrivia());

        public Token ReadIdentifierOrKeyword()
        {
            var start = reader.Location;

            do
            {
                sb.Append(reader.Current);

                reader.Next();
            }
            while ((char.IsLetterOrDigit(reader.Current) || reader.Current == '_') && !reader.IsEof);
            
            var text = sb.Extract();

            if (!InMode(Mode.Tag) && keywords.TryGetValue(text, out TokenKind kind))
            {
                return new Token(kind, start, text, ReadTrivia());
            }

            return new Token(Identifier, start, text, ReadTrivia());
        }

        public static readonly Dictionary<string, TokenKind> keywords = new Dictionary<string, TokenKind> {
            { "ƒ"              , Function },
            { "as"             , Op },
            { "ascending"      , Ascending },
            { "async"          , Async },
            { "false"          , False },
            { "null"           , Null },
            { "catch"          , Catch },
            { "continue"       , Continue },
            { "default"        , Default },
            { "do"             , Do },
            { "else"           , Else },
            { "emit"           , Emit },
            { "enum"           , Enum },
            { "for"            , For },
            { "from"           , From },
            { "function"       , Function },
            { "let"            , Let },
            { "match"          , Match },
            { "module"         , Module },
            { "if"             , If },
            { "impl"           , Implementation },
            { "implementation" , Implementation },
            { "in"             , In },
            { "is"             , Op },
            { "descending"     , Descending },
            { "mutable"        , Mutable },
            { "mutating"       , Mutating },
            { "on"             , On },
            { "observe"        , Observe },
            { "operator"       , Operator },
            { "orderby"        , Orderby },
            { "return"         , Return },
            { "select"         , Select },
            { "this"           , This },
            { "throw"          , Throw },
            { "to"             , To },
            { "true"           , True },
            { "try"            , Try },
            { "until"          , Until },
            { "unit"           , Unit },
            { "using"          , Using },
            { "var"            , Var },
            { "when"           , When },
            { "while"          , While },
            { "with"           , With },
            { "where"          , Where },
            { "yield"          , Yield },             
            { "event"          , Event },
            { "protocol"       , Protocol },
            { "record"         , Record },
            { "public"         , Public },
            { "private"        , Private },
            { "internal"       , Internal },

            // Types
            { "class"          , Class },
            { "struct"         , Struct }
        };

        public Token ReadQuotedString()
        {
            if (reader.Current == '"')
            {
                ExitMode();

                return Read(Quote);
            }

            var start = reader.Location;

            while (!reader.IsEof && reader.Current != '"')
            {
                sb.Append(reader.Consume());
            }

            return new Token(String, start, sb.Extract());
        }

        // 1
        // 1.1
        // 1.2e-03
        // 1_000
        // 1__000.1__00000___0_0
        public Token ReadNumber()
        {
            var start = reader.Location;

            ReadDigits();

            if (reader.Current == 'e' && IsSignOrDigit(reader.Peek()))
            {
                ReadExponent();
            }

            if (reader.Current == '.' && char.IsDigit(reader.Peek()))
            {
                sb.Append(reader.Consume()); // .

                ReadDigits();

                if (reader.Current == 'e')
                {
                    ReadExponent();
                }
            }

            return new Token(Number, start, sb.Extract(), ReadTrivia());
        }

        private void ReadExponent()
        {
            sb.Append(reader.Consume()); // ! e

            if (reader.Current == '-' || reader.Current == '+')
            {
                sb.Append(reader.Consume());
            }

            ReadDigits();
        }

        private static bool IsSignOrDigit(char value)
        {
            return value == '-' || value == '+' || char.IsDigit(value);
        }

        private void ReadDigits()
        {
            if (reader.Current == '-')
            {
                sb.Append(reader.Consume());
            }

            while (!reader.IsEof && (char.IsDigit(reader.Current) || reader.Current == '_'))
            {
                sb.Append(reader.Consume());
            }
        }

        public Token ReadSuperscript()
        {
            var start = reader.Location;

            do
            {
                sb.Append(reader.Current);
            }
            while (!reader.IsEof && IsSuperscript(reader.Next()));

            return new Token(Superscript, start, sb.Extract(), ReadTrivia());
        }

        // TODO: c# 7, use local function
        private bool IsSuperscript(char c)
            => (c == '⁰' || c == '¹' || c == '²'|| c == '³'|| c == '⁴'
             || c == '⁵'|| c == '⁶'|| c == '⁷'|| c == '⁸' || c == '⁹');               

        public void Dispose()
        {
            reader.Dispose();
        }

        #region Whitespace

        private readonly StringBuilder sb = new StringBuilder();

        private string? ReadTrivia()
        {
            while (char.IsWhiteSpace(reader.Current) && !reader.IsEof)
            {
                sb.Append(reader.Current);

                reader.Next();
            }

            if (reader.Current == '/' && reader.Peek() == '/')
            {
                // TODO: Append to trivia

                ReadComment();
            }

            if (sb.Length == 0) return null;

            return sb.Extract();
        }

        public void ReadComment()
        {
            sb.Append(reader.Consume()); // /
            sb.Append(reader.Consume()); // /

            var line = reader.Line;

            while (line == reader.Line && !reader.IsEof)
            {
                sb.Append(reader.Current);

                reader.Next();
            }
        }

        #endregion

        #region Modes

        public void EnterMode(Mode mode) => modes.Push(mode);

        public void ExitMode() => modes.Pop();

        public bool InMode(Mode mode) => modes.Peek() == mode;

        public enum Mode : byte
        {
            Default = 1,
            Apostrophe = 2,
            Quotes = 3,
            InterpolatedString = 4,
            Expression = 5,
            Tag = 6 // <tag
        }

        #endregion
    }
}
