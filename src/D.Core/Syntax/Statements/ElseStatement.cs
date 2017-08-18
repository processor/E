namespace D.Syntax
{
    public class ElseStatementSyntax : SyntaxNode
    {
        public ElseStatementSyntax(BlockSyntax body)
        {
            Body = body;
        }

        public BlockSyntax Body { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ElseStatement;
    }
}