namespace D.Syntax
{
    public class WhileStatementSyntax : ISyntax
    {
        public WhileStatementSyntax(ISyntax condition, BlockStatementSyntax body)
        {
            Condition = condition;
            Body = body;
        }

        public ISyntax Condition { get; }

        public BlockStatementSyntax Body { get; }

        Kind IObject.Kind => Kind.WhileStatement;
    }
}