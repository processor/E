namespace D.Syntax
{
    public class VariableDeclarationSyntax : IMemberSyntax, ISyntaxNode
    {
        public VariableDeclarationSyntax(
            Symbol name, 
            TypeSymbol type, 
            ISyntaxNode value = null, 
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

        public ISyntaxNode Value { get; }

        public ObjectFlags Flags { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.VariableDeclaration;
    }

    // a, b, c: Number

    public class CompoundVariableDeclaration : ISyntaxNode
    {
        public CompoundVariableDeclaration(PropertyDeclarationSyntax[] declarations)
        {
            Members = declarations;
        }

        public PropertyDeclarationSyntax[] Members { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.CompoundVariableDeclaration;
    }
}

/*
let a: Integer = 1;
let a: Integer > 1 = 5;
let a = 1;
var a = 1
*/