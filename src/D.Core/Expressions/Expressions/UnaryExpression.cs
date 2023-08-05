namespace E.Expressions;

public sealed class UnaryExpression(Operator @operator, IExpression arg) : IExpression
{
    public Operator Operator { get; } = @operator;

    public IExpression Argument { get; } = arg;

    public override string ToString()
    {
        return $"{Operator}({Argument})";
    }

    public ObjectType Kind => Operator.OpKind;
}