namespace E.Syntax;

public sealed class CharacterLiteralSyntax(char value) : ISyntaxNode
{
    public char Value { get; } = value;

    public static implicit operator CharacterLiteralSyntax(char value)
    {
        return new CharacterLiteralSyntax(value);
    }

    public static implicit operator char(CharacterLiteralSyntax text) => text.Value;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.CharacterLiteral;

    public override string ToString() => Value.ToString();
}