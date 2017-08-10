namespace D.Syntax
{
    // let a: Integer = 5
    // let mutable y: i64

    public class VariableDeclarationSyntax : SyntaxNode
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

        public TypeSymbol Type { get; }

        public SyntaxNode Value { get; }

        public ObjectFlags Flags { get; }

        Kind IObject.Kind => Kind.VariableDeclaration;
    }

    public class CompoundVariableDeclaration : SyntaxNode
    {
        public CompoundVariableDeclaration(VariableDeclarationSyntax[] declarations)
        {
            Declarations = declarations;
        }

        public VariableDeclarationSyntax[] Declarations { get; }

        Kind IObject.Kind => Kind.CompoundVariableDeclaration;
    }
}

/*
let a: Integer = 1;
let a: Integer > 1 = 5;
let a = 1;
var a = 1
*/