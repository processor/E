namespace E.Expressions
{
    public sealed class IfStatement : IExpression
    {
        public IfStatement(IExpression condition, BlockExpression body, IExpression? elseBranch)
        {
            Condition = condition;
            Body = body;
            ElseBranch = elseBranch;
        }

        public IExpression Condition { get; }

        public BlockExpression Body { get; }

        // Else | ElseIf
        public IExpression? ElseBranch { get; }

        ObjectType IObject.Kind => ObjectType.IfStatement;
    }
}