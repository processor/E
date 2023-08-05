using E.Symbols;

namespace E.Expressions;

public sealed class TypeInitializer(
    TypeSymbol type,
    Argument[] arguments) : IExpression
{
    public TypeSymbol Type { get; } = type;

    public Argument[] Arguments { get; } = arguments;

    public int Count => Arguments.Length;

    ObjectType IObject.Kind => ObjectType.TypeInitializer;
}