using System;

namespace D.Syntax
{
    public class TupleExpressionSyntax : SyntaxNode
    {
        public TupleExpressionSyntax(SyntaxNode[] elements)
        {
            #region Preconditions

            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            #endregion

            Elements = elements;
        }

        public int Size => Elements.Length;

        public SyntaxNode[] Elements { get; }

        public Kind Kind => Kind.TupleExpression;
    }

    // a: 100
    public class NamedElement : SyntaxNode
    {
        public NamedElement(string name, SyntaxNode value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        // type or constant
        public SyntaxNode Value { get; }

        Kind IObject.Kind => Kind.NamedValue;
    }

    // a: [ ] byte
    public class NamedType : SyntaxNode
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
        public TypedValueSyntax(SyntaxNode value, Symbol type)
        {
            Value = value;
            Type = type;
        }

        public SyntaxNode Value { get; }

        // type or constant
        public Symbol Type { get; }

        Kind IObject.Kind => Kind.TypedValue;
    }
}
 