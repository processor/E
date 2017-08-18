using System.Text;

namespace D.Syntax
{
    public class UnaryExpressionSyntax : SyntaxNode
    {
        public UnaryExpressionSyntax(Operator op, SyntaxNode arg)
        {
            Operator = op;
            Argument = arg;
        }

        // Change to symbol
        public Operator Operator { get; }

        public SyntaxNode Argument { get; }

        #region ToString

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Operator.ToString().ToLower());

            sb.Append("(");

            sb.Append(Argument.ToString());

            sb.Append(")");

            return sb.ToString();
        }

        #endregion

        SyntaxKind SyntaxNode.Kind => SyntaxKind.FunctionDeclaration;
    }
}
