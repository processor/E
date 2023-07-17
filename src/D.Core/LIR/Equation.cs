using E.Symbols;

namespace E.Expressions;

public sealed class Equation(
    IObject lhs,
    IObject rhs,
    Symbol[] symbols) : IExpression
{
    public IObject Left { get; } = lhs;

    public IObject Right { get; } = rhs;

    public Symbol[] Symbols { get; } = symbols;

    // TODO: Invariants
    // x > 10

    public ObjectType Kind => ObjectType.Equation;

    // TODO: Simplify()
}

// a + 5    =  a → a + 5
// a + 5^2  =  a → a^2

// A linear equation in one unknown x may always be rewritten
// If a ≠ 0, there is a unique solution

// Linear equation
// y = 5 + x

// differential equation
// A differential equation is any equation which contains derivatives, either ordinary derivatives or partial derivatives.