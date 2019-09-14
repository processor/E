using System.Text;

namespace D.Syntax
{
    // .member
    public sealed class MemberAccessExpressionSyntax : ISyntaxNode
    {
        public MemberAccessExpressionSyntax(ISyntaxNode left, Symbol name)
        {
            Left = left;
            Name = name;
        }

        public ISyntaxNode Left { get; }

        // Property | Function
        public Symbol Name { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Left.ToString());

            sb.Append('.');
            sb.Append(Name);

            return sb.ToString();
        }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.MemberAccessExpression;
    }
}