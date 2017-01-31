using System;

namespace D.Expressions
{
    public class MatchExpression : IExpression
    {
        public MatchExpression(IExpression expression, MatchCase[] cases)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            Cases      = cases      ?? throw new ArgumentNullException(nameof(cases)); ;
        }

        public IExpression Expression { get; }

        public MatchCase[] Cases { get; }

        Kind IObject.Kind => Kind.MatchExpression;
    }

    public class MatchCase
    {
        public MatchCase(IExpression pattern, IExpression condition, LambdaExpression body)
        {
            Pattern     = pattern;
            Condition   = condition;
            Body        = body;
        }
        
        public IExpression Pattern { get; }

        public IExpression Condition { get; }

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
