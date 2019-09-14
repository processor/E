namespace D.Syntax
{
    // $"{expression}text"

    public sealed class InterpolatedStringExpressionSyntax : ISyntaxNode
    {
        public InterpolatedStringExpressionSyntax(ISyntaxNode[] children)
        {
            Children = children;
        }

        public ISyntaxNode[] Children { get; }

        public ISyntaxNode this[int index] => Children[index];

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.InterpolatedStringExpression;
    }
}