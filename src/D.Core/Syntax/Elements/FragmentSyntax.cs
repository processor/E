namespace D.Syntax
{
    public sealed class FragmentNode : IObject
    {
        public FragmentNode(IObject[] children)
        {
            Children = children;
        }

        public IObject[] Children { get; }

        ObjectType IObject.Kind => ObjectType.Fragment;
    }
}