using System;
using System.Text;

namespace D.Syntax
{
    public class BinaryExpressionSyntax : ISyntaxNode
    {
        public BinaryExpressionSyntax(Operator op, ISyntaxNode lhs, ISyntaxNode rhs)
        {
            Operator = op;
            Left     = lhs ?? throw new ArgumentNullException(nameof(lhs));
            Right    = rhs ?? throw new ArgumentNullException(nameof(rhs));
        }

        public Operator Operator { get; }

        public ISyntaxNode Left { get; }

        public ISyntaxNode Right { get; }

        public bool Parenthesized { get; set; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.BinaryExpression;

        #region ToString

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Parenthesized)
            {
                sb.Append('(');
            }

            sb.Append(Left.ToString());

            sb.Append(' ');
            sb.Append(Operator.Name);
            sb.Append(' ');

            sb.Append(Right.ToString());

            if (Parenthesized)
            {
                sb.Append(')');
            }

            return sb.ToString();
        }

        #endregion
    }
}