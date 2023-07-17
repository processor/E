namespace E.Expressions;

// => ...
public sealed class LambdaExpression(IExpression expression) : IExpression
{
    public IExpression Expression { get; } = expression;

    ObjectType IObject.Kind => ObjectType.LambdaExpression;
}