namespace E.Expressions;

public sealed class ArrayInitializer : IExpression
{
    public ArrayInitializer(
        IExpression[] elements,
        int? stride = null,
        Type? elementType = null)
    {
        Elements = elements;
        Stride = stride;
        ElementType = elementType;
    }

    public IExpression[] Elements { get; }

    public int? Stride { get; }

    public Type? ElementType { get; }

    // ElementKind

    ObjectType IObject.Kind => ObjectType.ArrayInitializer;
}