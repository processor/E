using System.Text;

namespace D.Syntax
{
    public class UnaryExpressionSyntax : ISyntaxNode
    {
        public UnaryExpressionSyntax(Operator op, ISyntaxNode arg)
        {
            Operator = op;
            Argument = arg;
        }

        // Change to symbol
        public Operator Operator { get; }

        public ISyntaxNode Argument { get; }

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

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.FunctionDeclaration;
    }
}