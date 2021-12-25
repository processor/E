namespace E.Expressions;

// => ...
public sealed class LambdaExpression : IExpression
{
    public LambdaExpression(IExpression expression)
    {
        Expression = expression;
    }

    public IExpression Expression { get; }

    ObjectType IObject.Kind => ObjectType.LambdaExpression;
}

