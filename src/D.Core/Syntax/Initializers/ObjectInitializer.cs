namespace D.Syntax
{
    public class ObjectInitializerSyntax : SyntaxNode
    {
        public ObjectInitializerSyntax(TypeSymbol type, ObjectPropertySyntax[] properties)
        {
            Type = type;
            Properties = properties;
        }

        public TypeSymbol Type { get; }

        public ObjectPropertySyntax[] Properties { get; }

        Kind IObject.Kind => Kind.ObjectInitializer; 
    }

    // { a: 1, b: 2 }
    // { a, b, c }

    public struct ObjectPropertySyntax
    {
        public ObjectPropertySyntax(Symbol auto)
        {
            Name = auto;
            Value = auto;
            Implict = true;
        }

        public ObjectPropertySyntax(Symbol name, SyntaxNode value)
        {
            Name = name;
            Value = value;
            Implict = false;
        }

        public Symbol Name { get; }

        public bool Implict { get; }

        public SyntaxNode Value { get; }
    }

    // Point { x: 1, y: 2 }
    // Rust Notes: There is exactly one way to create an instance of a user-defined type: name it, and initialize all its fields at once:
}