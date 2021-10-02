namespace E.Expressions;

// { a, b }
public sealed class ObjectPattern : IExpression
{
    ObjectType IObject.Kind => ObjectType.ObjectPattern;
}