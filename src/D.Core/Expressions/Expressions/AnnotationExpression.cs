namespace D.Expressions
{
    public class AnnotationExpression : IExpression
    {
        public AnnotationExpression(Symbol name, IArguments arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Symbol Name { get; }

        public IArguments Arguments { get; }

        Kind IObject.Kind => Kind.AnnotationExpression;
    }
}