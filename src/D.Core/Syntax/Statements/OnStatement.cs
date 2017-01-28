namespace D.Syntax
{
    // e.g. on bank Account'Opened opening { }

    public class ObserveStatement : SyntaxNode
    {
        public ObserveStatement(SyntaxNode observable, Symbol eventType, string eventName, SyntaxNode body, UntilExpression until)
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
        public UntilExpression UntilExpression { get; set; }

        Kind IObject.Kind => Kind.ObserveStatement;
    }

    public class UntilExpression
    {
        public UntilExpression(SyntaxNode observable, Symbol eventType)
        {
            Observable = observable;
            EventType = eventType;
        }

        public SyntaxNode Observable { get; }

        public Symbol EventType { get; }
    }
}