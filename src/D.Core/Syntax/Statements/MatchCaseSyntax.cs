namespace D.Syntax
{
    public sealed class MatchCaseSyntax
    {
        public MatchCaseSyntax(ISyntaxNode pattern, ISyntaxNode? condition, LambdaExpressionSyntax body)
        {
            Pattern = pattern;
            Condition = condition;
            Body = body;
        }

        public ISyntaxNode Pattern { get; }

        public ISyntaxNode? Condition { get; }

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
