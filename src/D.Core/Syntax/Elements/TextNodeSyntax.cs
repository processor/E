namespace E.Syntax;

public sealed class TextNodeSyntax(string content) : ISyntaxNode
{
    public string Content { get; } = content;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.TextNode;
}