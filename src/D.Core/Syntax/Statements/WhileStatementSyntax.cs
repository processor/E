namespace E.Syntax;

public sealed class WhileStatementSyntax(
    ISyntaxNode condition,
    BlockSyntax body) : ISyntaxNode
{
    public ISyntaxNode Condition { get; } = condition;

    public BlockSyntax Body { get; } = body;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.WhileStatement;
}