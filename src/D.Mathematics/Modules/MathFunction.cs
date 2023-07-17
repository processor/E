namespace E.Mathematics;

public class MathFunction(string name, Func<double, double> func) : IFunction
{
    private readonly Func<double, double> _func = func;

    public string Name { get; } = name;

    public Parameter[] Parameters { get; } = new[] { Parameter.Get(ObjectType.Number) };

    ObjectType IObject.Kind => ObjectType.Function;

    public object Invoke(IArguments args)
    { 
        var arg0 = (INumber)args[0];

        return new Number(_func.Invoke(arg0.Real));
    }
}