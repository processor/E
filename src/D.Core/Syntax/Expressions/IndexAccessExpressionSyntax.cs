using System.Collections.Generic;

namespace E.Syntax
{
    // [index]
    public sealed class IndexAccessExpressionSyntax : ISyntaxNode
    {
        public IndexAccessExpressionSyntax(ISyntaxNode left, IReadOnlyList<ArgumentSyntax> arguments)
        {
            Left = left;
            Arguments = arguments;
        }

        public ISyntaxNode Left { get; }

        // [1]
        // [1, 2]
        public IReadOnlyList<ArgumentSyntax> Arguments { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.IndexAccessExpression;
    }
}