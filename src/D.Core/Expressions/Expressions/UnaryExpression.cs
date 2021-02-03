using System.Text;

namespace E.Expressions
{
    public sealed class UnaryExpression : IExpression
    {
        public UnaryExpression(Operator op, IExpression arg)
        {
            Operator = op;
            Argument = arg;
        }

        public Operator Operator { get; }

        public IExpression Argument { get; }

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

        public ObjectType Kind => Operator.OpKind;
    }
}
