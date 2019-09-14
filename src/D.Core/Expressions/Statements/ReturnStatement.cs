namespace D.Expressions
{
    public sealed class ReturnStatement : IExpression
    {
        public ReturnStatement(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        ObjectType IObject.Kind => ObjectType.ReturnStatement;
    }
}