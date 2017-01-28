namespace D.Expressions
{
    public class ArrayLiteral : IExpression
    {
        public ArrayLiteral(IExpression[] elements)
        {
            Elements = elements;
        }

        public IExpression[] Elements { get; }

        public IExpression this[int index] => Elements[index];

        public int Count => Elements.Length;

        // ElementKind

        public Kind Kind => Kind.ArrayLiteral;
    }
}