using System;

namespace D.Syntax
{
    // e.g. on bank Account'Opened opening { }

    public class ObserveStatementSyntax : SyntaxNode
    {
        public ObserveStatementSyntax(SyntaxNode observable, Symbol eventType, string eventName, SyntaxNode body, UntilConditionSyntax until)
        {
            Observable = observable;
            EventType = eventType;
            ParameterName = eventName;
            Body = body;
            UntilExpression = until;
        }

        // document
        public IObject Observable { get; set; }

        // Pointer'Moved
        public Symbol EventType { get; }

        // e
        public string ParameterName { get; set; }

        // Block | Lambda
        public SyntaxNode Body { get; }

        // until gallary Detached
        public UntilConditionSyntax UntilExpression { get; set; }

        Kind IObject.Kind => Kind.ObserveStatement;
    }

    public class UntilConditionSyntax
    {
        public UntilConditionSyntax(SyntaxNode observable, Symbol @event)
        {
            Observable = observable;
            Event = @event ?? throw new ArgumentNullException(nameof(@event));
        }

        public SyntaxNode Observable { get; }

        public Symbol Event { get; }
    }
}