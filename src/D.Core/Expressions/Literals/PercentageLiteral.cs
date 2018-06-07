namespace D.Expressions
{
    public readonly struct PercentageLiteral : IExpression
    {
        public PercentageLiteral(double value)
        {
            Value = value;
        }

        public double Value { get; }

        Kind IObject.Kind => Kind.Percentage;

        public override string ToString() => Value + "%";
    }
}