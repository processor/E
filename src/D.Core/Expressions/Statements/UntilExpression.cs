using E.Symbols;

namespace E.Expressions;

public sealed class UntilExpression(IExpression observable, Symbol eventType)
{
    public IExpression Observable { get; } = observable;

    public Symbol EventType { get; } = eventType;
}