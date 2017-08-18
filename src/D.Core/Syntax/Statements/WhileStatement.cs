namespace D.Syntax
{
    public class WhileStatementSyntax : SyntaxNode
    {
        public WhileStatementSyntax(SyntaxNode condition, BlockExpressionSyntax body)
        {
            Condition = condition;
            Body = body;
        }

        public SyntaxNode Condition { get; }

        public BlockExpressionSyntax Body { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.WhileStatement;
    }
}