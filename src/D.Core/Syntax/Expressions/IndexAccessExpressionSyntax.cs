using System.Collections.Generic;

namespace E.Syntax;

// [index]
public sealed class IndexAccessExpressionSyntax(
    ISyntaxNode left,
    IReadOnlyList<ArgumentSyntax> arguments) : ISyntaxNode
{
    public ISyntaxNode Left { get; } = left;

    // [1]
    // [1, 2]
    public IReadOnlyList<ArgumentSyntax> Arguments { get; } = arguments;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.IndexAccessExpression;
}