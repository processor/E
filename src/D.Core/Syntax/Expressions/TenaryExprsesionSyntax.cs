namespace E.Syntax;

public sealed class TernaryExpressionSyntax(
    ISyntaxNode condition,
    ISyntaxNode left,
    ISyntaxNode right) : ISyntaxNode
{
    public ISyntaxNode Condition { get; } = condition;

    public ISyntaxNode Left { get; } = left;

    public ISyntaxNode Right { get; } = right;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.TernaryExpression;
}