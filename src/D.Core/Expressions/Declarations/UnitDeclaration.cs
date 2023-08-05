using E.Symbols;

namespace E.Expressions;

public sealed class UnitDeclaration(
    Symbol name,
    Symbol baseType,
    IExpression expression) : IExpression
{
    public Symbol  Name     { get; } = name;
    public Symbol  BaseType { get; } = baseType;
    public Symbol? Symbol   { get; }

    // Arithmetic relationship to another unit

    // ml = cm**3

    public IExpression Expression { get; } = expression;

    ObjectType IObject.Kind => ObjectType.UnitDeclaration;
}

// rad unit : Angle @name("Radian") = 1
