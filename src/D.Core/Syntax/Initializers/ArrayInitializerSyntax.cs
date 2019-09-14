namespace D.Syntax
{
    public sealed class ArrayInitializerSyntax : ISyntaxNode
    {
        public ArrayInitializerSyntax(ISyntaxNode[] elements, int? stride)
        {
            Elements = elements;
            Stride = stride;
        }

        public ISyntaxNode[] Elements { get; }

        public int? Stride { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ArrayInitializer;
    }
}