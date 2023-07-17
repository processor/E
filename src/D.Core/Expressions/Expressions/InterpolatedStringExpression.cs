namespace E.Expressions;

// $"{expression}text"
public sealed class InterpolatedStringExpression(params IExpression[] children) : IExpression
{
    public IExpression[] Children { get; } = children;

    public IExpression this[int index] => Children[index];

    ObjectType IObject.Kind => ObjectType.InterpolatedStringExpression;
}