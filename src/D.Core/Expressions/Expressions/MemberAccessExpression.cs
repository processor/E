using System.Text;

namespace D.Expressions
{
    // .member
    public class MemberAccessExpression : IExpression
    {
        public MemberAccessExpression(IExpression left, Symbol memberName)
        {
            Left = left;
            MemberName = memberName;
        }

        // Type: Array | Property
        public IExpression Left { get; }

        // The member
        public Symbol MemberName { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Left.ToString());

            sb.Append(".");
            sb.Append(MemberName);

            return sb.ToString();
        }

        Kind IObject.Kind => Kind.MemberAccessExpression;
    }
}