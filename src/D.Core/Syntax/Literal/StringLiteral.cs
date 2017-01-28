namespace D.Syntax
{
    public struct StringLiteralSyntax : SyntaxNode
    { 
        public StringLiteralSyntax(string text)
        {
            Value = text;
        }

        public string Value { get; }

        public static implicit operator StringLiteralSyntax(string text)
            => new StringLiteralSyntax(text);

        public static implicit operator string(StringLiteralSyntax text)
            => text.Value;

        Kind IObject.Kind => Kind.StringLiteral;

        public override string ToString() => Value;
    }
}
