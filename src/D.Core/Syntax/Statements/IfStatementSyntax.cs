namespace E.Syntax;

public sealed class IfStatementSyntax(
    ISyntaxNode condition,
    BlockSyntax body,
    ISyntaxNode? elseBranch) : ISyntaxNode
{
    public ISyntaxNode Condition { get; } = condition;

    public BlockSyntax Body { get; } = body;

    // Else | ElseIf
    public ISyntaxNode? ElseBranch { get; } = elseBranch;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.IfStatement;
}
