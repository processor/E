namespace D.Expressions
{    
    public sealed class TernaryExpression : IExpression
    {
        public TernaryExpression(
            IExpression condition,
            IExpression left, 
            IExpression right)
        {
            Condition = condition;
            Left = left;
            Right = right;
        }

        public IExpression Condition { get; }

        public IExpression Left { get; }

        public IExpression Right { get; }

        public ObjectType Kind => ObjectType.TernaryExpression;
    }
}
