using System;

namespace D.Mathematics
{
    public interface IArithmetic<T>
        where T: unmanaged
    {
        T Add(T x, T y);

        T Subtract(T x, T y);

        T Mutiply(T x, T y);

        T Divide(T x, T y);

        T Mod(T x, T y);

        T Pow(T x, T y);
    }

    public class RealArithmetic : IArithmetic<double>
    {
        public double Add(double x, double y)      => x + y;
        public double Subtract(double x, double y) => x - y;
        public double Mutiply(double x, double y)  => x * y;
        public double Divide(double x, double y)   => x / y;
        public double Mod(double x, double y)      => x % y;
        public double Pow(double x, double y)      => Math.Pow(x, y);
    }

    public class IntegerArithmetic : IArithmetic<long>
    {
        public long Add(long x, long y)         => x + y;
        public long Subtract(long x, long y)    => x - y;
        public long Mutiply(long x, long y)     => x * y;
        public long Divide(long x, long y)      => x / y;
        public long Mod(long x, long y)         => x % y;
        public long Pow(long x, long y)         => (long)Math.Pow(x, y);
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