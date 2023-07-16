namespace E.Expressions;

public sealed class TupleExpression(IExpression[] elements) : IExpression
{
    public int Size => Elements.Length;

    // {expression} | {name}:{expression}

    public IExpression[] Elements { get; } = elements;

    ObjectType IObject.Kind => ObjectType.TupleExpression;
}