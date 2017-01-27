namespace D.Expressions
{
    public class ElseIfStatement : IExpression
    {
        public ElseIfStatement(IExpression condition, BlockExpression body, IExpression elseBranch)
        {
            Condition = condition;
            Body = body;
            ElseBranch = elseBranch;
        }

        public IExpression Condition { get; }

        public BlockExpression Body { get; }

        // Else, ElseIf
        public IExpression ElseBranch { get; }

        Kind IObject.Kind => Kind.ElseIfStatement;
    }
}