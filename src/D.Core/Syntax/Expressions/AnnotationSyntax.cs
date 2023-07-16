using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

public sealed class AnnotationSyntax(Symbol name, IReadOnlyList<ArgumentSyntax> arguments) : ISyntaxNode
{
    public Symbol Name { get; } = name;

    public IReadOnlyList<ArgumentSyntax> Arguments { get; } = arguments;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.AnnotationExpression;
}