using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

// type | record | event

/*
A type { 
  a: String
  b: Number
}     
*/
public abstract class TypeDefinitionBase(
    Symbol baseType,
    IReadOnlyList<ISyntaxNode> members,
    TypeFlags flags) : ISyntaxNode
{
    // : A
    public Symbol BaseType { get; } = baseType;

    public TypeFlags Flags { get; } = flags;

    public IReadOnlyList<ISyntaxNode> Members { get; } = members;

    public bool IsRecord => Flags.HasFlag(TypeFlags.Record);

    public bool IsEvent => Flags.HasFlag(TypeFlags.Event);

    public bool IsRole => Flags.HasFlag(TypeFlags.Role);

    public bool IsActor => Flags.HasFlag(TypeFlags.Actor);

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.TypeDeclaration;
}

public sealed class TypeDeclarationSyntax(
    Symbol name,
    IReadOnlyList<ParameterSyntax> genericParameters,
    Symbol baseType,
    IReadOnlyList<ArgumentSyntax> arguments,
    AnnotationSyntax[] annotations,
    IReadOnlyList<ISyntaxNode> members,
    TypeFlags flags = TypeFlags.None) : TypeDefinitionBase(baseType, members, flags)
{

    // e.g.
    // Crash 
    // Vehicle 'Crash 
    public Symbol Name { get; } = name;

    public IReadOnlyList<ArgumentSyntax> Arguments { get; } = arguments;

    public AnnotationSyntax[] Annotations { get; } = annotations;

    public IReadOnlyList<ParameterSyntax> GenericParameters { get; } = genericParameters;
}

// Las `Vegas, New `York : State class { }

public sealed class CompoundTypeDeclarationSyntax(
    Symbol[] names,
    TypeFlags flags,
    Symbol baseType,
    IReadOnlyList<ISyntaxNode> members) : TypeDefinitionBase(baseType, members, flags)
{
    public Symbol[] Names { get; } = names;
}


// MAP = * -> *

/*
Person type {
name: String where length > 0
}
*/
