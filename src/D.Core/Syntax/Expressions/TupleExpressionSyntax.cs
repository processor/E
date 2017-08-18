using System;

namespace D.Syntax
{
    public class TupleExpressionSyntax : SyntaxNode
    {
        public TupleExpressionSyntax(SyntaxNode[] elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public SyntaxNode[] Elements { get; }

        public int Size => Elements.Length;

        SyntaxKind SyntaxNode.Kind => SyntaxKind.TupleExpression;
    }

    // a: 100
    public class NamedElementSyntax : SyntaxNode
    {
        public NamedElementSyntax(Symbol name, SyntaxNode value)
        {
            Name  = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
        }

        public Symbol Name { get; }

        // type or constant
        public SyntaxNode Value { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.NamedValue;
    }
}
 