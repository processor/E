using System;

namespace D.Syntax
{
    public class BlockExpressionSyntax : SyntaxNode
    {
        public BlockExpressionSyntax(params SyntaxNode[] statements)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }

        public SyntaxNode[] Statements { get; }

        public SyntaxNode this[int index] => Statements[index];

        public int Count => Statements.Length;

        Kind IObject.Kind => Kind.BlockStatement;
    }
}