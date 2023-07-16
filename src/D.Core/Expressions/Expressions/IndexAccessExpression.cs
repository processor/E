namespace E.Expressions;

// [index]
public sealed class IndexAccessExpression(IExpression left, IArguments arguments) : IExpression
{
    public IExpression Left { get; } = left;

    // [1]
    // [1, 2]
    public IArguments Arguments { get; } = arguments;

    ObjectType IObject.Kind => ObjectType.IndexAccessExpression;
}