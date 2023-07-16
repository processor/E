namespace E.Syntax;

public sealed class EmitStatementSyntax(ISyntaxNode expression) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = expression;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.EmitStatement;
}