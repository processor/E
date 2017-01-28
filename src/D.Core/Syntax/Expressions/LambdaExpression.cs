namespace D.Syntax
{
    // => ...
    public class LambdaExpressionSyntax : SyntaxNode
    {
        public LambdaExpressionSyntax(SyntaxNode expression)
        {
            Expression = expression;
        }

        public SyntaxNode Expression { get; }

        Kind IObject.Kind => Kind.LambdaExpression;
    }
}

