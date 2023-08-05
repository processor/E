using E.Symbols;

namespace E.Expressions;

// .member
public sealed class MemberAccessExpression(
    IExpression left,
    Symbol memberName) : IExpression
{
    // Type: Array | Property
    public IExpression Left { get; } = left;

    // The member
    public Symbol MemberName { get; } = memberName;

    public override string ToString() => $"{Left}.{MemberName}";

    ObjectType IObject.Kind => ObjectType.MemberAccessExpression;
}