using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

public sealed class UsingStatement(IReadOnlyList<Symbol> domains) : ISyntaxNode
{
    public IReadOnlyList<Symbol> Domains { get; } = domains;

    public Symbol this[int i] => Domains[i];

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ImportStatement;
}

// Rust       | use
// C#         | using
// GO         | import
// JavaScript | import