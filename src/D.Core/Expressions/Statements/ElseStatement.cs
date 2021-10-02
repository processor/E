namespace E.Expressions;

public sealed class ElseStatement : IExpression
{
    public ElseStatement(BlockExpression body)
    {
        Body = body;
    }

    public BlockExpression Body { get; }

    ObjectType IObject.Kind => ObjectType.ElseStatement;
}
