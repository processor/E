using E.Symbols;

namespace E.Expressions;

public sealed class TypeDeclaration : TypeDeclarationBase
{
    public TypeDeclaration(Symbol name, Parameter[] genericParameters, Symbol baseType, Property[] members, TypeFlags flags)
        : base (baseType, members, flags)
    {
        Name = name;
        GenericParameters = genericParameters;
    }

    // e.g.
    // Crash 
    // Vehicle 'Crash   term
    public Symbol Name { get; }

    public Parameter[] GenericParameters { get; }
}

/*
type Person = {
  name: String where length > 0
}
*/
