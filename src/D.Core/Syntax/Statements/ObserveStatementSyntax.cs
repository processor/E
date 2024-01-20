using E.Symbols;

namespace E.Syntax;

// e.g. on bank Account'Opened opening { }

public sealed class ObserveStatementSyntax(
    ISyntaxNode observable,
    Symbol eventType,
    Symbol eventName,
    ISyntaxNode body,
    UntilConditionSyntax? untilExpression) : ISyntaxNode
{
    // document
    public ISyntaxNode Observable { get; } = observable;

    // Pointer'Moved
    public Symbol EventType { get; } = eventType;

    // e
    public Symbol ParameterName { get; } = eventName;

    // Block | Lambda
    public ISyntaxNode Body { get; } = body;

    // until x Detached
    public UntilConditionSyntax? UntilExpression { get; } = untilExpression;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ObserveStatement;
}

public sealed class UntilConditionSyntax(ISyntaxNode observable, Symbol @event)
{
    public ISyntaxNode Observable { get; } = observable;

    public Symbol Event { get; } = @event;
}