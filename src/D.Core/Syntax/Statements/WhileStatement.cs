namespace D.Syntax
{
    public class WhileStatementSyntax : ISyntaxNode
    {
        public WhileStatementSyntax(ISyntaxNode condition, BlockSyntax body)
        {
            Condition = condition;
            Body = body;
        }

        public ISyntaxNode Condition { get; }

        public BlockSyntax Body { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.WhileStatement;
    }
}