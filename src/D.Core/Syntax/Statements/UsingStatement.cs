using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

public sealed class UsingStatement : ISyntaxNode
{
    public UsingStatement(IReadOnlyList<Symbol> domains)
    {
        Domains = domains;
    }

    public IReadOnlyList<Symbol> Domains { get; }

    public Symbol this[int i] => Domains[i];

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ImportStatement;
}

// Rust       | use
// C#         | using
// GO         | import
// JavaScript | import