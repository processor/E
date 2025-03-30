using System.Numerics;

namespace E.Mathematics;

public sealed class MathFunction<T>(string name, Func<T, T> func) : IFunction
    where T: unmanaged, INumber<T>
{
    private readonly Func<T, T> _func = func;

    public string Name { get; } = name;

    public Parameter[] Parameters { get; } = [ Parameter.Get(ObjectType.Number) ];

    ObjectType IObject.Kind => ObjectType.Function;

    public object Invoke(IArguments args)
    { 
        var arg0 = (INumberObject)args[0];

        return new Number<T>(_func.Invoke(arg0.As<T>()));
    }
}