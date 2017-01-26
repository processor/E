using System;

namespace D.Mathematics
{
    using Units;

    using static ArithmethicFunction;

    public class ArithmeticModule : Module
    {
        public ArithmeticModule()
        {
            Add(ArithmethicFunction.Add);
            Add(Subtract);
            Add(Multiply);
            Add(Divide);
            Add(Modulus);
            Add(Power);

            // Generic
            Add(Floor);
            Add(SquareRoot);
            Add(Log);
            Add(Log10);
        }
    }
  
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

        public IObject Invoke(IArguments args)
        {
            var x = (INumber)args[0];
            var y = (INumber)args[1];

            return func.Invoke(x, y);
        }
    }

    public class Arithmetic
    {
        // abs
        // intergrate
        // ceiling
        // floor

        public static IArithmetic<T> GetProvider<T>()
        {
            if (typeof(T) == typeof(Float))  return (IArithmetic<T>)new RealArithmetic();
            if (typeof(T) == typeof(int))   return (IArithmetic<T>)new Int32Arithmetic();
            if (typeof(T) == typeof(long))  return (IArithmetic<T>)new Int64Arithmetic();

            throw new Exception("No arithmethic provider for:" + typeof(T).Name);
        }

        public static INumber Multiply(INumber x, INumber y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!(x is IUnit) && !(y is IUnit))
            {
                return new Float(x.Real * y.Real);
            }

            var l = (x as IUnit);
            var r = (y as IUnit)?.To(l) ?? y.Real;

            return y is IUnit
                ? l.With(
                    quantity: l.Real * r,                // multiply the quantities
                    power: l.Power + (y as IUnit).Power  // add the exponents
                )
                : l.With(l.Real * r);
                
        }
    
        public static INumber Add(INumber x, INumber y)
        {
            if (!(x is IUnit) && !(y is IUnit))
            {
                return new Float(x.Real + y.Real);
            }

            var l = (x as IUnit);
            var r = (y as IUnit)?.To(l) ?? y.Real;


            return l.With(l.Real + r);
        }

        public static INumber Subtract(INumber x, INumber y)
        {
            if (!(x is IUnit) && !(y is IUnit))
            {
                return new Float(x.Real - y.Real);
            }

            var l = x as IUnit;
            var r = (y as IUnit)?.To(l) ?? y.Real;

            return l.With(l.Real - r);
        }

        public static INumber Divide(INumber x, INumber y)
        {
            if (!(x is IUnit) && !(y is IUnit))
            {
                return new Float(x.Real / y.Real);
            }

            var l = (x as IUnit);
            var r = (y as IUnit)?.To(l) ?? y.Real;
            
            return l.With(l.Real / r);
        }

        public static INumber Pow(INumber x, INumber y)
        {
            var result = Math.Pow(x.Real, y.Real);

            if (!(x is IUnit) && !(y is IUnit))
            {
                return new Float(result);
            }
            else
            {
                var unit = (IUnit)x;

                return new Unit<double>(
                    quantity : Math.Pow(x.Real, y.Real),
                    prefix   : unit.Prefix, 
                    type     : unit.Type,
                    power    : unit.Power + ((int)y.Real - 1));
            }

        }

        public static INumber Modulus(INumber x, INumber y)
            => new Float(x.Real % y.Real);
        
    }
}
