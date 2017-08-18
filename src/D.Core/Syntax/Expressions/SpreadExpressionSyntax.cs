namespace D.Syntax
{
    public class SpreadExpressionSyntax : SyntaxNode
    {
        public SpreadExpressionSyntax(SyntaxNode symbol)
        {
            Expression = symbol;
        }

        public SyntaxNode Expression { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.SpreadStatement;
    }
}