namespace D.Expressions
{
    public class ConstantExpression : IExpression
    {
        public ConstantExpression(object value)
        {
            Value = value;
        }

        public object Value { get; }

        Kind IObject.Kind => Kind.ConstantExpression;
    }
}
