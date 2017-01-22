namespace D.Expressions
{
    // ... Inclusive
    public class RangeExpression : IExpression
    {
        public RangeExpression(IExpression start, IExpression end)
        {
            Start = start;
            End = end;
        }

        public IExpression Start { get; }

        public IExpression End { get; }

        Kind IObject.Kind => Kind.RangeLiteral;
    }

    // ..<
    public class HalfOpenRangeExpression : IExpression
    {
        public HalfOpenRangeExpression(IExpression start, IExpression end)
        {
            Start = start;
            End = end;
        }

        public IExpression Start { get; }

        public IExpression End { get; }

        Kind IObject.Kind => Kind.HalfOpenRange;
    }
}