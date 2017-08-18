namespace D
{
    public enum SyntaxKind
    {
        Object  = 1,
        Symbol  = 6,
    
        Function     = 8,  // ƒ
        
        // Literal Expressions
        NullLiteral         = 0, 
        BooleanLiteral      = 1, 
        CharacterLiteral    = 21,
        RangeLiteral        = 2007,
        NumberLiteral       = 2008,
        StringLiteral       = 2009,
        UnitLiteral         = 11,
       
        // Declarations
        TypeDeclaration             = 2201,
        FunctionDeclaration         = 2202,
        ObserverDeclaration         = 2203,
        PropertyDeclaration         = 2204, // Let ?
        CompoundPropertyDeclaration = 2205,
        ProtocolDeclaration         = 2206,
        ChannelDeclaration          = 2207,
        ImplementationDeclaration   = 2208,
        UnitDeclaration             = 2209,
        OperatorDeclaration         = 2010,
        ImportDeclaration           = 2011,

        DestructuringAssignment = 2220,

        InterpolatedStringExpression    = 2221,
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

        NamedValue,

        Argument,

        // Statements
        BlockStatement           = 3000, // { ... }
        ReturnStatement          = 3001,
        ForStatement             = 3003,
        IfStatement              = 3004,
        ElseIfStatement          = 3005,
        ElseStatement            = 3006,
        UsingStatement           = 3007,
        ObserveStatement         = 3008,
        EmitStatement            = 3009,
        WhileStatement           = 3010,
        ModuleStatement          = 3012,
                              
        CoalesceExpression       = 5001, // ??   
                                 
        AnnotationExpression     = 5003,
        CallExpression           = 5004,
        ConstantExpression       = 2505,
        TupleExpression          = 5006,
        LambdaExpression         = 5007,
        MatchExpression          = 5008,
        QueryExpression          = 5009,
                                 
        ArrayInitializer         = 5020,
        TypeInitializer          = 5021,
                                 
        // Unary                 
        UnaryExpression          = 6000,
        BinaryExpression         = 6010, // +
        TernaryExpression        = 7072,  // ? :             
                                 
        MemberAccessExpression   = 6051, // a.b
        IndexAccessExpression    = 6052, // a[b]
    }
}