using System;

namespace D.Expressions
{
    public class TupleExpression : IExpression
    {
        public TupleExpression(IExpression[] elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public int Size => Elements.Length;

        // {expression} | {name}:{expression}

        public IExpression[] Elements { get; }

        Kind IObject.Kind => Kind.TupleExpression;
    }

    // a: 100
    public class NamedElement : IExpression
    {
        public NamedElement(Symbol name, IExpression value)
        {
            Name = name;
            Value = value;
        }

        public Symbol Name { get; }

        // type or constant
        public IExpression Value { get; }
        
        public void Deconstruct(out Symbol name, out IExpression value)
        {
            name = Name;
            value = Value;
        }

        Kind IObject.Kind => Kind.NamedValue;
    }
}