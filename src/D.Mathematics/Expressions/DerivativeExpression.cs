using E.Expressions;

namespace E.Mathematics;

public class DerivativeExpression(IExpression expression) : IExpression
{
    public IExpression Expression { get; } = expression;

    ObjectType IObject.Kind => ObjectType.Derivative;
}