using System;

namespace D.Expressions
{
    public class BlockExpression : IExpression
    {
        public BlockExpression(params IExpression[] statements)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }

        public IExpression[] Statements { get; }

        public IExpression this[int index] => Statements[index];

        public int Count => Statements.Length;

        Kind IObject.Kind => Kind.BlockExpression;
    }
}