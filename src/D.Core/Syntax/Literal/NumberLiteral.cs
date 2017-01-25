namespace D.Syntax
{
    public class NumberLiteral : ISyntax
    {
        public NumberLiteral(string text)
        {
            Text = text;
        }

        public string Text { get; }

        Kind IObject.Kind => Kind.Number;

        public static implicit operator int(NumberLiteral value)
            => int.Parse(value.Text);

        public static implicit operator long(NumberLiteral value)
            => long.Parse(value.Text);

        public static implicit operator double(NumberLiteral value)
           => double.Parse(value.Text);

        public override string ToString() => Text;
    }
}
