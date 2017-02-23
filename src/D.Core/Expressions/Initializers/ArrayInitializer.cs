namespace D.Expressions
{
    public class ArrayInitializer : IExpression
    {
        public ArrayInitializer(IExpression[] elements, int? stride = null)
        {
            Elements = elements;
            Stride = stride;
        }

        public IExpression[] Elements { get; }

        public int? Stride { get; }

        // ElementKind

        public Kind Kind => Kind.ArrayInitializer;
    }
}