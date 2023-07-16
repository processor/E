using E.Symbols;

namespace E.Syntax;

public sealed class ParameterSyntax(
    Symbol name,
    Symbol? type = null,
    ISyntaxNode? defaultValue = null,
    ISyntaxNode? condition = null,
    AnnotationSyntax[]? annotations = null,
    int index = 0)
{
    public Symbol Name { get; } = name;

    public Symbol? Type { get; } = type;

    public int Index { get; } = index;

    public ISyntaxNode? DefaultValue { get; } = defaultValue;

    public ISyntaxNode? Condition { get; } = condition;

    public AnnotationSyntax[]? Annotations { get; } = annotations;
}