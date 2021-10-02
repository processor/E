using E.Symbols;

namespace E.Syntax;

// e.g. on bank Account'Opened opening { }

public sealed class ObserveStatementSyntax : ISyntaxNode
{
    public ObserveStatementSyntax(
        ISyntaxNode observable,
        Symbol eventType, 
        Symbol eventName,
        ISyntaxNode body,
        UntilConditionSyntax? untilExpression)
    {
        Observable = observable;
        EventType = eventType;
        ParameterName = eventName;
        Body = body;
        UntilExpression = untilExpression;
    }

    // document
    public ISyntaxNode Observable { get; set; }

    // Pointer'Moved
    public Symbol EventType { get; }

    // e
    public Symbol ParameterName { get; set; }

    // Block | Lambda
    public ISyntaxNode Body { get; }

    // until gallary Detached
    public UntilConditionSyntax? UntilExpression { get; set; }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ObserveStatement;
}

public sealed class UntilConditionSyntax
{
    public UntilConditionSyntax(ISyntaxNode observable, Symbol @event)
    {
        Observable = observable;
        Event = @event;
    }

    public ISyntaxNode Observable { get; }

    public Symbol Event { get; }
}