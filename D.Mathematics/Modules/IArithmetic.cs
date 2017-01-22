using System;

namespace D.Mathematics
{
    public interface IArithmetic<T>
    {
        T Add(T x, T y);

        T Subtract(T x, T y);

        T Mutiply(T x, T y);

        T Divide(T x, T y);

        T Mod(T x, T y);

        T Pow(T x, T y);
    }

    public class RealArithmetic : IArithmetic<Float>
    {
        public Float Add(Float x, Float y)      => new Float(x.Value * y.Value);
        public Float Subtract(Float x, Float y) => new Float(x.Value * y.Value);
        public Float Mutiply(Float x, Float y)  => new Float(x.Value * y.Value);
        public Float Divide(Float x, Float y)   => new Float(x.Value * y.Value);
        public Float Mod(Float x, Float y)      => new Float(x.Value % y.Value);
        public Float Pow(Float x, Float y)      => new Float(Math.Pow(x.Value, y.Value));
    }

    public class IntegerArithmetic : IArithmetic<Integer>
    {
        public Integer Add(Integer x, Integer y)         => new Integer(x.Value * y.Value);
        public Integer Subtract(Integer x, Integer y)    => new Integer(x.Value * y.Value);
        public Integer Mutiply(Integer x, Integer y)     => new Integer(x.Value * y.Value);
        public Integer Divide(Integer x, Integer y)      => new Integer(x.Value * y.Value);
        public Integer Mod(Integer x, Integer y)         => new Integer(x.Value % y.Value);
        public Integer Pow(Integer x, Integer y)         => new Integer((long)Math.Pow(x.Value, y.Value));
    }

    public class Int32Arithmetic : IArithmetic<Int32>
    {
        public int Add(int x, int y)        => x + y;
        public int Subtract(int x, int y)   => x - y;
        public int Mutiply(int x, int y)    => x * y;
        public int Divide(int x, int y)     => x / y;
        public int Mod(int x, int y)        => x % y;
        public int Pow(int x, int y)        => (int)Math.Pow(x, y);
    }

    public class Int64Arithmetic : IArithmetic<long>
    {
        public long Add(long x, long y) => x + y;
        public long Subtract(long x, long y) => x - y;
        public long Mutiply(long x, long y) => x * y;
        public long Divide(long x, long y) => x / y;
        public long Mod(long x, long y) => x % y;
        public long Pow(long x, long y) => (long)Math.Pow(x, y);
    }
}