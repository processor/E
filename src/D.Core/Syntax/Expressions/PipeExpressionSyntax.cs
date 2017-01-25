namespace D.Syntax
{
    // TODO: Merge with call operator...

    public class PipeStatementSyntax : ISyntax
    {
        public PipeStatementSyntax(ISyntax callee, ISyntax expression)
        {
            Callee = callee;
            Expression = expression;
        }

        public ISyntax Callee { get; }

        // CallExpression | MatchStatement
        public ISyntax Expression { get; }

        public Kind Kind => Kind.PipeStatement;
    }
}
