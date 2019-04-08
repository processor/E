namespace D.Syntax
{
    public class FragmentNode : IObject
    {
        public FragmentNode(IObject[] children)
        {
            Children = children;
        }

        public IObject[] Children { get; }

        Kind IObject.Kind => Kind.Fragment;
    }
}