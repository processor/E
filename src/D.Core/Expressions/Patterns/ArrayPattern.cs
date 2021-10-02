namespace E.Expressions;

// [ a, b ]
public sealed class ArrayPattern : IExpression
{
    ObjectType IObject.Kind => ObjectType.ArrayPattern;
}