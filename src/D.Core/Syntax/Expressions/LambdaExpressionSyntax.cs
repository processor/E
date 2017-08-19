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

        SyntaxKind SyntaxNode.Kind => SyntaxKind.LambdaExpression;
    }
}

