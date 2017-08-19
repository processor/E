namespace D.Syntax
{
    public struct NullLiteralSyntax : SyntaxNode
    {
        public static readonly NullLiteralSyntax Instance = new NullLiteralSyntax();

        SyntaxKind SyntaxNode.Kind => SyntaxKind.NullLiteral;

        public override string ToString() => "null";
    }
}
