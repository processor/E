namespace E.Syntax;

public sealed class ReturnStatementSyntax : ISyntaxNode
{
    public ReturnStatementSyntax(ISyntaxNode expression)
    {
        Expression = expression;
    }

    public ISyntaxNode Expression { get; }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ReturnStatement;
}