namespace D.Expressions
{
    // e.g. on bank Account'Opened opening { }

    public class ObserveStatement : IExpression
    {
        public ObserveStatement(IExpression observable, Symbol eventType, string eventName, IExpression body, UntilExpression until)
        {
            Observable = observable;
            EventType = eventType;
            ParameterName = eventName;
            Body = body;
            UntilExpression = until;
        }

        // document
        public IExpression Observable { get; set; }

        // Pointer'Moved
        public Symbol EventType { get; }

        // e
        public string ParameterName { get; set; }

        // Block | Lambda
        public IExpression Body { get; }

        // until gallary Detached
        public UntilExpression UntilExpression { get; set; }

        Kind IObject.Kind => Kind.ObserveStatement;
    }

    public class UntilExpression
    {
        public UntilExpression(IExpression observable, Symbol eventType)
        {
            Observable = observable;
            EventType = eventType;
        }

        public IExpression Observable { get; }

        public Symbol EventType { get; }
    }
}