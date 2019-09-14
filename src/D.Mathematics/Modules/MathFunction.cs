using System;

namespace D.Mathematics
{
    public class MathFunction : IFunction
    {
        private readonly Func<double, double> func;

        public MathFunction(string name, Func<double, double> func)
        {
            Name = name;
            Parameters = new[] { Parameter.Get(ObjectType.Number) };

            this.func = func;
        }

        public string Name { get; }

        public Parameter[] Parameters { get; }

        ObjectType IObject.Kind => ObjectType.Function;

        public object Invoke(IArguments args)
        { 
            var arg0 = (INumber)args[0];

            return new Number(func.Invoke(arg0.Real));
        }
    }
}