using E.Symbols;

namespace E.Expressions;

// e.g. on bank Account'Opened opening { }

public sealed class ObserveStatement(
    IExpression observable,
    Symbol eventType,
    string eventName,
    IExpression body,
    UntilExpression until) : IExpression
{
    // document
    public IExpression Observable { get; } = observable;

    // Pointer'Moved
    public Symbol EventType { get; } = eventType;

    // e
    public string ParameterName { get; } = eventName;

    // Block | Lambda
    public IExpression Body { get; } = body;

    // until x Detached
    public UntilExpression UntilExpression { get; } = until;

    ObjectType IObject.Kind => ObjectType.ObserveStatement;
}
