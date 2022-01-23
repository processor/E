using E.Expressions;

namespace E.Mathematics;

public sealed class SigmaExpression : IExpression
{
    public SigmaExpression(IExpression expression)
    {
        Expression = expression;
    }

    public IExpression Expression { get; }

    ObjectType IObject.Kind => ObjectType.Sigma;
}

// summation (capital Greek sigma symbol: ∑) is the addition of a sequence of numbers; 
// the result is their sum or total.