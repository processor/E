using System;

namespace D.Syntax
{
    public class TupleExpressionSyntax : ISyntaxNode
    {
        public TupleExpressionSyntax(ISyntaxNode[] elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public ISyntaxNode[] Elements { get; }

        public int Size => Elements.Length;

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TupleExpression;
    }

    // a: 100
    public class NamedElementSyntax : ISyntaxNode
    {
        public NamedElementSyntax(Symbol name, ISyntaxNode value)
        {
            Name  = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
        }

        public Symbol Name { get; }

        // type or constant
        public ISyntaxNode Value { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.NamedValue;
    }
}
 