namespace E.Expressions;

public sealed class RangeExpression : IExpression
{
    public RangeExpression(
        IExpression start,
        IExpression end,
        RangeFlags flags)
    {
        Start = start;
        End = end;
        Flags = flags;
    }

    public IExpression Start { get; }

    // Step?

    public IExpression End { get; }

    // Inclusive | Exclusive

    public RangeFlags Flags { get; }

    ObjectType IObject.Kind => ObjectType.RangeLiteral;
}