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
    public IExpression Observable { get; set; } = observable;

    // Pointer'Moved
    public Symbol EventType { get; } = eventType;

    // e
    public string ParameterName { get; set; } = eventName;

    // Block | Lambda
    public IExpression Body { get; } = body;

    // until gallary Detached
    public UntilExpression UntilExpression { get; set; } = until;

    ObjectType IObject.Kind => ObjectType.ObserveStatement;
}

public sealed class UntilExpression(IExpression observable, Symbol eventType)
{
    public IExpression Observable { get; } = observable;

    public Symbol EventType { get; } = eventType;
}