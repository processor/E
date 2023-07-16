using System.Text;

namespace E.Syntax;

public sealed class BinaryExpressionSyntax(Operator op, ISyntaxNode lhs, ISyntaxNode rhs) : ISyntaxNode
{
    public Operator Operator { get; } = op;

    public ISyntaxNode Left { get; } = lhs;

    public ISyntaxNode Right { get; } = rhs;

    public bool IsParenthesized { get; set; }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.BinaryExpression;

    #region ToString

    public override string ToString()
    {
        var sb = new ValueStringBuilder(128);

        if (IsParenthesized)
        {
            sb.Append('(');
        }

        sb.Append(Left.ToString());

        sb.Append(' ');
        sb.Append(Operator.Name);
        sb.Append(' ');

        sb.Append(Right.ToString());

        if (IsParenthesized)
        {
            sb.Append(')');
        }

        return sb.ToString();
    }

    #endregion
}