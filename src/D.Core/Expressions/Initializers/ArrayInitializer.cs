using System;

namespace D.Expressions
{
    public sealed class ArrayInitializer : IExpression
    {
        public ArrayInitializer(IExpression[] elements, int? stride = null, Type elementType = null)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            Stride   = stride;
            ElementType = elementType;
        }

        public IExpression[] Elements { get; }

        public int? Stride { get; }

        public Type ElementType { get; }

        // ElementKind

        Kind IObject.Kind => Kind.ArrayInitializer;
    }
}