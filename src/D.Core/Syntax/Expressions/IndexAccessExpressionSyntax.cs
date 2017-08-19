using System;

namespace D.Syntax
{
    // [index]
    public class IndexAccessExpressionSyntax : SyntaxNode
    {
        public IndexAccessExpressionSyntax(SyntaxNode left, ArgumentSyntax[] arguments)
        {
            Left = left;
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public SyntaxNode Left { get; }

        // [1]
        // [1, 2]
        public ArgumentSyntax[] Arguments { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.IndexAccessExpression;
    }
}