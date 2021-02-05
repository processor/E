namespace E.Expressions
{
    // 0...10
    // 0..<10       // Half open
    public sealed class RangePattern : IExpression
    {
        public RangePattern(IExpression start, IExpression end)
        {
            Start = start;
            End   = end;
        }

        public IExpression Start { get; }

        public IExpression End { get; }

        ObjectType IObject.Kind => ObjectType.RangePattern;
    }
}