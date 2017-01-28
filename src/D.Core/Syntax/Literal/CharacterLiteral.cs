namespace D.Syntax
{
    public struct CharacterLiteralSyntax : ISyntax
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

        Kind IObject.Kind => Kind.Character;

        public override string ToString() => Value.ToString();
    }
}
