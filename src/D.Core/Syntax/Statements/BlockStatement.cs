using System;

namespace D.Syntax
{
    public class BlockExpressionSyntax : SyntaxNode
    {
        public BlockExpressionSyntax(params SyntaxNode[] statements)
        {
            #region Preconditions

            if (statements == null)
                throw new ArgumentNullException(nameof(statements));

            #endregion

            Statements = statements;
        }

        public SyntaxNode[] Statements { get; }

        public SyntaxNode this[int index] => Statements[index];

        public int Count => Statements.Length;

        Kind IObject.Kind => Kind.BlockStatement;
    }
}