using E.Symbols;

namespace E.Expressions;

public abstract class TypeDeclarationBase(
    Symbol baseType,
    Property[] members,
    TypeFlags flags = TypeFlags.None) : IExpression
{

    // : A
    public Symbol BaseType { get; } = baseType;

    public TypeFlags Flags { get; } = flags;

    public Property[] Members { get; } = members;

    public bool IsRecord => Flags.HasFlag(TypeFlags.Record);

    public bool IsEvent => Flags.HasFlag(TypeFlags.Event);

    ObjectType IObject.Kind => ObjectType.TypeDeclaration;
}