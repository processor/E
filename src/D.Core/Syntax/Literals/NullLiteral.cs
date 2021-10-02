namespace E.Syntax;

public sealed class NullLiteralSyntax : ISyntaxNode
{
    public static readonly NullLiteralSyntax Instance = new ();

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.NullLiteral;

    public override string ToString() => "null";
}