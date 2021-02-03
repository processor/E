using System.Text;

using E.Symbols;

namespace E.Expressions
{
    // .member
    public sealed class MemberAccessExpression : IExpression
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

            sb.Append('.');
            sb.Append(MemberName);

            return sb.ToString();
        }

        ObjectType IObject.Kind => ObjectType.MemberAccessExpression;
    }
}