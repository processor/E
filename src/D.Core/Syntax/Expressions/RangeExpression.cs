namespace D.Syntax
{
    // ... Inclusive
    public class RangeExpression : ISyntax
    {
        public RangeExpression(ISyntax start, ISyntax end)
        {
            Start = start;
            End = end;
        }

        public ISyntax Start { get; }

        public ISyntax End { get; }

        Kind IObject.Kind => Kind.RangeLiteral;
    }

    // ..<
    public class HalfOpenRangeExpression : ISyntax
    {
        public HalfOpenRangeExpression(ISyntax start, ISyntax end)
        {
            Start = start;
            End = end;
        }

        public ISyntax Start { get; }

        public ISyntax End { get; }

        Kind IObject.Kind => Kind.HalfOpenRange;
    }
}