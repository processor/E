using System.Text;

namespace D.Syntax
{
    public sealed class BinaryExpressionSyntax : ISyntaxNode
    {
        public BinaryExpressionSyntax(Operator op, ISyntaxNode lhs, ISyntaxNode rhs)
        {
            Operator = op;
            Left = lhs;
            Right = rhs;
        }

        public Operator Operator { get; }

        public ISyntaxNode Left { get; }

        public ISyntaxNode Right { get; }

        public bool IsParenthesized { get; set; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.BinaryExpression;

        #region ToString

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (IsParenthesized)
            {
                sb.Append('(');
            }

            sb.Append(Left.ToString());

            sb.Append(' ');
            sb.Append(Operator.Name);
            sb.Append(' ');

            sb.Append(Right.ToString());

            if (IsParenthesized)
            {
                sb.Append(')');
            }

            return sb.ToString();
        }

        #endregion
    }
}