using System;

namespace D.Syntax
{
    public class BlockExpressionSyntax : ISyntax
    {
        public BlockExpressionSyntax(params ISyntax[] statements)
        {
            #region Preconditions

            if (statements == null)
                throw new ArgumentNullException(nameof(statements));

            #endregion

            Statements = statements;
        }

        public ISyntax[] Statements { get; }

        public ISyntax this[int index] => Statements[index];

        public int Count => Statements.Length;

        Kind IObject.Kind => Kind.BlockExpression;
    }
}