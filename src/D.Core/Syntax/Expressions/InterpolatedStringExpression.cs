namespace E.Syntax;

// $"{expression}text"

public sealed class InterpolatedStringExpressionSyntax(ISyntaxNode[] children) : ISyntaxNode
{
    public ISyntaxNode[] Children { get; } = children;

    public ISyntaxNode this[int index] => Children[index];

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.InterpolatedStringExpression;
}