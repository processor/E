namespace D.Expressions
{
    public sealed class TypeInitializer : IExpression
    {
        public TypeInitializer(TypeSymbol type, Argument[] arguments)
        {
            Type = type;
            Arguments = arguments;
        }

        public TypeSymbol Type { get; }

        public Argument[] Arguments { get; }

        public int Count => Arguments.Length;

        ObjectType IObject.Kind => ObjectType.TypeInitializer; 
    }
}