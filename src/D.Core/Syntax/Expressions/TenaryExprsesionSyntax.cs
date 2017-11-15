namespace D.Syntax
{    
    public class TernaryExpressionSyntax : ISyntaxNode
    {
        public TernaryExpressionSyntax(ISyntaxNode condition, ISyntaxNode left, ISyntaxNode right)
        {
            Condition = condition;
            Left = left;
            Right = right;
        }

        public ISyntaxNode Condition { get; }

        public ISyntaxNode Left { get; }

        public ISyntaxNode Right { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TernaryExpression;
    }
}
