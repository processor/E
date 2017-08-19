using System;

namespace D.Syntax
{
    // e.g. on bank Account'Opened opening { }

    public class ObserveStatementSyntax : SyntaxNode
    {
        public ObserveStatementSyntax(
            SyntaxNode observable,
            Symbol eventType, 
            Symbol eventName,
            SyntaxNode body,
            UntilConditionSyntax untilExpression)
        {
            Observable = observable;
            EventType = eventType;
            ParameterName = eventName;
            Body = body;
            UntilExpression = untilExpression;
        }

        // document
        public SyntaxNode Observable { get; set; }

        // Pointer'Moved
        public Symbol EventType { get; }

        // e
        public Symbol ParameterName { get; set; }

        // Block | Lambda
        public SyntaxNode Body { get; }

        // until gallary Detached
        public UntilConditionSyntax UntilExpression { get; set; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ObserveStatement;
    }

    public class UntilConditionSyntax
    {
        public UntilConditionSyntax(SyntaxNode observable, Symbol @event)
        {
            Observable = observable;
            Event      = @event ?? throw new ArgumentNullException(nameof(@event));
        }

        public SyntaxNode Observable { get; }

        public Symbol Event { get; }
    }
}