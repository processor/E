namespace D.Syntax
{
    public class WhileStatementSyntax : ISyntax
    {
        public WhileStatementSyntax(ISyntax condition, BlockExpressionSyntax body)
        {
            Condition = condition;
            Body = body;
        }

        public ISyntax Condition { get; }

        public BlockExpressionSyntax Body { get; }

        Kind IObject.Kind => Kind.LoopExpression;
    }
}