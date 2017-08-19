using System;

namespace D.Syntax
{
    // => ...
    public class LambdaExpressionSyntax : SyntaxNode
    {
        public LambdaExpressionSyntax(SyntaxNode expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public SyntaxNode Expression { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.LambdaExpression;
    }
}