namespace E.Syntax;

public sealed class CharacterLiteralSyntax : ISyntaxNode
{
    public CharacterLiteralSyntax(char value)
    {
        Value = value;
    }

    public char Value { get; }

    public static implicit operator CharacterLiteralSyntax(char value)
    {
        return new CharacterLiteralSyntax(value);
    }

    public static implicit operator char(CharacterLiteralSyntax text) => text.Value;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.CharacterLiteral;

    public override string ToString() => Value.ToString();
}