namespace D.Syntax
{
    // let a: Integer = 5
    // let mutable y: i64

    public class PropertyDeclarationSyntax : ISyntaxNode, SyntaxNode
    {
        public PropertyDeclarationSyntax(
            Symbol name, 
            TypeSymbol type, 
            SyntaxNode value = null, 
            ObjectFlags flags = ObjectFlags.None)
        {
            Name  = name;
            Type  = type;
            Value = value;
            Flags = flags;
        }

        public Symbol Name { get; }

        // String
        // String | Number
        // A & B
        public TypeSymbol Type { get; }

        public SyntaxNode Value { get; }

        public ObjectFlags Flags { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.PropertyDeclaration;
    }

    // a, b, c: Number

    public class CompoundPropertyDeclaration : SyntaxNode
    {
        public CompoundPropertyDeclaration(PropertyDeclarationSyntax[] declarations)
        {
            Members = declarations;
        }

        public PropertyDeclarationSyntax[] Members { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.CompoundPropertyDeclaration;
    }
}

/*
let a: Integer = 1;
let a: Integer > 1 = 5;
let a = 1;
var a = 1
*/