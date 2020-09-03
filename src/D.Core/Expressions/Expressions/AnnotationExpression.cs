using D.Symbols;

namespace D.Expressions
{
    public sealed class AnnotationExpression : IExpression
    {
        public AnnotationExpression(Symbol name, IArguments arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Symbol Name { get; }

        public IArguments Arguments { get; }

        ObjectType IObject.Kind => ObjectType.AnnotationExpression;
    }
}