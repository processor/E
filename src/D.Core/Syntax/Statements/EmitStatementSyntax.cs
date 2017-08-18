namespace D.Syntax
{
    public class EmitStatementSyntax : SyntaxNode
    {
        public EmitStatementSyntax(SyntaxNode expression)
        {
            Expression = expression;
        }

        public SyntaxNode Expression { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.EmitStatement;
    }
}