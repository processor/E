namespace E.Syntax;

public sealed class EmitStatementSyntax : ISyntaxNode
{
    public EmitStatementSyntax(ISyntaxNode expression)
    {
        Expression = expression;
    }

    public ISyntaxNode Expression { get; }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.EmitStatement;
}