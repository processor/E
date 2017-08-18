namespace D.Syntax
{
    // $"{expression}text"

    public class InterpolatedStringExpressionSyntax : SyntaxNode
    {
        public InterpolatedStringExpressionSyntax(SyntaxNode[] children)
        {
            Children = children;
        }

        public SyntaxNode[] Children { get; }

        public SyntaxNode this[int index] => Children[index];

        SyntaxKind SyntaxNode.Kind => SyntaxKind.InterpolatedStringExpression;
    }
}