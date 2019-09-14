namespace D.Syntax
{
    public sealed class ElseIfStatementSyntax : ISyntaxNode
    {
        public ElseIfStatementSyntax(ISyntaxNode condition, BlockSyntax body, ISyntaxNode? elseBranch)
        {
            Condition = condition;
            Body = body;
            ElseBranch = elseBranch;
        }

        public ISyntaxNode Condition { get; }

        public BlockSyntax Body { get; }

        // Else, ElseIf
        public ISyntaxNode? ElseBranch { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ElseIfStatement;
    }
}