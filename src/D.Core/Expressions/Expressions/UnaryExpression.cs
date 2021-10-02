namespace E.Expressions;

public sealed class UnaryExpression : IExpression
{
    public UnaryExpression(Operator op, IExpression arg)
    {
        Operator = op;
        Argument = arg;
    }

    public Operator Operator { get; }

    public IExpression Argument { get; }

    public override string ToString()
    {
        return $"{Operator}({Argument})";
    }

    public ObjectType Kind => Operator.OpKind;
}