namespace D.Syntax
{
    // [index]
    public sealed class IndexAccessExpressionSyntax : ISyntaxNode
    {
        public IndexAccessExpressionSyntax(ISyntaxNode left, ArgumentSyntax[] arguments)
        {
            Left = left;
            Arguments = arguments;
        }

        public ISyntaxNode Left { get; }

        // [1]
        // [1, 2]
        public ArgumentSyntax[] Arguments { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.IndexAccessExpression;
    }
}