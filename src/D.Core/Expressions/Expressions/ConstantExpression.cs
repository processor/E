namespace D.Expressions
{
    public sealed class ConstantExpression : IExpression
    {
        public ConstantExpression(object value)
        {
            Value = value;
        }

        public object Value { get; }

        ObjectType IObject.Kind => ObjectType.ConstantExpression;
    }
}
