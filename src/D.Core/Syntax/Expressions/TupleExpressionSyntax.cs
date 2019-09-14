using System;

namespace D.Syntax
{
    public sealed class TupleExpressionSyntax : ISyntaxNode
    {
        public TupleExpressionSyntax(ISyntaxNode[] elements)
        {
            Elements = elements;
        }

        public ISyntaxNode[] Elements { get; }

        public int Size => Elements.Length;

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TupleExpression;
    }
}
