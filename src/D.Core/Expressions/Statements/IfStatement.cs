namespace D.Expressions
{
    public class IfStatement : IExpression
    {
        public IfStatement(IExpression condition, BlockStatement body, IExpression elseBranch)
        {
            Condition = condition;
            Body = body;
            ElseBranch = elseBranch;
        }

        public IExpression Condition { get; }

        public BlockStatement Body { get; }

        // Else | ElseIf
        public IExpression ElseBranch { get; }

        Kind IObject.Kind => Kind.IfStatement;
    }
}