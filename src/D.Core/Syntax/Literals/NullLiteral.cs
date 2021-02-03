namespace E.Syntax
{
    public sealed class NullLiteralSyntax : ISyntaxNode
    {
        public static readonly NullLiteralSyntax Instance = new NullLiteralSyntax();

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.NullLiteral;

        public override string ToString() => "null";
    }
}
