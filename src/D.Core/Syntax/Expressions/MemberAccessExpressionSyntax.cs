using E.Symbols;

namespace E.Syntax;

// .member
public sealed class MemberAccessExpressionSyntax : ISyntaxNode
{
    public MemberAccessExpressionSyntax(ISyntaxNode left, Symbol name)
    {
        Left = left;
        Name = name;
    }

    public ISyntaxNode Left { get; }

    // Property | Function
    public Symbol Name { get; }

    public override string ToString()
    {
        return $"{Left}.{Name}";
    }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.MemberAccessExpression;
}