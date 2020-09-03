using D.Symbols;

namespace D.Expressions
{
    // a: 100
    public sealed class TupleElement : IExpression
    {
        public TupleElement(Symbol name, IExpression? value)
        {
            Name = name;
            Value = value;
        }

        public Symbol Name { get; }

        // type or constant
        public IExpression? Value { get; }

        public void Deconstruct(out Symbol name, out IExpression? value)
        {
            name = Name;
            value = Value;
        }

        ObjectType IObject.Kind => ObjectType.TupleElement;
    }
}