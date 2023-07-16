using E.Symbols;

namespace E.Expressions;

// (fruit: Fruit)
// Fruit | Walrus

public sealed class TypePattern(Symbol typeExpression, Symbol variable) : IExpression
{
    public IExpression TypeExpression { get; } = typeExpression;

    public Symbol VariableName { get; } = variable;

    ObjectType IObject.Kind => ObjectType.TypePattern;
}