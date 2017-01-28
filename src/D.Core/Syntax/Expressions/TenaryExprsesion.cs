namespace D.Syntax
{    
    public class TernaryExpressionSyntax : SyntaxNode
    {
        public TernaryExpressionSyntax(SyntaxNode condition, SyntaxNode left, SyntaxNode right)
        {
            Condition = condition;
            Left = left;
            Right = right;
        }

        public SyntaxNode Condition { get; }

        public SyntaxNode Left { get; }

        public SyntaxNode Right { get; }

        public Kind Kind => Kind.TernaryExpression;
    }
}
