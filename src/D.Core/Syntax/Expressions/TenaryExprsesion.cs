namespace D.Syntax
{    
    public class TernaryExpressionSyntax : ISyntax
    {
        public TernaryExpressionSyntax(ISyntax condition, ISyntax left, ISyntax right)
        {
            Condition = condition;
            Left = left;
            Right = right;
        }

        public ISyntax Condition { get; }

        public ISyntax Left { get; }

        public ISyntax Right { get; }

        public Kind Kind => Kind.TernaryExpression;
    }
}
