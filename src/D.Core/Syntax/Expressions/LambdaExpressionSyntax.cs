using System;

namespace D.Syntax
{
    // => ...
    public class LambdaExpressionSyntax : ISyntaxNode
    {
        public LambdaExpressionSyntax(ISyntaxNode expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public ISyntaxNode Expression { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.LambdaExpression;
    }
}