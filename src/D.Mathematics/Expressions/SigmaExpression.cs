using E.Expressions;

namespace E.Mathematics;

public sealed class SigmaExpression(IExpression expression) : IExpression
{
    public IExpression Expression { get; } = expression;

    ObjectType IObject.Kind => ObjectType.Sigma;
}

// summation (capital Greek sigma symbol: ∑) is the addition of a sequence of numbers; 
// the result is their sum or total.