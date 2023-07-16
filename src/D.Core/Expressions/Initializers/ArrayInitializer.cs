namespace E.Expressions;

public sealed class ArrayInitializer(
    IExpression[] elements,
    int? stride = null,
    Type? elementType = null) : IExpression
{
    public IExpression[] Elements { get; } = elements;

    public int? Stride { get; } = stride;

    public Type? ElementType { get; } = elementType;

    // ElementKind

    ObjectType IObject.Kind => ObjectType.ArrayInitializer;
}