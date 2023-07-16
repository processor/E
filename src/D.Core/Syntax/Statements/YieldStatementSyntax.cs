namespace E.Syntax;

public sealed class YieldStatementSyntax(ISyntaxNode expression) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = expression;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.YieldStatement;
}