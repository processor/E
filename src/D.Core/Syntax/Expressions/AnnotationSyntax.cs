using E.Symbols;

namespace E.Syntax;

public sealed class AnnotationSyntax(Symbol name, ArgumentSyntax[] arguments) : ISyntaxNode
{
    public Symbol Name { get; } = name;

    public ArgumentSyntax[] Arguments { get; } = arguments;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.AnnotationExpression;
}