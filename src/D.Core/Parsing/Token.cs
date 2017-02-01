using System;

namespace D.Parsing
{
    public struct Token
    {
        public Token(TokenKind kind, Location start, string text = null, string trailing = null)
        {
            Kind = kind;
            Start = start;
            Text = text;
            Trailing = trailing;
        }

        public TokenKind Kind { get; }

        public Location Start { get; }

        // End (calculate)

        public string Text { get; }

        public string Trailing { get; }

        public bool Is(TokenKind kind)
            => Kind == kind;

        #region Casts

        public static implicit operator string(Token token)
            => token.Text;

        #endregion

        public override string ToString()
            => Kind.ToString() + ":" + Text;
    }

    public struct Location : IEquatable<Location>
    {
        public Location(int line, int column, int position)
        {
            Line = line;
            Column = column;
            Position = position;
        }

        public int Line { get; }

        public int Column { get; }

        public int Position { get; }

        public override bool Equals(object obj)
            => Equals((Location)obj);

        public bool Equals(Location other)
        {
            return Position == other.Position
                && Column == other.Column
                && Line == other.Line;
        }

        public static bool operator == (Location left, Location right)
            => left.Equals(right);

        public static bool operator != (Location left, Location right)
            => !left.Equals(right);

        public override string ToString()
            => "(" + Line + "," + Column + "," + Position + ")";

        public override int GetHashCode()
            => Position.GetHashCode();
    }

    public enum TokenKind
    {
        EOF                 = 0,

        Boolean             = 1, // true, false
        
        Null                = 2, // null
        
        Number              = 3, // 1, 1.1, 1.1e+10

        String              = 4, // hi
        True                = 5,
        False               = 6,
        Identifier          = 7,

        DecimalPoint        = 9,

        Op                  = 10,


        At                  = 100, // @
        Apostrophe          = 101, // '
        Backtick            = 102, // `
        Bar                 = 103, // |
        Colon               = 104, // :
        ColonColon          = 105, // ::
        Comma               = 106, // ,
        Dollar              = 107, // $
        Dot                 = 108, // .
        DotDot              = 109, // ..
        DotDotDot           = 110, // ... spread operator
        // Pound            = 111, // #     
        Semicolon           = 112, // ;
        Quote               = 113, // "
        Question            = 114, // ?
        Underscore          = 115, // _
        // Exclamation      = 116, // !
        HalfOpenRange       = 117, // ..<
        // TripleQuote,     // """


        InterpolatedStringOpen  = 120, // $"

        // Tags
        TagOpen                 = 151, // <
        TagClose                = 152, // >

        Character               = 200, // within '' 

        PipeForward,                    // |>

        BracketOpen,                    // [
        BracketClose,                   // ]
        BraceOpen,                      // {
        BraceClose,                     // }
        ParenthesisOpen,                // (
        ParenthesisClose,               // )
        
        Superscript          = 504,     // ⁰¹²³⁴⁵⁶⁷⁸⁹

        LambdaOperator       = 900,     // =>
        ReturnArrow          = 901,     // ->

        // ¬            // bitwise not (logical negation)
        // ≠
        // ≜
        // ≐
        // ≡
        // →

        ForAll,         // ∀ (for all, each, every, any)

        // Keywords
        Async,
        Await,
        Break,
        Case,
        Catch,
        Continue,
        Default,
        Do,
        Else,
        Emit,
        Enum,
        Event,
        For,
        Function,
        If,
        Implementation,
        In,
        Match,
        Mutable,
        Mutating,
        Let,
        Observe,
        On,
        To,
        Type,
        Unit,
        Until,
        Using,
        Var,
        Repeats,   // ↺
        End,       // ∎

        Record,
        Protocal,

        Ascending,
        Descending,

        From,
        Select,
        Where,
        Orderby,
        When,

        Return,

        This,
        Throw,
        Try,
       
        While,
        With,
        Yield
    }
}
