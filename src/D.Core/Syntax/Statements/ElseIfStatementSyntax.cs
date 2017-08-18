namespace D.Syntax
{
    public class ElseIfStatementSyntax : SyntaxNode
    {
        public ElseIfStatementSyntax(SyntaxNode condition, BlockSyntax body, SyntaxNode elseBranch)
        {
            Condition = condition;
            Body = body;
            ElseBranch = elseBranch;
        }

        public SyntaxNode Condition { get; }

        public BlockSyntax Body { get; }

        // Else, ElseIf
        public SyntaxNode ElseBranch { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ElseIfStatement;
    }
}