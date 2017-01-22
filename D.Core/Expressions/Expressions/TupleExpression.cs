using System.Collections.Generic;

namespace D.Expressions
{
    public class TupleExpression : IExpression
    {
        public TupleExpression(IList<IExpression> elements)
        {
            Elements = elements;
        }

        public int Size => Elements.Count;

        public IList<IExpression> Elements { get; }

        public Kind Kind => Kind.TupleExpression;
    }

    // a: 100
    public class NamedElement : IExpression
    {
        public NamedElement(string name, IExpression value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        // type or constant
        public IExpression Value { get; }

        Kind IObject.Kind => Kind.NamedValue;
    }

    // a: [ ] byte
    public class NamedType : IExpression
    {
        public NamedType(string name, Symbol type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }

        public Symbol Type { get; }

        Kind IObject.Kind => Kind.NamedType;

    }

    // 1: i32
    public class TypedValue : IObject
    {
        public TypedValue(IExpression value, Symbol type)
        {
            Value = value;
            Type = type;
        }

        public IExpression Value { get; }

        // type or constant
        public Symbol Type { get; }

        Kind IObject.Kind => Kind.TypedValue;
    }
}
 