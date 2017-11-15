using System;

namespace D.Syntax
{
    // [index]
    public class IndexAccessExpressionSyntax : ISyntaxNode
    {
        public IndexAccessExpressionSyntax(ISyntaxNode left, ArgumentSyntax[] arguments)
        {
            Left = left;
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public ISyntaxNode Left { get; }

        // [1]
        // [1, 2]
        public ArgumentSyntax[] Arguments { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.IndexAccessExpression;
    }
}