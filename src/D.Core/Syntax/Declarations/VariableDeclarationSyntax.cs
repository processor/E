using E.Symbols;

namespace E.Syntax;

public sealed class VariableDeclarationSyntax(
    Symbol name,
    TypeSymbol type,
    ISyntaxNode? value = null,
    ObjectFlags flags = default) : IMemberSyntax, ISyntaxNode
{
    public Symbol Name { get; } = name;

    // String
    // String | Number
    // A & B
    public TypeSymbol Type { get; } = type;

    // TODO: Condition

    public ISyntaxNode? Value { get; } = value;

    public ObjectFlags Flags { get; } = flags;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.VariableDeclaration;
}

// a, b, c: Number

public sealed class CompoundVariableDeclaration(PropertyDeclarationSyntax[] declarations) : ISyntaxNode
{
    public PropertyDeclarationSyntax[] Members { get; } = declarations;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.CompoundVariableDeclaration;
}

/*
let a: Integer = 1;
let a: Integer > 1 = 5;
let a = 1;
var a = 1
*/