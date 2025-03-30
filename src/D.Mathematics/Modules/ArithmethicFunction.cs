namespace E.Mathematics;

public sealed class ArithmeticFunction(string name, Func<INumberObject, INumberObject, INumberObject> func) : IFunction
{
    public static readonly ArithmeticFunction Add      = new("+",  Arithmetic.Add);
    public static readonly ArithmeticFunction Multiply = new("*",  Arithmetic.Multiply);
    public static readonly ArithmeticFunction Subtract = new("-",  Arithmetic.Subtract);
    public static readonly ArithmeticFunction Divide   = new("/",  Arithmetic.Divide);
    public static readonly ArithmeticFunction Power    = new("**", Arithmetic.Pow);
    public static readonly ArithmeticFunction Modulus  = new("%",  Arithmetic.Modulus);

    public static readonly MathFunction<double> Floor          = new("floor", Math.Floor);
    public static readonly MathFunction<double> Log            = new("log",   Math.Log);
    public static readonly MathFunction<double> Log10          = new("log10", Math.Log10);
    public static readonly MathFunction<double> SquareRoot     = new("sqrt",  Math.Sqrt);

    private readonly Func<INumberObject, INumberObject, INumberObject> func = func;

    public string Name { get; } = name;

    public Parameter[] Parameters { get; } = [Parameter.Number, Parameter.Number];

    public ObjectType Kind => ObjectType.Function; // TODO: Use function info...

    public object Invoke(IArguments args)
    {
        var x = (INumberObject)args[0];
        var y = (INumberObject)args[1];

        return func.Invoke(x, y);
    }
}