using System;

namespace D.Syntax
{
    public class BlockSyntax : SyntaxNode
    {
        public BlockSyntax(params SyntaxNode[] statements)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }

        public SyntaxNode[] Statements { get; }

        public SyntaxNode this[int index] => Statements[index];

        SyntaxKind SyntaxNode.Kind => SyntaxKind.Block;
    }
}