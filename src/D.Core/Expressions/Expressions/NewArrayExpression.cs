namespace D.Expressions
{
    public class NewArrayExpression : IExpression
    {
        public NewArrayExpression(IExpression[] elements, int? stride = null)
        {
            Elements = elements;
            Stride = stride;
        }

        public IExpression[] Elements { get; }

        public int? Stride { get; }

        // ElementKind

        public Kind Kind => Kind.NewArrayExpression;
    }
}