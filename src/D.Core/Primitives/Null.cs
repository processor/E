namespace D
{
    using Expressions;

    public class Null : IExpression
    {
        public static readonly Null Instance = new Null();

        private Null() { }

        ObjectType IObject.Kind => ObjectType.Null;

        public override string ToString() => "[none]";
    }
}