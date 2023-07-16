namespace E.Syntax;

public sealed class ReturnStatementSyntax(ISyntaxNode expression) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = expression;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ReturnStatement;
}