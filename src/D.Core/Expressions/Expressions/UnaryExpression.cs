namespace E.Expressions;

public sealed class UnaryExpression(Operator op, IExpression arg) : IExpression
{
    public Operator Operator { get; } = op;

    public IExpression Argument { get; } = arg;

    public override string ToString()
    {
        return $"{Operator}({Argument})";
    }

    public ObjectType Kind => Operator.OpKind;
}