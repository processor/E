using System.Text;

namespace D.Syntax
{
    public class UnaryExpressionSyntax : ISyntax
    {
        public UnaryExpressionSyntax(Operator op, ISyntax arg)
        {
            Operator = op;
            Argument = arg;
        }

        // Change to symbol
        public Operator Operator { get; }

        public ISyntax Argument { get; }

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

        public Kind Kind => Operator.OpKind;
    }
}
