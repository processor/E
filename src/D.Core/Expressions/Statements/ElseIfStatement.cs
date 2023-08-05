namespace E.Expressions;

public sealed class ElseIfStatement(
    IExpression condition,
    BlockExpression body,
    IExpression elseBranch) : IExpression
{
    public IExpression Condition { get; } = condition;

    public BlockExpression Body { get; } = body;

    // Else, ElseIf
    public IExpression ElseBranch { get; } = elseBranch;

    ObjectType IObject.Kind => ObjectType.ElseIfStatement;
}