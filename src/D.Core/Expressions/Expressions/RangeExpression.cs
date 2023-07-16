namespace E.Expressions;

public sealed class RangeExpression(
    IExpression start,
    IExpression end,
    RangeFlags flags) : IExpression
{
    public IExpression Start { get; } = start;

    // Step?

    public IExpression End { get; } = end;

    // Inclusive | Exclusive

    public RangeFlags Flags { get; } = flags;

    ObjectType IObject.Kind => ObjectType.RangeLiteral;
}