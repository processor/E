namespace D.Syntax
{
    public class TextSyntax : ISyntaxNode
    {
        public TextSyntax(string content)
        {
            Content = content;
        }
        
        public string Content { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.Text;
    }
}