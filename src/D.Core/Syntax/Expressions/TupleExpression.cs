using System;

namespace D.Syntax
{
    public class TupleExpressionSyntax : ISyntax
    {
        public TupleExpressionSyntax(ISyntax[] elements)
        {
            #region Preconditions

            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            #endregion

            Elements = elements;
        }

        public int Size => Elements.Length;

        public ISyntax[] Elements { get; }

        public Kind Kind => Kind.TupleExpression;
    }

    // a: 100
    public class NamedElement : ISyntax
    {
        public NamedElement(string name, ISyntax value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        // type or constant
        public ISyntax Value { get; }

        Kind IObject.Kind => Kind.NamedValue;
    }

    // a: [ ] byte
    public class NamedType : ISyntax
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
    public class TypedValueSyntax : IObject
    {
        public TypedValueSyntax(ISyntax value, Symbol type)
        {
            Value = value;
            Type = type;
        }

        public ISyntax Value { get; }

        // type or constant
        public Symbol Type { get; }

        Kind IObject.Kind => Kind.TypedValue;
    }
}
 