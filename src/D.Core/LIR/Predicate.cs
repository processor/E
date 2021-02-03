using System.Text;

using E.Symbols;

namespace E
{
    // x > 10
    // x < 10

    public sealed class Predicate : IObject
    {
        public Predicate(Operator op, Symbol left, IObject right)
        {
            Operator = op;
            Left = left;
            Right = right;
        }

        public Operator Operator { get; }

        public Symbol Left { get; }

        public IObject Right { get; }

        ObjectType IObject.Kind => ObjectType.Predicate;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Left.ToString());
            sb.Append(' ');
            sb.Append(Operator.Name);
            sb.Append(' ');
            sb.Append(Right.ToString());

            return sb.ToString();
        }
    }

    //  {x | x is a positive integer less than 4} is the set {1,2,3}.

    // x : Integer where value > 0 && value < 4,
}