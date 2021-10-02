using E.Symbols;

namespace E.Expressions;

// .member
public sealed class MemberAccessExpression : IExpression
{
    public MemberAccessExpression(IExpression left, Symbol memberName)
    {
        Left = left;
        MemberName = memberName;
    }

    // Type: Array | Property
    public IExpression Left { get; }

    // The member
    public Symbol MemberName { get; }

    public override string ToString()
    {
        return $"{Left}.{MemberName}";
    }

    ObjectType IObject.Kind => ObjectType.MemberAccessExpression;
}