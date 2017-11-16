using System;

namespace D.Syntax
{
    public class MatchExpressionSyntax : ISyntaxNode
    {
        public MatchExpressionSyntax(ISyntaxNode expression, CaseSyntax[] cases)
        {
            Expression = expression;
            Cases = cases ?? throw new ArgumentNullException(nameof(cases));
        }

        public ISyntaxNode Expression { get; }

        public CaseSyntax[] Cases { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.MatchExpression;
    }

    public class CaseSyntax
    {
        public CaseSyntax(ISyntaxNode pattern, ISyntaxNode condition, LambdaExpressionSyntax body)
        {
            Pattern     = pattern;
            Condition   = condition;
            Body        = body;
        }
        
        public ISyntaxNode Pattern { get; }

        public ISyntaxNode Condition { get; }

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
