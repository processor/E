using System;
using System.Text;

namespace D.Syntax
{
    public class BinaryExpressionSyntax : SyntaxNode
    {
        public BinaryExpressionSyntax(Operator op, SyntaxNode lhs, SyntaxNode rhs)
        {
            Operator = op;
            Left     = lhs ?? throw new ArgumentNullException(nameof(lhs));
            Right    = rhs ?? throw new ArgumentNullException(nameof(rhs));
        }

        public Operator Operator { get; }

        public SyntaxNode Left { get; }

        public SyntaxNode Right { get; }

        public bool Grouped { get; set; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.BinaryExpression;

        #region ToString

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Grouped)
            {
                sb.Append("(");
            }

            sb.Append(Left.ToString());

            sb.Append(" ");
            sb.Append(Operator.Name);
            sb.Append(" ");

            sb.Append(Right.ToString());

            if (Grouped)
            {
                sb.Append(")");
            }

            return sb.ToString();
        }

        #endregion
    }
}