using D.Symbols;

namespace D.Syntax
{
    public sealed class ArrayInitializerSyntax : ISyntaxNode
    {
        public ArrayInitializerSyntax(ISyntaxNode[] elements, int? stride)
        {
            Elements = elements;
            Stride = stride;
        }

        public ISyntaxNode[] Elements { get; }

        public int? Stride { get; }

        public TypeSymbol? ElementType { get; set; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ArrayInitializer;
    }
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