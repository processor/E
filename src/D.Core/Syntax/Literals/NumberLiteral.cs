namespace D.Syntax
{
    public class NumberLiteralSyntax : SyntaxNode
    {
        public NumberLiteralSyntax(string text)
        {
            Text = text;
        }
        
        public string Text { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.NumberLiteral;

        public static implicit operator int (NumberLiteralSyntax value) => int.Parse(value.Text);

        public static implicit operator long (NumberLiteralSyntax value) => long.Parse(value.Text);

        public static implicit operator double (NumberLiteralSyntax value) => double.Parse(value.Text);

        public override string ToString() => Text;
    }

    // Sign...
    // Fraction
    // Exponent

    // Formats:
    // Decimal
    // Binary
    // Octal
    // Hexadecimal

    // Bit / Word Count (8, 16, 32, ...)
}
