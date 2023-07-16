using E.Symbols;

namespace E.Syntax;

public sealed class ArrayInitializerSyntax(ISyntaxNode[] elements, int? stride) : ISyntaxNode
{
    public ISyntaxNode[] Elements { get; } = elements;

    public int? Stride { get; } = stride;

    public TypeSymbol? ElementType { get; set; }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ArrayInitializer;
}


// [N] Type
// [32] Pixel

// ------------

// Rust
// [ T; N ]

// Swift
// [T](count: 64)

// GoLang
// [N]T

// C#
// new T[N]

// Stark
// [N] T