namespace E.Syntax;

public sealed class MatchCaseSyntax(ISyntaxNode pattern, ISyntaxNode? condition, LambdaExpressionSyntax body)
{
    public ISyntaxNode Pattern { get; } = pattern;

    public ISyntaxNode? Condition { get; } = condition;

    public LambdaExpressionSyntax Body { get; } = body;
}

/*
switch expression { 
  case pattern => body
  case pattern when condition => body
  case pattern => { 
  }
}
*/
