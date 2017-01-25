namespace D.Syntax
{
    public struct CharacterLiteral : ISyntax
    { 
        public CharacterLiteral(char value)
        {
            Value = value;
        }

        public char Value { get; }

        public static implicit operator CharacterLiteral(char value)
            => new CharacterLiteral(value);

        public static implicit operator char(CharacterLiteral text)
            => text.Value;

        Kind IObject.Kind => Kind.Character;

        public override string ToString() => Value.ToString();
    }
}
