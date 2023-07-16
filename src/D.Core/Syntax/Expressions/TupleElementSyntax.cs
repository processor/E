using E.Symbols;

namespace E.Syntax;

// a: 100
public sealed class TupleElementSyntax(Symbol name, ISyntaxNode? value) : ISyntaxNode
{
    public Symbol Name { get; } = name;

    // type or constant
    public ISyntaxNode? Value { get; } = value;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.TupleElement;
}
