namespace D.Expressions
{
    public struct StringLiteral : IExpression
    { 
        public StringLiteral(string text)
        {
            Value = text;
        }

        public string Value { get; }

        public static implicit operator StringLiteral(string text)
            => new StringLiteral(text);

        public static implicit operator string(StringLiteral text)
            => text.Value;

        Kind IObject.Kind => Kind.String;

        public override string ToString() => Value;
    }
}
