namespace D.Expressions
{
    public class SpreadExpression : IExpression
    {
        public SpreadExpression(IExpression symbol)
        {
            Expression = symbol;
        }

        public IExpression Expression { get; }

        Kind IObject.Kind => Kind.SpreadStatement;
    }
}