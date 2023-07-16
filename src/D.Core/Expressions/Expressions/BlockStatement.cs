namespace E.Expressions;

public class BlockExpression(params IExpression[] statements) : IExpression
{
    public IExpression[] Statements { get; } = statements;

    public IExpression this[int index] => Statements[index];

    public int Count => Statements.Length;

    ObjectType IObject.Kind => ObjectType.BlockStatement;
}