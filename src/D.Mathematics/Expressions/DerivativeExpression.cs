using D.Expressions;

namespace D.Mathematics
{
    public class DerivativeExpression : IExpression
    {
        public DerivativeExpression(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        ObjectType IObject.Kind => ObjectType.Derivative;
    }
}