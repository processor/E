using E.Symbols;

namespace E.Syntax;

public sealed class PropertyDeclarationSyntax(
    Symbol name,
    TypeSymbol? type,
    ISyntaxNode? value = null,
    ObjectFlags flags = ObjectFlags.None) : IMemberSyntax, ISyntaxNode
{
    public Symbol Name { get; } = name;

    // String
    // String | Number
    // A & B
    public TypeSymbol? Type { get; } = type;

    public ISyntaxNode? Value { get; } = value;

    public ObjectFlags Flags { get; } = flags;

    public bool IsMutable => Flags.HasFlag(ObjectFlags.Mutable);

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.PropertyDeclaration;
}

// a, b, c: Number

public sealed class CompoundPropertyDeclaration(PropertyDeclarationSyntax[] declarations) : ISyntaxNode
{
    public PropertyDeclarationSyntax[] Members { get; } = declarations;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.CompoundPropertyDeclaration;
}

/*
let a: Integer = 5
let y: i64
let a: Integer = 1;
let a: Integer > 1 = 5;
let a = 1;
var a = 1
*/
