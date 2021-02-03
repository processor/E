using System;

namespace E.Parsing
{
    public readonly struct Token
    {
        public Token(TokenKind kind, Location start, string? text = null, string? trailing = null)
        {
            Kind = kind;
            Start = start;
            Text = text!;
            Trailing = trailing;
        }

        public readonly TokenKind Kind { get; }

        public readonly Location Start { get; }

        // End (calculate)

        public readonly string Text { get; }

        public readonly string? Trailing { get; }

        public readonly bool Is(TokenKind kind) => Kind == kind;

        public readonly override string ToString() => Kind.ToString() + ":" + Text;

        public static implicit operator string(Token token) => token.Text!;

        public readonly bool Equals(string? value)
        {
            return string.Equals(Text, value, StringComparison.Ordinal);
        }
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
        Semicolon           = 112, // ;
        Quote               = 113, // "
        Question            = 114, // ?
        Underscore          = 115, // _
        HalfOpenRange       = 117, // ..<
        // TripleQuote,     // """


        InterpolatedStringOpen  = 120, // $"

        // Tags
        TagStart            = 151, // <
        TagEnd              = 152, // >
        TagCloseStart       = 153, // </
        TagSelfClosed       = 154, // />
        
        Character           = 200, // within '' 

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
        Let, // Constant
        Observe,
        On,
        Operator,
        To,
        Unit,
        Until,
        Using,
        Var,
        Repeats,   // ↺
        Tombstone, // ∎

        Module,

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
        Yield,

        // Visibility
        Public,
        Private,
        Internal,

        // Types
        Class    = 1000,
        Struct   = 1001,
        Actor    = 1002,
        Record   = 1003,
        Role     = 1004,
        Protocol = 1005
    }
}
