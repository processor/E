namespace E.Syntax;

public sealed class FragmentNode(IObject[] children) : IObject
{
    public IObject[] Children { get; } = children;

    ObjectType IObject.Kind => ObjectType.Fragment;
}