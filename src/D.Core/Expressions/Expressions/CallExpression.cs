using E.Symbols;

namespace E.Expressions;

public sealed class CallExpression : IExpression
{
    public CallExpression(
        IExpression? callee, 
        Symbol functionName,
        IArguments arguments, 
        bool isPiped)
    {
        Callee       = callee;
        FunctionName = functionName;
        Arguments    = arguments;
        IsPiped      = isPiped;
    }
        
    // is constructor?

    public IExpression? Callee { get; }

    public Symbol FunctionName { get; }

    public IArguments Arguments { get; }

    public bool IsPiped { get; }

    ObjectType IObject.Kind => ObjectType.CallExpression;

    public Type? ReturnType { get; set; }
}