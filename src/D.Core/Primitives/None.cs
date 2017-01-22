namespace D
{
    using Expressions;

    public class None : IExpression
    {
        public static readonly None Instance = new None();

        private None() { }

        Kind IObject.Kind => Kind.None;

        public override string ToString() => "[none]";
    }
}
