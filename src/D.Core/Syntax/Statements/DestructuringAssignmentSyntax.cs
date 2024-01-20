using E.Symbols;

namespace E.Syntax;

// let (a, b, c) = point

public sealed class DestructuringAssignmentSyntax(
    AssignmentElementSyntax[] elements,
    ISyntaxNode instance) : ISyntaxNode
{
    public AssignmentElementSyntax[] Variables { get; } = elements;

    public ISyntaxNode Instance { get; } = instance;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.DestructuringAssignment;
}

public readonly struct AssignmentElementSyntax(Symbol name, Symbol? type)
{
    public Symbol Name { get; } = name;

    public Symbol? Type { get; } = type;
}