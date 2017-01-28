namespace D.Expressions
{
    public class ArrayLiteral : IExpression
    {
        public ArrayLiteral(IObject[] elements)
        {
            Elements = elements;
        }

        public IObject[] Elements { get; }

        public IObject this[int index] => Elements[index];

        public int Count => Elements.Length;

        // ElementKind

        public Kind Kind => Kind.ArrayLiteral;
    }
}