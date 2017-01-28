namespace D.Syntax
{
    public class NewArrayExpressionSyntax : ISyntax
    {
        public NewArrayExpressionSyntax(ISyntax[] elements, int? stride)
        {
            Elements = elements;
            Stride = stride;
        }

        public ISyntax[] Elements { get; }

        public int? Stride { get; }

        public Kind Kind => Kind.NewArrayExpression;
    }
}