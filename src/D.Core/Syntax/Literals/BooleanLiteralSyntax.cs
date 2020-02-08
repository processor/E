namespace D.Syntax
{
    public sealed class BooleanLiteralSyntax : ISyntaxNode
    {
        public static readonly BooleanLiteralSyntax True = new BooleanLiteralSyntax(true);
        public static readonly BooleanLiteralSyntax False = new BooleanLiteralSyntax(false);

        public BooleanLiteralSyntax(bool value)
        {
            Value = value;
        }

        public bool Value { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.BooleanLiteral;

        public override string ToString() => Value ? "true": "false";
    }
}