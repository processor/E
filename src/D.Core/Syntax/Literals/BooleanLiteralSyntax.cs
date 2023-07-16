namespace E.Syntax;

public sealed class BooleanLiteralSyntax(bool value) : ISyntaxNode
{
    public static readonly BooleanLiteralSyntax True  = new(true);
    public static readonly BooleanLiteralSyntax False = new(false);

    public bool Value { get; } = value;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.BooleanLiteral;

    public override string ToString() => Value ? "true": "false";
}