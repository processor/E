namespace E.Expressions;

public sealed class CompoundVariableDeclaration(VariableDeclaration[] declarations) : IExpression
{
    public VariableDeclaration[] Declarations { get; } = declarations;

    ObjectType IObject.Kind => ObjectType.CompoundPropertyDeclaration;
}