namespace E.Syntax
{
    public sealed class MatchExpressionSyntax : ISyntaxNode
    {
        public MatchExpressionSyntax(ISyntaxNode expression, MatchCaseSyntax[] cases)
        {
            Expression = expression;
            Cases = cases;
        }

        public ISyntaxNode Expression { get; }

        public MatchCaseSyntax[] Cases { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.MatchExpression;
    }
}

/*
switch expression { 
    case pattern => body
    case pattern when condition => body
    case pattern => { 

    }
*/
