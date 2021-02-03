namespace E.Syntax
{
    // .. Inclusive
    // ..< (Half Open)
   
    public sealed class RangeExpressionSyntax : ISyntaxNode
    {
        public RangeExpressionSyntax(ISyntaxNode start, ISyntaxNode end, RangeFlags flags)
        {
            Start = start;
            End   = end;
            Flags = flags;
        }

        public ISyntaxNode Start { get; }

        public ISyntaxNode End { get; }

        public RangeFlags Flags { get; }

        public bool IsInclusive => Flags.HasFlag(RangeFlags.Inclusive);

        public bool IsHalfOpen => Flags.HasFlag(RangeFlags.HalfOpen);

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.RangeLiteral;
    }
}