namespace D.Expressions
{
    public class WhileStatement : IExpression
    {
        public WhileStatement(IExpression condition, BlockStatement body)
        {
            Condition = condition;
            Body = body;
        }

        public IExpression Condition { get; }

        public BlockStatement Body { get; }

        Kind IObject.Kind => Kind.WhileStatement;
    }
}