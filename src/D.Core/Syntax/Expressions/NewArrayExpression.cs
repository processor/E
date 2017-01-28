namespace D.Syntax
{
    public class NewArrayExpressionSyntax : SyntaxNode
    {
        public NewArrayExpressionSyntax(SyntaxNode[] elements, int? stride)
        {
            Elements = elements;
            Stride = stride;
        }

        public SyntaxNode[] Elements { get; }

        public int? Stride { get; }

        public Kind Kind => Kind.NewArrayExpression;
    }
}