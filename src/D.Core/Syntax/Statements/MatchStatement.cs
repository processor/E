using System;
using System.Collections.Generic;

namespace D.Syntax
{
    public class MatchExpressionSyntax : SyntaxNode
    {
        public MatchExpressionSyntax(SyntaxNode expression, IList<MatchCaseSyntax> cases)
        {
            #region Preconditions

            if (cases == null)
                throw new ArgumentNullException(nameof(cases));

            #endregion

            Expression = expression;
            Cases = cases;
        }

        public SyntaxNode Expression { get; }

        public IList<MatchCaseSyntax> Cases { get; }

        Kind IObject.Kind => Kind.MatchExpression;
    }

    public class MatchCaseSyntax
    {
        public MatchCaseSyntax(SyntaxNode pattern, SyntaxNode condition, LambdaExpressionSyntax body)
        {
            Pattern     = pattern;
            Condition   = condition;
            Body        = body;
        }
        
        public SyntaxNode Pattern { get; }

        public SyntaxNode Condition { get; }

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
