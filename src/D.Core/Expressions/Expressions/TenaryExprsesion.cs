namespace E.Expressions;

public sealed class TernaryExpression(
    IExpression condition,
    IExpression left,
    IExpression right) : IExpression
{
    public IExpression Condition { get; } = condition;

    public IExpression Left { get; } = left;

    public IExpression Right { get; } = right;

    public ObjectType Kind => ObjectType.TernaryExpression;
}