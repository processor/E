namespace D.Expressions
{
    public sealed class SpreadExpression : IExpression
    {
        public SpreadExpression(IExpression symbol)
        {
            Expression = symbol;
        }

        public IExpression Expression { get; }

        ObjectType IObject.Kind => ObjectType.SpreadStatement;
    }
}