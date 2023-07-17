using E.Symbols;

namespace E.Expressions;

public sealed class AnnotationExpression(
    Symbol name,
    IArguments arguments) : IExpression
{
    public Symbol Name { get; } = name;

    public IArguments Arguments { get; } = arguments;

    ObjectType IObject.Kind => ObjectType.AnnotationExpression;
}