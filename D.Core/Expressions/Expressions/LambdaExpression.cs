namespace D.Expressions
{
    // => ...
    public class LambdaExpression : IExpression
    {
        public LambdaExpression(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        Kind IObject.Kind => Kind.LambdaExpression;
    }
}

