using E.Symbols;

namespace E.Syntax;

// .member
public sealed class MemberAccessExpressionSyntax(ISyntaxNode left, Symbol name) : ISyntaxNode
{
    public ISyntaxNode Left { get; } = left;

    // Property | Function
    public Symbol Name { get; } = name;

    public override string ToString()
    {
        return $"{Left}.{Name}";
    }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.MemberAccessExpression;
}