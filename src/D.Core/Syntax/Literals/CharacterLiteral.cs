namespace D.Syntax
{
    public struct CharacterLiteralSyntax : SyntaxNode
    { 
        public CharacterLiteralSyntax(char value)
        {
            Value = value;
        }

        public char Value { get; }

        public static implicit operator CharacterLiteralSyntax(char value)
            => new CharacterLiteralSyntax(value);

        public static implicit operator char(CharacterLiteralSyntax text)
            => text.Value;

        SyntaxKind SyntaxNode.Kind => SyntaxKind.CharacterLiteral;

        public override string ToString() => Value.ToString();
    }
}
