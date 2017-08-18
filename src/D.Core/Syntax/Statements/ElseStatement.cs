namespace D.Syntax
{
    public class ElseStatementSyntax : SyntaxNode
    {
        public ElseStatementSyntax(BlockExpressionSyntax body)
        {
            Body = body;
        }

        public BlockExpressionSyntax Body { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ElseStatement;
    }
}