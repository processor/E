namespace D.Syntax
{
    // => ...
    public sealed class LambdaExpressionSyntax : ISyntaxNode
    {
        public LambdaExpressionSyntax(ISyntaxNode expression)
        {
            Expression = expression;
        }

        public ISyntaxNode Expression { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.LambdaExpression;
    }
}