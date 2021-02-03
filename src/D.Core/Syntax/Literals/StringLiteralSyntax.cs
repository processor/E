namespace E.Syntax
{
    public sealed class StringLiteralSyntax : ISyntaxNode
    {
        public StringLiteralSyntax(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static implicit operator StringLiteralSyntax(string text) =>
            new StringLiteralSyntax(text);

        public static implicit operator string(StringLiteralSyntax text) =>
            text.Value;

        public override string ToString() => Value;

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.StringLiteral;
    }
}

// TODO: Multiline literals (""")