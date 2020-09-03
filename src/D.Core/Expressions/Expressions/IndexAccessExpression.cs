namespace D.Expressions
{
    // [index]
    public sealed class IndexAccessExpression : IExpression
    {
        public IndexAccessExpression(IExpression left, IArguments arguments)
        {
            Left = left;
            Arguments = arguments;
        }

        public IExpression Left { get; set; }

        // [1]
        // [1, 2]
        public IArguments Arguments { get; set; }

        ObjectType IObject.Kind => ObjectType.IndexAccessExpression;
    }
}