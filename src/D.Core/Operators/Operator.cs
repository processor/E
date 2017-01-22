namespace D
{
    using static Associativity;

    public class Operator : INamedObject
    {
        public Operator(Kind kind, 
            string name, 
            OperatorType type, 
            int precedence = 1000,
            Associativity associativity = Left)
        {
            OpKind = kind;
            Name = name;
            Type = type;
            Precedence = precedence;
        }

        public string Name { get; }

        public Kind OpKind { get; }

        public int Precedence { get; }

        public Associativity Associativity { get; }

        public OperatorType Type { get; }

        public static Operator Prefix(Kind kind, string name, int precedence, Associativity associativity = Left)
           => new Operator(kind, name, OperatorType.Prefix, precedence, associativity);

        public static Operator Infix(Kind kind, string name, int precedence, Associativity associativity = Left)
            => new Operator(kind, name, OperatorType.Infix, precedence, associativity);

        // highest
        public static readonly Operator Not                = Prefix(Kind.LogicalNotExpression,        "!",   precedence: 15, associativity: Right);
        public static readonly Operator Negation           = Prefix(Kind.NegateExpression,            "-",   precedence: 15, associativity: Right);
        public static readonly Operator UnaryPlus          = Prefix(Kind.UnaryPlusExpression,         "+",   precedence: 15, associativity: Right);
        public static readonly Operator BitwiseNot         = Prefix(Kind.BitwiseNotExpression,        "~",   precedence: 15, associativity: Right);

        public static readonly Operator Power              = Infix(Kind.ExponentiationExpression,     "**",  precedence: 14, associativity: Right);  
        public static readonly Operator Multiplication     = Infix(Kind.MultiplyExpression,           "*",   precedence: 14);                        
        public static readonly Operator Division           = Infix(Kind.DivideExpression,             "/",   precedence: 14);                        
        public static readonly Operator Remainder          = Infix(Kind.ModuloExpression,             "%",   precedence: 14);                       

        public static readonly Operator Addition           = Infix(Kind.AddExpression,                "+",   precedence: 13);
        public static readonly Operator Subtraction        = Infix(Kind.SubtractExpression,           "-",   precedence: 13);
        public static readonly Operator LeftShift          = Infix(Kind.LeftShiftExpression,          "<<",  precedence: 12);
        public static readonly Operator SignedRightShift   = Infix(Kind.RightShiftExpression,         ">>",  precedence: 12);
        public static readonly Operator UnsignedRightShift = Infix(Kind.UnsignedRightShiftExpression, ">>>", precedence: 12);

        public static readonly Operator GreaterThan        = Infix(Kind.GreaterThanExpression,        ">",   precedence: 11);
        public static readonly Operator GreaterOrEqual     = Infix(Kind.GreaterThanOrEqualExpression, ">=",  precedence: 11);
        public static readonly Operator LessThan           = Infix(Kind.LessThanExpression,           "<",   precedence: 11);
        public static readonly Operator LessOrEqual        = Infix(Kind.LessThanOrEqualExpression,    "<=",  precedence: 11);     
                                                                                           
        public static readonly Operator Equal              = Infix(Kind.EqualsExpression,             "==",  precedence: 10);
        public static readonly Operator Identical          = Infix(Kind.IdenticalExpression,          "===", precedence: 10); 
        public static readonly Operator NotEqual           = Infix(Kind.NotEqualsExpression,          "!=",  precedence: 10);
        public static readonly Operator StrictNotEqual     = Infix(Kind.StrictNotEqual,               "!==", precedence: 10);

        // Bitwise
        public static readonly Operator BitwiseXor         = Infix(Kind.XorExpression,                 "^",  precedence: 9);


        public static readonly Operator Coalesce           = Infix(Kind.CoalesceExpression,           "??", precedence: 1);
                                                           
        public static readonly Operator LogicalAnd         = Infix(Kind.LogicalAndExpression,         "&&",  precedence: 6);
        public static readonly Operator LogicalOr          = Infix(Kind.LogicalOrExpression,          "||",  precedence: 5);

        public static readonly Operator Assign             = Infix(Kind.AssignmentExpression,         "=",   precedence: 3, associativity: Right);  

        public static readonly Operator Is                 = Infix(Kind.IsExpression,                 "is", precedence: 3, associativity: Right);
        public static readonly Operator As                 = Infix(Kind.AsExpression,                 "as", precedence: 3, associativity: Right);

        public static readonly Operator BitwiseAnd         = Infix(Kind.BitwiseAndExpression,         "&",  precedence: 20); // check precedence

        // public static readonly Operator Negate = Prefix(Kind.NegateExpress);
        // public static readonly Operator Plus   = Prefix(Kind.NegateExpress);

        // lowest

        Kind IObject.Kind => Kind.Operator;

        public static readonly Operator[] DefaultList = new[] {
            // Unary
            Not,              // !
            Negation,           // -
            UnaryPlus,        // +

            Equal,            // ==
            Identical,        // ===

            // Arithmetic
            Addition,              // +
            Subtraction,         // -
            Multiplication,         // *
            Division,           // /
            Remainder,        // %
            Power,            // **
                                         
            NotEqual,         // !=
            GreaterThan,      // >
            GreaterOrEqual,   // >=
            LessThan,         // <
            LessOrEqual,      // <=
            Coalesce,         // ??

            // Assignment
            Assign,           // =

            // Logical
            LogicalOr,        // ||
            LogicalAnd,       // &&

            // Casting
            Is,               // is
            As,               // as

            // Bitwise
            BitwiseNot,         // ~
            LeftShift,          // <<
            SignedRightShift,   // >>
            UnsignedRightShift, // >>>
            BitwiseXor,                // ^
            BitwiseAnd          // &
                                // ~
        };
    }

    public enum OperatorCategory
    {
        Unknown = 0,
        Arithmetic,
        Comparison,
        Logical,
        Assignment
    }

    
    // Determines the order in which operators with the same precedence are processed
    public enum Associativity
    {
        Left,
        Right
    }

    public enum OperatorType
    {
        Prefix  , // --1,    Unary
        Postfix , // 1++     Unary
        Infix   , // 1 + 1   Binary
    }
}

/*
Order of operations:
parenthesis
powers
multiply & divide
add & substract
*/

/*
1   ??
2   ||
3   && 
4   |
5   ^
6   &

7   ==, !=, !==, :=

8   >, <, <=, >=

9   <<, >>, >>>

10  +, -
11  *, /
*/


// http://en.cppreference.com/w/cpp/language/operator_precedence
// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Operators/Operator_Precedence

// infix operator ^^ { associativity left precedence 160 }

// infix ^^ (x, y) := a ** b     


