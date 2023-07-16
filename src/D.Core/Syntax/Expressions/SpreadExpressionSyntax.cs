namespace E.Syntax;

public sealed class SpreadExpressionSyntax(ISyntaxNode symbol) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = symbol;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.SpreadStatement;
}