namespace D.Expressions
{
    public class ConstantExpression : IExpression
    {
        public ConstantExpression(object value)
        {
            Value = value;
        }

        public object Value { get; }

        ObjectType IObject.Kind => ObjectType.ConstantExpression;
    }
}
