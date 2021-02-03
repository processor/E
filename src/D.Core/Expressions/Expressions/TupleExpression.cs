namespace E.Expressions
{
    public sealed class TupleExpression : IExpression
    {
        public TupleExpression(IExpression[] elements)
        {
            Elements = elements;
        }

        public int Size => Elements.Length;

        // {expression} | {name}:{expression}

        public IExpression[] Elements { get; }

        ObjectType IObject.Kind => ObjectType.TupleExpression;
    }
}