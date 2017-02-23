using System;

namespace D.Expressions
{
    public class ReturnStatement : IExpression
    {
        public ReturnStatement(IExpression expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public IExpression Expression { get; }

        Kind IObject.Kind => Kind.ReturnStatement;
    }
}