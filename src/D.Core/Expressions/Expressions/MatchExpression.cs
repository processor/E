namespace E.Expressions
{
    public sealed class MatchExpression : IExpression
    {
        public MatchExpression(IExpression expression, MatchCase[] cases)
        {
            Expression = expression;
            Cases = cases;
        }

        public IExpression Expression { get; }

        public MatchCase[] Cases { get; }

        ObjectType IObject.Kind => ObjectType.MatchExpression;
    }

    public sealed class MatchCase
    {
        public MatchCase(
            IExpression pattern,
            IExpression? condition,
            LambdaExpression body)
        {
            Pattern     = pattern;
            Condition   = condition;
            Body        = body;
        }
        
        public IExpression Pattern { get; }

        public IExpression? Condition { get; }

        public LambdaExpression Body { get; }
    }
}

/*
switch expression { 
    case pattern => body
    case pattern when condition => body
    case pattern => { 

    }
*/
