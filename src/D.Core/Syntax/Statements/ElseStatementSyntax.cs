namespace D.Syntax
{
    public class ElseStatementSyntax : ISyntaxNode
    {
        public ElseStatementSyntax(BlockSyntax body)
        {
            Body = body;
        }

        public BlockSyntax Body { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ElseStatement;
    }
}