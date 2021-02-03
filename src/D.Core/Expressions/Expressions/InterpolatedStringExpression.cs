namespace E.Expressions
{
    // $"{expression}text"
    public sealed class InterpolatedStringExpression : IExpression
    {
        public InterpolatedStringExpression(IExpression[] children)
        {
            Children = children;
        }

        public IExpression[] Children { get; }

        public IExpression this[int index] => Children[index];

        ObjectType IObject.Kind => ObjectType.InterpolatedStringExpression;
    }
}