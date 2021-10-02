﻿
using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

// let (a, b, c) = point

public sealed class DestructuringAssignmentSyntax : ISyntaxNode
{
    public DestructuringAssignmentSyntax(IReadOnlyList<AssignmentElementSyntax> elements, ISyntaxNode instance)
    {
        Variables = elements;
        Instance = instance;
    }

    public IReadOnlyList<AssignmentElementSyntax> Variables { get; }

    public ISyntaxNode Instance { get; }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.DestructuringAssignment;
}

public sealed class AssignmentElementSyntax
{
    public AssignmentElementSyntax(Symbol name, Symbol? type)
    {
        Name = name;
        Type = type;
    }

    public Symbol Name { get; }

    public Symbol? Type { get; }
}