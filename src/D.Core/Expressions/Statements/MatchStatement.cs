using System.Collections.Generic;

namespace D.Expressions
{
    public class MatchExpression : IExpression
    {
        public MatchExpression(IExpression expression, MatchCase[] cases)
        {
            Expression = expression;
            Cases = cases;
        }

        public IExpression Expression { get; }

        public IList<MatchCase> Cases { get; }

        Kind IObject.Kind => Kind.MatchStatement;
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
