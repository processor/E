namespace D.Expressions
{
    public sealed class CompoundVariableDeclaration : IExpression
    {
        public CompoundVariableDeclaration(VariableDeclaration[] declarations)
        {
            Declarations = declarations;
        }

        public VariableDeclaration[] Declarations { get; }

        ObjectType IObject.Kind => ObjectType.CompoundPropertyDeclaration;
    }
}