namespace E.Syntax;

// [index]
public sealed class IndexAccessExpressionSyntax(
    ISyntaxNode left,
    ArgumentSyntax[] arguments) : ISyntaxNode
{
    public ISyntaxNode Left { get; } = left;

    // [1]
    // [1, 2]
    public ArgumentSyntax[] Arguments { get; } = arguments;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.IndexAccessExpression;
}