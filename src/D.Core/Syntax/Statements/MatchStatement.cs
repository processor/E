using System;
using System.Collections.Generic;

namespace D.Syntax
{
    public class MatchExpressionSyntax : ISyntax
    {
        public MatchExpressionSyntax(ISyntax expression, IList<MatchCaseSyntax> cases)
        {
            #region Preconditions

            if (cases == null)
                throw new ArgumentNullException(nameof(cases));

            #endregion

            Expression = expression;
            Cases = cases;
        }

        public ISyntax Expression { get; }

        public IList<MatchCaseSyntax> Cases { get; }

        Kind IObject.Kind => Kind.MatchStatement;
    }

    public class MatchCaseSyntax
    {
        public MatchCaseSyntax(ISyntax pattern, ISyntax condition, LambdaExpressionSyntax body)
        {
            Pattern     = pattern;
            Condition   = condition;
            Body        = body;
        }
        
        public ISyntax Pattern { get; }

        public ISyntax Condition { get; }

        public LambdaExpressionSyntax Body { get; }
    }
}

/*
switch expression { 
    case pattern => body
    case pattern when condition => body
    case pattern => { 

    }
*/
