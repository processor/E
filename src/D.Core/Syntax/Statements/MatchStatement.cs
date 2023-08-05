namespace E.Syntax;

public sealed class MatchExpressionSyntax(ISyntaxNode expression, MatchCaseSyntax[] cases) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = expression;

    public MatchCaseSyntax[] Cases { get; } = cases;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.MatchExpression;
}

/*
switch expression { 
    case pattern => body
    case pattern when condition => body
    case pattern => { 

    }
*/
