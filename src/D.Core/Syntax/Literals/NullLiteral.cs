namespace D.Syntax
{
    public struct NullLiteralSyntax : SyntaxNode
    {
        public static readonly NullLiteralSyntax Instance = new NullLiteralSyntax();

        Kind IObject.Kind => Kind.Null;

        public override string ToString() => "null";
    }
}
