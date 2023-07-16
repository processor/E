namespace E.Expressions;

public sealed class ConstantExpression(object value) : IExpression
{
    public object Value { get; } = value;

    ObjectType IObject.Kind => ObjectType.ConstantExpression;
}