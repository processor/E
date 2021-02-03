namespace E.Syntax
{
    public sealed class NumberLiteralSyntax : ISyntaxNode
    {
        public NumberLiteralSyntax(string text)
        {
            Text = text;
        }
        
        public string Text { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.NumberLiteral;

        public static implicit operator int (NumberLiteralSyntax value) => int.Parse(value.Text);

        public static implicit operator long (NumberLiteralSyntax value) => long.Parse(value.Text);

        public static implicit operator double (NumberLiteralSyntax value) => double.Parse(value.Text);

        public static implicit operator decimal (NumberLiteralSyntax value) => decimal.Parse(value.Text);

        public override string ToString() => Text;

        // postfix (u8, i8, i32, f32, d64, ..)
    }

    // Sign...
    // Fraction
    // Exponent

    // Formats:
    // Decimal
    // Binary
    // Octal
    // Hexadecimal
    
    // 456 as i32
    // 900 as f32

    // Bit / Word Count (8, 16, 32, ...)
}