namespace E.Expressions;

public sealed class SpreadExpression(IExpression symbol) : IExpression
{
    public IExpression Expression { get; } = symbol;

    ObjectType IObject.Kind => ObjectType.SpreadStatement;
}