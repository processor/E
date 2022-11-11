namespace E.Mathematics;

public class MathFunction : IFunction
{
    private readonly Func<double, double> _func;

    public MathFunction(string name, Func<double, double> func)
    {
        Name = name;
        Parameters = new[] { Parameter.Get(ObjectType.Number) };

        _func = func;
    }

    public string Name { get; }

    public Parameter[] Parameters { get; }

    ObjectType IObject.Kind => ObjectType.Function;

    public object Invoke(IArguments args)
    { 
        var arg0 = (INumber)args[0];

        return new Number(_func.Invoke(arg0.Real));
    }
}