namespace D
{
    using Expressions;

    public class Null : IExpression
    {
        public static readonly Null Instance = new Null();

        private Null() { }

        Kind IObject.Kind => Kind.Null;

        public override string ToString() => "[none]";
    }
}