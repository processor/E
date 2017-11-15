namespace D.Syntax
{
    public struct NullLiteralSyntax : ISyntaxNode
    {
        public static readonly NullLiteralSyntax Instance = new NullLiteralSyntax();

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.NullLiteral;

        public override string ToString() => "null";
    }
}
