using System;

namespace D.Syntax
{
    public class MatchExpressionSyntax : SyntaxNode
    {
        public MatchExpressionSyntax(SyntaxNode expression, CaseSyntax[] cases)
        {
            Expression = expression;
            Cases = cases ?? throw new ArgumentNullException(nameof(cases));
        }

        public SyntaxNode Expression { get; }

        public CaseSyntax[] Cases { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.MatchExpression;
    }

    public class CaseSyntax
    {
        public CaseSyntax(SyntaxNode pattern, SyntaxNode condition, LambdaExpressionSyntax body)
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
