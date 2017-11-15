namespace D.Syntax
{
    public class SpreadExpressionSyntax : ISyntaxNode
    {
        public SpreadExpressionSyntax(ISyntaxNode symbol)
        {
            Expression = symbol;
        }

        public ISyntaxNode Expression { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.SpreadStatement;
    }
}