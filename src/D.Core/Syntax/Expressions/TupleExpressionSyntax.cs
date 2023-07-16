namespace E.Syntax;

public sealed class TupleExpressionSyntax(ISyntaxNode[] elements) : ISyntaxNode
{
    public ISyntaxNode[] Elements { get; } = elements;

    public int Size => Elements.Length;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.TupleExpression;
}