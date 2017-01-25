namespace D.Syntax
{
    public struct NullLiteral : ISyntax
    {
        public static readonly NullLiteral Instance = new NullLiteral();

        Kind IObject.Kind => Kind.None;

        public override string ToString() => "null";
    }
}
