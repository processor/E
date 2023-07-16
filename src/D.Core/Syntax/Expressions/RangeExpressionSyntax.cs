namespace E.Syntax;

// .. Inclusive
// ..< (Half Open)

public sealed class RangeExpressionSyntax(
    ISyntaxNode start,
    ISyntaxNode end,
    RangeFlags flags) : ISyntaxNode
{
    public ISyntaxNode Start { get; } = start;

    public ISyntaxNode End { get; } = end;

    public RangeFlags Flags { get; } = flags;

    public bool IsInclusive => Flags.HasFlag(RangeFlags.Inclusive);

    public bool IsHalfOpen => Flags.HasFlag(RangeFlags.HalfOpen);

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.RangeLiteral;
}