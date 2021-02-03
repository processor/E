using System.Text;

namespace E.Syntax
{
    public sealed class UnaryExpressionSyntax : ISyntaxNode
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

            sb.Append(Operator.ToString());

            sb.Append('(');

            sb.Append(Argument.ToString());

            sb.Append(')');

            return sb.ToString();
        }

        #endregion

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.FunctionDeclaration;
    }
}