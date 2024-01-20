using E.Symbols;

namespace E.Syntax;

public sealed class UsingStatement(Symbol[] domains) : ISyntaxNode
{
    public Symbol[] Domains { get; } = domains;

    public Symbol this[int i] => Domains[i];

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ImportStatement;
}

// Rust       | use
// C#         | using
// GO         | import
// JavaScript | import
// zig        | @import