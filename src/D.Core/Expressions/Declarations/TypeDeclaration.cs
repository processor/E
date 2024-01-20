using E.Symbols;

namespace E.Expressions;

public sealed class TypeDeclaration(
    Symbol name,
    Parameter[] genericParameters,
    Symbol baseType,
    Property[] members,
    TypeFlags flags)
    : TypeDeclarationBase(baseType, members, flags)
{
    // | Crash 
    // | Vehicle 'Crash term
    public Symbol Name { get; } = name;

    public Parameter[] GenericParameters { get; } = genericParameters;
}

/*
Person type = {
  name: String { Length: > 0 }
}
*/