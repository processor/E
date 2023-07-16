using E.Symbols;

namespace E.Expressions;

public sealed class CallExpression(
    IExpression? callee,
    Symbol functionName,
    IArguments arguments,
    bool isPiped) : IExpression
{

    // is constructor?

    public IExpression? Callee { get; } = callee;

    public Symbol FunctionName { get; } = functionName;

    public IArguments Arguments { get; } = arguments;

    public bool IsPiped { get; } = isPiped;

    ObjectType IObject.Kind => ObjectType.CallExpression;

    public Type? ReturnType { get; set; }
}