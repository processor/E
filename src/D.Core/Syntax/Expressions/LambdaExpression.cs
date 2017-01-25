namespace D.Syntax
{
    // => ...
    public class LambdaExpressionSyntax : ISyntax
    {
        public LambdaExpressionSyntax(ISyntax expression)
        {
            Expression = expression;
        }

        public ISyntax Expression { get; }

        Kind IObject.Kind => Kind.LambdaExpression;
    }
}

