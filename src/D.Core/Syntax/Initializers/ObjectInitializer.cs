namespace D.Syntax
{
    // e.g. Point(x, y, z)

    public class ObjectInitializerSyntax : SyntaxNode
    {
        public ObjectInitializerSyntax(TypeSymbol type, ArgumentSyntax[] arguments)
        {
            Type      = type;
            Arguments = arguments;
        }

        public TypeSymbol Type { get; }

        public ArgumentSyntax[] Arguments { get; }

        Kind IObject.Kind => Kind.TypeInitializer; 
    }

    // { a: 1, b: 2 }
    // { a, b, c }
    

    // Point { x: 1, y: 2 }
    // Rust Notes: There is exactly one way to create an instance of a user-defined type: name it, and initialize all its fields at once:
}