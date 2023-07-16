using System.Collections.Generic;

namespace E.Syntax;

public sealed class MatchExpressionSyntax(ISyntaxNode expression, IReadOnlyList<MatchCaseSyntax> cases) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = expression;

    public IReadOnlyList<MatchCaseSyntax> Cases { get; } = cases;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.MatchExpression;
}

/*
switch expression { 
    case pattern => body
    case pattern when condition => body
    case pattern => { 

    }
*/
