using System;

namespace D.Syntax
{
    public class MatchExpressionSyntax : SyntaxNode
    {
        public MatchExpressionSyntax(SyntaxNode expression, MatchCaseSyntax[] cases)
        {
            Expression = expression;
            Cases = cases ?? throw new ArgumentNullException(nameof(cases));
        }

        public SyntaxNode Expression { get; }

        public MatchCaseSyntax[] Cases { get; }

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
