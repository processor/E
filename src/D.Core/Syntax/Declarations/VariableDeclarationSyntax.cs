namespace D.Syntax
{
    public class VariableDeclarationSyntax : IMemberSyntax, SyntaxNode
    {
        public VariableDeclarationSyntax(
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

        SyntaxKind SyntaxNode.Kind => SyntaxKind.VariableDeclaration;
    }

    // a, b, c: Number

    public class CompoundVariableDeclaration : SyntaxNode
    {
        public CompoundVariableDeclaration(PropertyDeclarationSyntax[] declarations)
        {
            Members = declarations;
        }

        public PropertyDeclarationSyntax[] Members { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.CompoundVariableDeclaration;
    }
}

/*
let a: Integer = 1;
let a: Integer > 1 = 5;
let a = 1;
var a = 1
*/