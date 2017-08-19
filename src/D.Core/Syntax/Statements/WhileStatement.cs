namespace D.Syntax
{
    public class WhileStatementSyntax : SyntaxNode
    {
        public WhileStatementSyntax(SyntaxNode condition, BlockSyntax body)
        {
            Condition = condition;
            Body = body;
        }

        public SyntaxNode Condition { get; }

        public BlockSyntax Body { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.WhileStatement;
    }
}