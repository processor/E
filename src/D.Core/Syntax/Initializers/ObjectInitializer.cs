using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax
{
    public sealed class ObjectInitializerSyntax : ISyntaxNode
    {
        public ObjectInitializerSyntax(TypeSymbol type, IReadOnlyList<ArgumentSyntax> arguments)
        {
            Type = type;
            Arguments = arguments;
        }

        public TypeSymbol Type { get; }

        public IReadOnlyList<ArgumentSyntax> Arguments { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TypeInitializer;
    }
}

// Tuple Based Syntax
// (x: 1, y: 2)
// (x, y)

// Rust uses a different syntax... and requires that all fields be initized at once
// Point { x: 1, y: 2 }