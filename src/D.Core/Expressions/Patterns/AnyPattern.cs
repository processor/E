namespace E.Expressions
{

    // _
    public sealed class AnyPattern : IExpression
    {
        ObjectType IObject.Kind => ObjectType.AnyPattern;
    }
}