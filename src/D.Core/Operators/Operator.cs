namespace E;

using static Associativity;

public sealed class Operator(
    ObjectType kind,
    string name,
    OperatorType type,
    int precedence = 1000,
    Associativity associativity = Left) : INamedObject
{
    public string Name { get; } = name;

    public ObjectType OpKind { get; } = kind;

    public int Precedence { get; } = precedence;

    public Associativity Associativity { get; } = associativity;

    public OperatorType Type { get; } = type;

    public bool IsLogical => OpKind 
        is ObjectType.LogicalAndExpression // &&
        or ObjectType.LogicalOrExpression; // ||

    // >
    // >=
    // <
    // <=
    // ==
    // ===
    // !=
    public bool IsComparison => (int)OpKind is >= 6_020 and <= 6_034;

    public static Operator Prefix(ObjectType kind, string name, int precedence, Associativity associativity = Left)
    {
        return new Operator(kind, name, OperatorType.Prefix, precedence, associativity);
    }
        
    public static Operator Infix(ObjectType kind, string name, int precedence, Associativity associativity = Left)
    {
        return new Operator(kind, name, OperatorType.Infix, precedence, associativity);
    }

    // highest
    public static readonly Operator Not                = Prefix(ObjectType.LogicalNotExpression,        "!",   precedence: 15, associativity: Right);
    public static readonly Operator Negation           = Prefix(ObjectType.NegateExpression,            "-",   precedence: 15, associativity: Right);
    public static readonly Operator UnaryPlus          = Prefix(ObjectType.UnaryPlusExpression,         "+",   precedence: 15, associativity: Right);
    public static readonly Operator BitwiseNot         = Prefix(ObjectType.BitwiseNotExpression,        "~",   precedence: 15, associativity: Right);

    public static readonly Operator Power              = Infix(ObjectType.ExponentiationExpression,     "**",  precedence: 14, associativity: Right);  
    public static readonly Operator Multiply           = Infix(ObjectType.MultiplyExpression,           "*",   precedence: 14);                        
    public static readonly Operator Divide             = Infix(ObjectType.DivideExpression,             "/",   precedence: 14);                        
    public static readonly Operator Remainder          = Infix(ObjectType.ModuloExpression,             "%",   precedence: 14);                       

    public static readonly Operator Add                = Infix(ObjectType.AddExpression,                "+",   precedence: 13);
    public static readonly Operator Subtract           = Infix(ObjectType.SubtractExpression,           "-",   precedence: 13);
    public static readonly Operator LeftShift          = Infix(ObjectType.LeftShiftExpression,          "<<",  precedence: 12);
    public static readonly Operator SignedRightShift   = Infix(ObjectType.RightShiftExpression,         ">>",  precedence: 12);
    public static readonly Operator UnsignedRightShift = Infix(ObjectType.UnsignedRightShiftExpression, ">>>", precedence: 12);

    public static readonly Operator GreaterThan        = Infix(ObjectType.GreaterThanExpression,        ">",   precedence: 11);
    public static readonly Operator GreaterOrEqual     = Infix(ObjectType.GreaterThanOrEqualExpression, ">=",  precedence: 11);
    public static readonly Operator LessThan           = Infix(ObjectType.LessThanExpression,           "<",   precedence: 11);
    public static readonly Operator LessOrEqual        = Infix(ObjectType.LessThanOrEqualExpression,    "<=",  precedence: 11);     
                                                                                           
    public static readonly Operator Equal              = Infix(ObjectType.EqualsExpression,             "==",  precedence: 10);
    public static readonly Operator Identical          = Infix(ObjectType.IdenticalExpression,          "===", precedence: 10); 
    public static readonly Operator NotEqual           = Infix(ObjectType.NotEqualsExpression,          "!=",  precedence: 10);
    public static readonly Operator StrictNotEqual     = Infix(ObjectType.StrictNotEqual,               "!==", precedence: 10);

    // Bitwise
    public static readonly Operator BitwiseXor         = Infix(ObjectType.XorExpression,                 "^",  precedence: 9);

    public static readonly Operator Coalesce           = Infix(ObjectType.CoalesceExpression,           "??", precedence: 1);
                                                           
    public static readonly Operator LogicalAnd         = Infix(ObjectType.LogicalAndExpression,         "&&",  precedence: 6);
    public static readonly Operator LogicalOr          = Infix(ObjectType.LogicalOrExpression,          "||",  precedence: 5);

    public static readonly Operator Assign             = Infix(ObjectType.AssignmentExpression,         "=",   precedence: 3, associativity: Right);  

    public static readonly Operator Is                 = Infix(ObjectType.IsExpression,                 "is", precedence: 3, associativity: Right);
    public static readonly Operator As                 = Infix(ObjectType.AsExpression,                 "as", precedence: 3, associativity: Right);

    public static readonly Operator BitwiseAnd         = Infix(ObjectType.BitwiseAndExpression,         "&",  precedence: 20); // check precedence

    // lowest

    ObjectType IObject.Kind => ObjectType.Operator;

    public static readonly Operator[] DefaultList = [
        // Unary
        Not,              // !
        Negation,         // -
        UnaryPlus,        // +

        Equal,            // ==
        Identical,        // ===

        // Arithmetic
        Add,              // +
        Subtract,         // -
        Multiply,         // *
        Divide,           // /
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
        BitwiseXor,         // ^
        BitwiseAnd          // &
                            // ~
    ];
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


