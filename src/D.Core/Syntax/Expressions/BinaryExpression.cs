using System.Text;

namespace D.Syntax
{
    public class BinaryExpressionSyntax : ISyntax
    {
        public BinaryExpressionSyntax(Operator op, ISyntax left, ISyntax right)
        {
            Operator = op;
            Left = left;
            Right = right;
        }

        public Operator Operator { get; }

        public ISyntax Left { get; }

        public ISyntax Right { get; }

        public bool Grouped { get; set; }

        public Kind Kind => Operator.OpKind;

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

    // may have been expanded from: apply then Assign
    
}
