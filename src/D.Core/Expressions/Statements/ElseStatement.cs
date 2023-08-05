namespace E.Expressions;

public sealed class ElseStatement(BlockExpression body) : IExpression
{
    public BlockExpression Body { get; } = body;

    ObjectType IObject.Kind => ObjectType.ElseStatement;
}
