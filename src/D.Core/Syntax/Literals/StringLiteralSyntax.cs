namespace E.Syntax;

public sealed class StringLiteralSyntax(string value) : ISyntaxNode
{
    public string Value { get; } = value;

    public static implicit operator StringLiteralSyntax(string text) =>
        new StringLiteralSyntax(text);

    public static implicit operator string(StringLiteralSyntax text) =>
        text.Value;

    public override string ToString() => Value;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.StringLiteral;
}

// TODO: Multiline literals (""")