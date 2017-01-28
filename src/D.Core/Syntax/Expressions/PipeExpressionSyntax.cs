namespace D.Syntax
{
    // TODO: Merge with call operator...

    public class PipeStatementSyntax : SyntaxNode
    {
        public PipeStatementSyntax(SyntaxNode callee, SyntaxNode expression)
        {
            Callee = callee;
            Expression = expression;
        }

        public SyntaxNode Callee { get; }

        // CallExpression | MatchStatement
        public SyntaxNode Expression { get; }

        public Kind Kind => Kind.PipeStatement;
    }
}
