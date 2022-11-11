namespace E.Mathematics;

public sealed class ArithmethicFunction : IFunction
{
    public static readonly ArithmethicFunction Add      = new ("+",  Arithmetic.Add);
    public static readonly ArithmethicFunction Multiply = new ("*",  Arithmetic.Multiply);
    public static readonly ArithmethicFunction Subtract = new ("-",  Arithmetic.Subtract);
    public static readonly ArithmethicFunction Divide   = new ("/",  Arithmetic.Divide);
    public static readonly ArithmethicFunction Power    = new ("**", Arithmetic.Pow);
    public static readonly ArithmethicFunction Modulus  = new ("%",  Arithmetic.Modulus);

    public static readonly MathFunction Floor           = new ("floor", Math.Floor);
    public static readonly MathFunction Log             = new ("log",   x => Math.Log(x));
    public static readonly MathFunction Log10           = new ("log10", x => Math.Log10(x));
    public static readonly MathFunction SquareRoot      = new ("sqrt",  x => Math.Sqrt(x));

    private readonly Func<INumber, INumber, INumber> func;

    public ArithmethicFunction(string name, Func<INumber, INumber, INumber> func)
    {
        Name = name;
        Parameters = new[] { Parameter.Number, Parameter.Number };

        this.func = func;
    }

    public string Name { get; }

    public Parameter[] Parameters { get; }

    public ObjectType Kind => ObjectType.Function; // TODO: Use function info...

    public object Invoke(IArguments args)
    {
        var x = (INumber)args[0];
        var y = (INumber)args[1];

        return func.Invoke(x, y);
    }
}