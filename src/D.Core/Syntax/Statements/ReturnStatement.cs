using System;

namespace D.Syntax
{
    public class ReturnStatementSyntax : ISyntax
    {
        public ReturnStatementSyntax(ISyntax expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Expression = expression;
        }

        public ISyntax Expression { get; }

        Kind IObject.Kind => Kind.ReturnStatement;
    }
}