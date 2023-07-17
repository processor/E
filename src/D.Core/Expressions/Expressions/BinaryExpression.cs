using System.Text;

namespace E.Expressions;

public class BinaryExpression(
    Operator op,
    IObject lhs,
    IObject rhs) : IExpression
{
    public Operator Operator { get; } = op;

    public IObject Left { get; } = lhs;

    public IObject Right { get; } = rhs;

    public bool Grouped { get; set; }

    public ObjectType Kind => Operator.OpKind;

    #region ToString

    public override string ToString()
    {
        var sb = new ValueStringBuilder(128);

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
