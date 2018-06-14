namespace D.Imaging
{
    public readonly struct Rgb : IColor, IObject
    {
        public Rgb(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        public double R { get; }

        public double G { get; }

        public double B { get; }

        public Kind Kind => Kind.Object;
    }

    public class RgbConstructor : IFunction
    {
        public Parameter[] Parameters => throw new System.NotImplementedException();

        public string Name => "rgb";

        public Kind Kind => throw new System.NotImplementedException();

        public IObject Invoke(IArguments args)
        {
            return new Rgb(
               r: (args[0] as INumber).Real,
               g: (args[1] as INumber).Real,
               b: (args[2] as INumber).Real
           );
        }
    }
}