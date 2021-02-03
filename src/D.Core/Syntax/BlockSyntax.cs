namespace E.Syntax
{
    public sealed class BlockSyntax : ISyntaxNode
    {
        public BlockSyntax(params ISyntaxNode[] statements)
        {
            Statements = statements;
        }

        public ISyntaxNode[] Statements { get; }

        public ISyntaxNode this[int index] => Statements[index];

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.Block;
    }
}