namespace D.Expressions
{
    // let a: Integer = 5
    // let a of type Integer equal 5
    public class VariableDeclaration : IExpression
    {
        public VariableDeclaration(string name, IType type, VariableFlags flags, IExpression value = null)
        {
            Name = name;
            Type = type;
            Flags = flags;
            Value = value;
        }

        public string Name { get; }

        public IType Type { get; }

        public VariableFlags Flags { get; }

        public IExpression Value { get; }

        public bool IsMutable => Flags.HasFlag(VariableFlags.Mutable);

        Kind IObject.Kind => Kind.VariableDeclaration;
    }

    public class CompoundVariableDeclaration : IExpression
    {
        public CompoundVariableDeclaration(VariableDeclaration[] declarations)
        {
            Declarations = declarations;
        }

        public VariableDeclaration[] Declarations { get; }

        Kind IObject.Kind => Kind.CompoundVariableDeclaration;
    }
}

/*
let a: Integer = 1;
let a: Integer > 1 = 5;
let mutable a = 1;
var a = 1
*/