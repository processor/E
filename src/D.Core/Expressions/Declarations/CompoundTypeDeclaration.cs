using E.Symbols;

namespace E.Expressions;

public sealed class CompoundTypeDeclaration(
    Symbol[] names,
    TypeFlags flags,
    Symbol baseType,
    Property[] properties) 
    : TypeDeclarationBase(baseType, properties, flags)
{
    public Symbol[] Names { get; } = names;
}    