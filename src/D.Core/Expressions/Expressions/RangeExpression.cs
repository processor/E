using System;

namespace D.Expressions
{
    public class RangeExpression : IExpression
    {
        public RangeExpression(IExpression start, IExpression end, RangeFlags flags)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End   = end ?? throw new ArgumentNullException(nameof(end));
            Flags = flags;
        }

        public IExpression Start { get; }

        // Step?

        public IExpression End { get; }

        public RangeFlags Flags { get; }

        Kind IObject.Kind => Kind.RangeLiteral;
    }
}