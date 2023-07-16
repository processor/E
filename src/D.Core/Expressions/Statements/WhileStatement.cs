namespace E.Expressions;

public sealed class WhileStatement(
    IExpression condition,
    BlockExpression body) : IExpression
{
    public IExpression Condition { get; } = condition;

    public BlockExpression Body { get; } = body;

    ObjectType IObject.Kind => ObjectType.WhileStatement;
}

// rust: loop { } 
// swift: repeat { } while condition

// onIteration  (i++)
// precondition
// postcondition