using E.Expressions;

namespace E.Mathematics;

// An integral assigns numbers to functions in a way that can describe displacement,
// area, volume, and other concepts that arise by combining infinitesimal data. I

public sealed class IntegralExpression(
    IExpression expression,
    IExpression a,
    IExpression b,
    IExpression x) : IExpression
{
    public IExpression Expression { get; } = expression;

    public IExpression A { get; } = a;

    public IExpression B { get; } = b;

    public IExpression X { get; } = x;

    ObjectType IObject.Kind => ObjectType.Integral;
}