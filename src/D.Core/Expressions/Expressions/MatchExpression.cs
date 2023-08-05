namespace E.Expressions;

public sealed class MatchExpression(IExpression expression, MatchCase[] cases) : IExpression
{
    public IExpression Expression { get; } = expression;

    public MatchCase[] Cases { get; } = cases;

    ObjectType IObject.Kind => ObjectType.MatchExpression;
}

public sealed class MatchCase(
    IExpression pattern,
    IExpression? condition,
    LambdaExpression body)
{
    public IExpression Pattern { get; } = pattern;

    public IExpression? Condition { get; } = condition;

    public LambdaExpression Body { get; } = body;
}

/*
switch expression { 
    case pattern => body
    case pattern when condition => body
    case pattern => { 

    }
*/
