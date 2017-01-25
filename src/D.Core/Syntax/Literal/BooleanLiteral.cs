namespace D.Syntax
{
    public class BooleanLiteral : ISyntax
    {
        public static readonly BooleanLiteral True = new BooleanLiteral(true);
        public static readonly BooleanLiteral False = new BooleanLiteral(false);

        public BooleanLiteral(bool value)
        {
            Value = value;
        }

        public bool Value { get; }

        Kind IObject.Kind => Kind.Boolean;

        public override string ToString() => Value ? "true": "false";
    }
}
