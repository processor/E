using D.Symbols;

namespace D.Syntax
{
    public sealed class PropertyDeclarationSyntax : IMemberSyntax, ISyntaxNode
    {
        public PropertyDeclarationSyntax(
            Symbol name, 
            TypeSymbol type, 
            ISyntaxNode? value = null, 
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

        public ISyntaxNode? Value { get; }

        public ObjectFlags Flags { get; }

        public bool IsMutable => Flags.HasFlag(ObjectFlags.Mutable);

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.PropertyDeclaration;
    }

    // a, b, c: Number

    public sealed class CompoundPropertyDeclaration : ISyntaxNode
    {
        public CompoundPropertyDeclaration(PropertyDeclarationSyntax[] declarations)
        {
            Members = declarations;
        }

        public PropertyDeclarationSyntax[] Members { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.CompoundPropertyDeclaration;
    }
}

/*
let a: Integer = 5
let y: i64
let a: Integer = 1;
let a: Integer > 1 = 5;
let a = 1;
var a = 1
*/
