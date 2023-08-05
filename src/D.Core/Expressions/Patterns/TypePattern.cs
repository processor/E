using E.Symbols;

namespace E.Expressions;

public sealed class TypePattern(Symbol typeExpression, Symbol variable) : IExpression
{
    public IExpression TypeExpression { get; } = typeExpression;

    public Symbol VariableName { get; } = variable;

    ObjectType IObject.Kind => ObjectType.TypePattern;
}

// EXAMPLES | 
// (fruit: Fruit)
// Fruit | Walrus