namespace E.Syntax
{
    public sealed class YieldStatementSyntax : ISyntaxNode
    {
        public YieldStatementSyntax(ISyntaxNode expression)
        {
            Expression = expression;
        }

        public ISyntaxNode Expression { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.YieldStatement;
    }
}