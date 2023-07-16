namespace E.Syntax;

// => ...
public sealed class LambdaExpressionSyntax(ISyntaxNode expression) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = expression;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.LambdaExpression;
}