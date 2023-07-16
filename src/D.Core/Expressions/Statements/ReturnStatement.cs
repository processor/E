namespace E.Expressions;

public sealed class ReturnStatement(IExpression expression) : IExpression
{
    public IExpression Expression { get; } = expression;

    ObjectType IObject.Kind => ObjectType.ReturnStatement;
}
