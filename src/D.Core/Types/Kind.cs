namespace D
{
    public enum Kind
    {
        Null            = 0, // Null
        Boolean         = 1, // boolean
        Number          = 2, // Alias Float64: IEEE-754 encoded. Matches JavaScript
        Object          = 3, // * | Any type

        String          = 5,
        Symbol          = 6,
        Type            = 7,         
        FunctionExpression        = 8,  // ƒ

        // 9-15
        Pointer        = 9,
        Expression     = 10,
        UnitLiteral    = 11, // A currency is a unit?
        Currency       = 12,
        Entity         = 13,
        Timestamp      = 14,

        Protocol       = 17,
        ImplementationExpression = 18,
        // Variable       = 19,
        Void           = 20, // static instance = nothing
        Character      = 21,

        // Collections
        List           = 101,
        Set            = 102,

        Byte           = 103,
       
        Rational       = 111, // 1/3                        // ?
        Decimal        = 113,                               // 128 bit
        Vector         = 114,
        Complex        = 115,
        Matrix         = 116,

        // Integers {…, -2, -1, 0, 1, 2,…}  
        Int16           = 119,
        Int32           = 120,
        Int64           = 121,
        Float32         = 122,
        Float64         = 123,


        #region 200-300 Vectors

        Vector64     = 200,
        Vector128    = 201,
        Vector256    = 202,
        Vector512    = 203,
        Vector1024   = 204, // future
        Vector2048   = 205, // future

        #endregion

        Operator = 300,
        Verb     = 301,

        #region Geometry (400-500)

        Arc             = 400,
        Curve           = 401,
        Line            = 402,
        Path            = 403, // Point *
        Plane           = 404,
        Point           = 405, // Vector | (x,y,z?)
        Polygon         = 406, // Path Point * 
        Quaternion      = 407,
        Ray             = 408, // (position: Vector3, direction: Vector3)

        // 2D
        Circle          = 420,
        Ellipse         = 421,
        Triangle        = 422,
        Rectangle       = 423, 

        // 3D
        Box             = 450,  // Cube has same dimensions on all sides
        Cone            = 451,
        Cylinder        = 452,
        Ellipsoid       = 453,
        Prism           = 454,
        Pyramid         = 455,
        Sphere          = 456,
        Torus           = 457,
        
        #endregion

        #region Declaration & Statement Expressions

        // Literal Expressions
      
        RangeLiteral     = 2007,
        NumberLiteral    = 2008,
        StringLiteral    = 2009,
        Equation         = 2010,
       
        Predicate        = 2100,

        // Declarations
        TypeDeclaration                 = 2201,
        FunctionDeclaration             = 2202,
        ObserverDeclaration             = 2203,
        VariableDeclaration             = 2204, // Let ?
        CompoundVariableDeclaration     = 2205,
        ProtocolDeclaration             = 2206,
        ChannelDeclaration              = 2207,
        ImplementationDeclaration       = 2208,
        UnitDeclaration                 = 2209,
        OperatorDeclaration             = 2010,
        ImportDeclaration               = 2011,



        DestructuringAssignment         = 2220,

        InterpolatedStringExpression    = 2221,


        PipeStatement = 2302,  // Merge with Call?
        
        SpreadStatement = 2305,



        // Patterns
        AnyPattern = 4000,
        ArrayPattern    = 4001,
        ConstantPattern = 4002,
        RangePattern    = 4003,
        ObjectPattern   = 4004,
        TuplePattern    = 4005,
        TypePattern     = 4006,

        NamedValue,

        Argument,
        Parameter,
        Property,

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
        ModuleStatement         = 3012,


        CoalesceExpression       = 5001, // ??

        
        AnnotationExpression     = 5003,
        CallExpression           = 5004,
        ConstantExpression       = 2505,
        TupleExpression          = 5006,
        LambdaExpression         = 5007,
        MatchExpression          = 5008,
        QueryExpression          = 5009,

        ArrayInitializer       = 5020,
        ObjectInitializer      = 5021,

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
        GreaterThanExpression           = 6020, // > 
        GreaterThanOrEqualExpression    = 6021, // >=, ≥
        LessThanExpression              = 6022, // <
        LessThanOrEqualExpression       = 6023, // <=, ≤

        // Binary - Equality
        EqualsExpression           = 6031, // ==
        IdenticalExpression               = 6032, // ===, ≡
        NotEqualsExpression                = 6033, // !=, ≠
        StrictNotEqual          = 6034, // !==

        TernaryExpression = 7072,  // ? : 

        // Binary - Assignment
        AssignmentExpression = 6040, // = 
     
        // Binary - Access
        MemberAccessExpression    = 6051, // a.b
        IndexAccessExpression               = 6052, // a[b]

        // Binary - Bitwise
        XorExpression                 = 6060, // ⊕     exclusive or - xor	
        
        LeftShiftExpression           = 6061, // <<
        RightShiftExpression          = 6062, // >>
        UnsignedRightShiftExpression  = 6063, // >>>
        BitwiseNotExpression          = 6064, // ~
        BitwiseAndExpression          = 6065, // &
        BitwiseOrExpression           = 6066, // |

        // Binary - Sets
        Intersection    = 6070,
        Union           = 6071,
        Subset          = 6072,
        ProperSubset    = 6073,
        NotSubset       = 6074,
        Superset        = 6075,
        ProperSuperset  = 6076,
        NotSuperset     = 6077,
        ElementOf       = 6078, // ∈
        NotElementOf    = 6079, // ∉

        Integral        = 6080, // ∫
        DoubleIntegral  = 6081,
        TripleIntegral  = 6082,
        Derivative      = 6083,
        Sigma           = 6084,

        #endregion

        #region Documents

        Document

        , TextContent

        // Block Elements
        , FooterElement         // <foooter /> 
        , HeaderElement         // <header />
        , HeadingElement        // <heading />  
        , ParagraphElement      // <paragraph />      
        , SectionElement        // <section />
        , ListElement           // <list />

        // Inline Elements
        , FigureElement          // <figure />
        , QuoteElement           // <quote />
        , CitationElement        // <citation />
        , CreditElement          // <credit />
        , DetailElement          // <detail />

        , TableElement           // <table />
        , RowElement             // <row />
        , ColumnElement          // <column />

        // Code
        , EquationElement        // <equation />
        , CodeElement            // <code />

        // Forms
        , LabelElement           // <label />
        , FieldElement
        , InputElement
        , FormElement

        // Media
        , AudioElement
        , ImageElement
        , VideoElement
        
        // Graphic Elements
        , CanvasElement          // <canvas />
        , GraphicElement         // <graphic />     | may be 3d
        , LineElement            // <line />
        , PathElement            // <path />
        , CircleElement          // <circle />
        , GlyphElement           // <glyph />
        , PolygonElement         // <polygon />

        // Layouts
        , GridLayout
        , MasonaryLayout

        #endregion

        #region Functions

        , Map
        , Filter
        , Split
        , Skip
        , Take
        , Uppercase
        , Lowercase
        , Min
        , Max
        , Slice
        , Push
        , Pop
        , Shift
        , Sort
        , Abs
        , Acos
        , Asin
        , Atan
        , Atan2
        , Ceiling
        , Floor
        
        #endregion
    }

    // LLVM notes
    // type { i8, [10 x [20 x i32]], i8 }
    // [2 x [3 x [4 x i16]]]	
}


/*
// ≡ identical
// := equal by defination

// = equality
// ≠ inquality
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
