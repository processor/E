namespace E.Expressions;

public sealed class EmitStatement(IExpression expression) : IExpression
{
    public IExpression Expression { get; } = expression;

    ObjectType IObject.Kind => ObjectType.EmitStatement;
}