namespace D.Syntax
{
    // .. Inclusive
    // ..< (Half Open)
   
    public class RangeExpression : SyntaxNode
    {
        public RangeExpression(SyntaxNode start, SyntaxNode end, RangeFlags flags)
        {
            Start = start;
            End = end;
            Flags = flags;
        }

        public SyntaxNode Start { get; }

        public SyntaxNode End { get; }

        public RangeFlags Flags { get; }

        public bool IsInclusive => Flags.HasFlag(RangeFlags.Inclusive);

        public bool IsHalfOpen => Flags.HasFlag(RangeFlags.HalfOpen);

        SyntaxKind SyntaxNode.Kind => SyntaxKind.RangeLiteral;
    }
}