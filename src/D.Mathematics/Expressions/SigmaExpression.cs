using D.Expressions;

namespace D.Mathematics
{
    public class SigmaExpression : IExpression
    {
        public SigmaExpression(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        Kind IObject.Kind => Kind.Sigma;
    }
}

// summation (capital Greek sigma symbol: ∑) is the addition of a sequence of numbers; 
// the result is their sum or total.