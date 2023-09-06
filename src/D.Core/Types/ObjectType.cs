namespace E;

public enum ObjectType
{
    Null           = 0, // Null
    Boolean        = 520_777, // boolean
    Number         = 11_563, // Alias Float64: IEEE-754 encoded. Matches JavaScript
    Object         = 3, // * | Any type
                       
    String         = 184_754,
    Symbol         = 80_071,
    Type           = 190_087,
    Function       = 8,  // ƒ

    // 9-15
    Pointer        = 118_155,
    Expression     = 778_379,
    UnitValue      = 11,
    Currency       = 8_142,
    Entity         = 35_120,
    Timestamp      = 186_885,
    Map            = 80_585, 

    Protocol       = 7_251_920, // AKA interface
    Implementation = 245_962,
    Void           = 513_000, // static instance = nothing
    Character      = 3_241_972,

    Unit           = 47_574, // of measurement

    // Collections
    Array          = 186_152,
    Set            = 36_161,
    Tuple          = 600_590,

    // Numerics
    Rational       = 1_244_890,  // 1/3
    Decimal        = 2_0154_908,
    Vector         = 114,
    Complex        = 11_567,
    Matrix         = 44_337,

    Decimal32      = 5_249_164,
    Decimal64      = 5_249_165,
    Decimal128     = 5_249_163,

    // Integers {…, -2, -1, 0, 1, 2,…}  
    Byte            = 8_799,
    Int8            = 110_660_555, // sbyte
    Int16           = 119,
    Int32           = 110_644_030,
    Int64           = 9_358_198,
    Float16         = 1_994_657,
    Float32         = 1_307_173,
    Float64         = 1_243_369,

    // Brain Floats
    BFloat16        = 54_083_815,

    // Vectors
    Vector64        = 200,
    Vector128       = 201,
    Vector256       = 202,
    Vector512       = 203,
    Vector1024      = 204,
    Vector2048      = 205,
        
    Operator        = 1_206_110,
    Operation       = 3_884_033,

    #region Declaration & Statement Expressions

    // Literal Expressions
      
    RangeLiteral     = 2007,
    NumericLiteral   = 11_761_915,
    StringLiteral    = 4_736_519,
    Equation         = 11_345,
       
    Predicate        = 1_144_319,

    // Declarations
    TypeDeclaration              = 2201,
    FunctionDeclaration          = 2202,
    ObserverDeclaration          = 2203,
    PropertyDeclaration          = 2204, // Let ?
    CompoundPropertyDeclaration  = 2205,
    ProtocolDeclaration          = 2206,
    ChannelDeclaration           = 2207,
    ImplementationDeclaration    = 2208,
    UnitDeclaration              = 2209,
    OperatorDeclaration          = 2010,
    ImportDeclaration            = 2011,

    DestructuringAssignment      = 110_745_268,

    InterpolatedStringExpression = 2221,

    PipeStatement = 2302,  // Merge with Call?
        
    SpreadStatement = 2305,

    // Patterns
    AnyPattern      = 4000,
    ArrayPattern    = 4001,
    ConstantPattern = 4002,
    RangePattern    = 4003,
    ObjectPattern   = 4004,
    TuplePattern    = 4005,
    TypePattern     = 4006,

    TupleElement,

    Argument  = 1_027_788,
    Parameter = 1_410_440,
    Property  = 937_228,

    #endregion

    #region Expressions & Statements

    // Statements
    BlockStatement          = 3000, // { ... }
    ReturnStatement         = 3001,
    ForStatement            = 3003,
    IfStatement             = 3004,
    ElseIfStatement         = 3005,
    ElseStatement           = 3006,
    UsingStatement          = 3007,
    ObserveStatement        = 3008,
    EmitStatement           = 3009,
    WhileStatement          = 3010,
    Module                  = 3012,

    CoalesceExpression       = 5001, // ??
        
    AnnotationExpression     = 5003,
    CallExpression           = 5004,
    ConstantExpression       = 2505,
    TupleExpression          = 5006,
    LambdaExpression         = 5007,
    MatchExpression          = 5008,
    QueryExpression          = 5009,

    ArrayInitializer        = 5020,
    TypeInitializer         = 5021,

    // Unary                 
    LogicalNotExpression     = 6000, // ! prefix
    UnaryPlusExpression      = 6001, // + prefix
    NegateExpression         = 6002, // - prefix
                                 
    // Binary: Logical       
    LogicalAndExpression     = 6003, // &&
    LogicalOrExpression      = 6004, // ||

    // Binary - Arithmetic
    AddExpression            = 6010, // +
    SubtractExpression       = 6011, // -
    MultiplyExpression       = 6012, // *, ∙
    DivideExpression         = 6013, // /, 
    ModuloExpression         = 6014, // %, mod
    ExponentiationExpression = 6015, // **, ^

    // Binary - Casting
    IsExpression              = 6016,
    AsExpression              = 6017,

    // Binary - Comparision
    GreaterThanExpression        = 6020, // > 
    GreaterThanOrEqualExpression = 6021, // >=, ≥
    LessThanExpression           = 6022, // <
    LessThanOrEqualExpression    = 6023, // <=, ≤

    // Binary - Equality
    EqualsExpression             = 6031, // ==
    IdenticalExpression          = 6032, // ===, ≡
    NotEqualsExpression          = 6033, // !=, ≠
    StrictNotEqual               = 6034, // !==

    TernaryExpression = 7072,  // ? : 

    // Binary - Assignment
    AssignmentExpression = 6040, // = 
     
    // Binary - Access
    MemberAccessExpression    = 6051, // a.b
    IndexAccessExpression     = 6052, // a[b]

    // Binary - Bitwise
    XorExpression                 = 6060, // ⊕     exclusive or - xor	
        
    LeftShiftExpression           = 6061, // <<
    RightShiftExpression          = 6062, // >>
    UnsignedRightShiftExpression  = 6063, // >>>
    BitwiseNotExpression          = 6064, // ~
    BitwiseAndExpression          = 6065, // &
    BitwiseOrExpression           = 6066, // |

    // Binary - Sets
    Intersection    = 185_837,
    Union           = 185_359,
    Subset          = 177_646,
    ProperSubset    = 59_489_332,
    NotSubset       = 6074,
    Superset        = 15_882_515,
    ProperSuperset  = 6076,
    NotSuperset     = 6077,
    ElementOf       = 655_288,     // ∈
    NotElementOf    = 109_610_770, // ∉

    Integral        = 80_091,   // ∫    | Q80091
    DoubleIntegral  = 9185332, // ∬ 
    TripleIntegral  = 110_660_584,
    Derivative      = 29_175,
    Sigma           = 159_375,

    #endregion

    #region Documents

    Document    = 7000,
    Element     = 7001,
    Fragment    = 7002,
    TextContent = 7003
        
    #endregion
}

// LLVM notes
// type { i8, [10 x [20 x i32]], i8 }
// [2 x [3 x [4 x i16]]]	

/*
// ≡ identical
// := equal by defination

// = equality
// ≠ inequality
// > greater then
// ~ weak approximation
// ≈ approximation
// ≪ much less than
// { } SET
// 

EmptySet,       // Ø
Imply,          // ⇒
ForAll,         // ∀
ThereExist,     // ∃
ThereNotExist,  // ∄
Therefore,      // ∴
Because,        // ∵
*/
