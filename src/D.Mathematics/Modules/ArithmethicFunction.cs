using System;

namespace D.Mathematics
{
    public class ArithmethicFunction : IFunction
    {
        public static readonly IFunction Add        = new ArithmethicFunction("+",  Arithmetic.Add);
        public static readonly IFunction Multiply   = new ArithmethicFunction("*",  Arithmetic.Multiply);

        public static readonly IFunction Subtract   = new ArithmethicFunction("-",  Arithmetic.Subtract);

        public static readonly IFunction Divide     = new ArithmethicFunction("/",  Arithmetic.Divide);
        public static readonly IFunction Power      = new ArithmethicFunction("**", Arithmetic.Pow);
        public static readonly IFunction Modulus    = new ArithmethicFunction("%",  Arithmetic.Modulus);

        public static readonly IFunction Floor      = new MathFunction("floor", x => Math.Floor(x));
        public static readonly IFunction Log        = new MathFunction("log",   x => Math.Log(x));
        public static readonly IFunction Log10      = new MathFunction("log10", x => Math.Log10(x));
        public static readonly IFunction SquareRoot = new MathFunction("sqrt",  x => Math.Sqrt(x));

        private readonly Func<INumber, INumber, INumber> func;

        public ArithmethicFunction(string name, Func<INumber, INumber, INumber> func)
        {
            Name = name;
            Parameters = new[] { Parameter.Number, Parameter.Number };

            this.func = func;
        }

        public string Name { get; }

        public Parameter[] Parameters { get; }

        public Kind Kind => Kind.Function; // TODO: Use function info...

        public object Invoke(IArguments args)
        {
            var x = (INumber)args[0];
            var y = (INumber)args[1];

            return func.Invoke(x, y);
        }
    }
}