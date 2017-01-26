namespace D.Syntax
{
    public class NumberLiteralSyntax : ISyntax
    {
        public NumberLiteralSyntax(string text)
        {
            Text = text;
        }

        public string Text { get; }

        Kind IObject.Kind => Kind.NumberLiteral;

        public static implicit operator int(NumberLiteralSyntax value)
            => int.Parse(value.Text);

        public static implicit operator long(NumberLiteralSyntax value)
            => long.Parse(value.Text);

        public static implicit operator double(NumberLiteralSyntax value)
           => double.Parse(value.Text);

        public override string ToString() => Text;
    }
}
