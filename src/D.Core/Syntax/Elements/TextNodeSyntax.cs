namespace E.Syntax
{
    public sealed class TextNodeSyntax : ISyntaxNode
    {
        public TextNodeSyntax(string content)
        {
            Content = content;
        }
        
        public string Content { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TextNode;
    }
}