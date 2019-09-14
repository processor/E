namespace D.Syntax
{
    public sealed class SpreadExpressionSyntax : ISyntaxNode
    {
        public SpreadExpressionSyntax(ISyntaxNode symbol)
        {
            Expression = symbol;
        }

        public ISyntaxNode Expression { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.SpreadStatement;
    }
}