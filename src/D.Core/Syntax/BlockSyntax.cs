using System;

namespace D.Syntax
{
    public class BlockSyntax : ISyntaxNode
    {
        public BlockSyntax(params ISyntaxNode[] statements)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }

        public ISyntaxNode[] Statements { get; }

        public ISyntaxNode this[int index] => Statements[index];

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.Block;
    }
}