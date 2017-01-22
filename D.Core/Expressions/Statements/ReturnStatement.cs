using System;

namespace D.Expressions
{
    public class ReturnStatement : IExpression
    {
        public ReturnStatement(IExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Expression = expression;
        }

        public IExpression Expression { get; }

        Kind IObject.Kind => Kind.ReturnStatement;
    }
}