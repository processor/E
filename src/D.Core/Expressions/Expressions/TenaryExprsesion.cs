namespace E.Expressions;

public sealed class TernaryExpression(
    IExpression condition,
    IExpression lhs,
    IExpression rhs) : IExpression
{
    public IExpression Condition { get; } = condition;

    public IExpression Left { get; } = lhs;

    public IExpression Right { get; } = rhs;

    public ObjectType Kind => ObjectType.TernaryExpression;
}