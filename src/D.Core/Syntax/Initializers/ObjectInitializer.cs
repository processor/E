﻿using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

public sealed class ObjectInitializerSyntax(
    TypeSymbol type,
    IReadOnlyList<ArgumentSyntax> arguments) : ISyntaxNode
{
    public TypeSymbol Type { get; } = type;

    public IReadOnlyList<ArgumentSyntax> Arguments { get; } = arguments;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.TypeInitializer;
}

// Tuple Based Syntax
// (x: 1, y: 2)
// (x, y)

// Rust uses a different syntax... and requires that all fields be initized at once
// Point { x: 1, y: 2 }