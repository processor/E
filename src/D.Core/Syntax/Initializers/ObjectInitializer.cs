using System;

namespace D.Syntax
{
    public class ObjectInitializerSyntax : SyntaxNode
    {
        public ObjectInitializerSyntax(TypeSymbol type, ArgumentSyntax[] arguments)
        {
            Type      = type ?? throw new ArgumentNullException(nameof(type));
            Arguments = arguments ?? throw new ArgumentNullException(nameof(type));
        }

        public TypeSymbol Type { get; }

        public ArgumentSyntax[] Arguments { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.TypeInitializer;
    }
}



// Tuple Based Syntax
// (x: 1, y: 2)
// (x, y)

// Rust uses a different syntax... and requires that all fields be initized at once
// Point { x: 1, y: 2 }