namespace E.Syntax
{
    public enum SyntaxKind
    {
        Object = 1,
        Symbol = 6,

        // Literal Expressions
        NullLiteral,
        BooleanLiteral,
        CharacterLiteral,
        RangeLiteral,
        NumberLiteral,
        StringLiteral,
        UnitValueLiteral,

        // Declarations
        TypeDeclaration,
        FunctionDeclaration,
        ObserverDeclaration,
        PropertyDeclaration,
        CompoundPropertyDeclaration,
        VariableDeclaration,
        CompoundVariableDeclaration,
        ProtocolDeclaration,
        ChannelDeclaration,
        ImplementationDeclaration,
        UnitDeclaration,
        OperatorDeclaration,
        ImportDeclaration,

        DestructuringAssignment,

        InterpolatedStringExpression,
        PipeStatement,  // Merge with Call?
        SpreadStatement,

        // Patterns
        AnyPattern,
        ArrayPattern,
        ConstantPattern,
        RangePattern,
        ObjectPattern,
        TuplePattern,
        TypePattern,
        TupleElement,

        // Statements
        Block, // { ... }
        ForStatement, // for x in y
        IfStatement,
        ElseIfStatement,
        ElseStatement,
        UsingStatement,
        ObserveStatement,
        EmitStatement,
        ReturnStatement,
        YieldStatement,

        WhileStatement,
        ModuleStatement,

        // Expressions
        CoalesceExpression, // ??               
        AnnotationExpression,
        CallExpression,
        TupleExpression,
        LambdaExpression,
        MatchExpression,
        QueryExpression,

        ArrayInitializer,
        TypeInitializer,

        // Unary             
        UnaryExpression,
        BinaryExpression, // +
        TernaryExpression,  // ? :             

        MemberAccessExpression, // a.b
        IndexAccessExpression, // a[b]


        Element, // <element />
        TextNode
    }
}