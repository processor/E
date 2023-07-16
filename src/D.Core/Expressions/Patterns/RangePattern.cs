namespace E.Expressions;

// 0...10
// 0..<10       // Half open
public sealed class RangePattern(IExpression start, IExpression end) : IExpression
{
    public IExpression Start { get; } = start;

    public IExpression End { get; } = end;

    ObjectType IObject.Kind => ObjectType.RangePattern;
}