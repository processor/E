namespace D.Expressions
{
    public class CallExpression : IExpression
    {
        public CallExpression(IExpression callee, Symbol functionName, IArguments arguments, bool piped)
        {
            Callee       = callee;
            FunctionName = functionName;
            Arguments    = arguments;
            IsPiped      = piped;
        }
        
        // is constructor?

        public IExpression /*?*/ Callee { get; }

        public Symbol FunctionName { get; }

        public IArguments Arguments { get; }

        public bool IsPiped { get; }

        Kind IObject.Kind => Kind.CallExpression;

        public Type ReturnType { get; set; }
    }
}