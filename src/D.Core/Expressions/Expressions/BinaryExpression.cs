using System.Text;

namespace E.Expressions;

public class BinaryExpression : IExpression
{
    public BinaryExpression(Operator op, IObject lhs, IObject rhs)
    {
        Operator = op;
        Left = lhs;
        Right = rhs;
    }

    public Operator Operator { get; }

    public IObject Left { get; }

    public IObject Right { get; }

    public bool Grouped { get; set; }

    public ObjectType Kind => Operator.OpKind;

    #region ToString

    public override string ToString()
    {
        var sb = new StringBuilder();

        if (Grouped)
        {
            sb.Append('(');
        }

        sb.Append(Left.ToString());

        sb.Append(' ');
        sb.Append(Operator.Name);
        sb.Append(' ');

        sb.Append(Right.ToString());

        if (Grouped)
        {
            sb.Append(')');
        }

        return sb.ToString();
    }

    #endregion
}
