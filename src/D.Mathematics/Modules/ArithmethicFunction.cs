namespace E.Mathematics;

public sealed class ArithmeticFunction(string name, Func<INumber, INumber, INumber> func) : IFunction
{
    public static readonly ArithmeticFunction Add      = new("+",  Arithmetic.Add);
    public static readonly ArithmeticFunction Multiply = new("*",  Arithmetic.Multiply);
    public static readonly ArithmeticFunction Subtract = new("-",  Arithmetic.Subtract);
    public static readonly ArithmeticFunction Divide   = new("/",  Arithmetic.Divide);
    public static readonly ArithmeticFunction Power    = new("**", Arithmetic.Pow);
    public static readonly ArithmeticFunction Modulus  = new("%",  Arithmetic.Modulus);

    public static readonly MathFunction Floor          = new("floor", Math.Floor);
    public static readonly MathFunction Log            = new("log",   x => Math.Log(x));
    public static readonly MathFunction Log10          = new("log10", x => Math.Log10(x));
    public static readonly MathFunction SquareRoot     = new("sqrt",  x => Math.Sqrt(x));

    private readonly Func<INumber, INumber, INumber> func = func;

    public string Name { get; } = name;

    public Parameter[] Parameters { get; } = [Parameter.Number, Parameter.Number];

    public ObjectType Kind => ObjectType.Function; // TODO: Use function info...

    public object Invoke(IArguments args)
    {
        var x = (INumber)args[0];
        var y = (INumber)args[1];

        return func.Invoke(x, y);
    }
}