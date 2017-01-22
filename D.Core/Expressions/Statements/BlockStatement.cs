﻿using System;

namespace D.Expressions
{
    public class BlockStatement : IExpression
    {
        public BlockStatement(params IExpression[] statements)
        {
            #region Preconditions

            if (statements == null)
                throw new ArgumentNullException(nameof(statements));

            #endregion

            Statements = statements;
        }

        public IExpression[] Statements { get; }

        public IExpression this[int index] => Statements[index];

        public int Count => Statements.Length;

        Kind IObject.Kind => Kind.BlockStatement;
    }
}