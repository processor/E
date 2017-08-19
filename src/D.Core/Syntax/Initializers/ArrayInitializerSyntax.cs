namespace D.Syntax
{
    public class ArrayInitializerSyntax : SyntaxNode
    {
        public ArrayInitializerSyntax(SyntaxNode[] elements, int? stride)
        {
            Elements = elements;
            Stride = stride;
        }

        public SyntaxNode[] Elements { get; }

        public int? Stride { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ArrayInitializer;
    }
}