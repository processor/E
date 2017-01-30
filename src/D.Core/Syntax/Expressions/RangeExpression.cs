namespace D.Syntax
{
    // .. Inclusive
    public class RangeExpression : SyntaxNode
    {
        public RangeExpression(SyntaxNode start, SyntaxNode end)
        {
            Start = start;
            End = end;
        }

        public SyntaxNode Start { get; }

        public SyntaxNode End { get; }
        
        Kind IObject.Kind => Kind.RangeLiteral;
    }

    // ..<
    public class HalfOpenRangeExpression : SyntaxNode
    {
        public HalfOpenRangeExpression(SyntaxNode start, SyntaxNode end)
        {
            Start = start;
            End = end;
        }

        public SyntaxNode Start { get; }

        public SyntaxNode End { get; }

        Kind IObject.Kind => Kind.HalfOpenRange;
    }
}