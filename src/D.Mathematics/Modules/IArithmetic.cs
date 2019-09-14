using System;

namespace D.Mathematics
{
    public abstract class Arithmetic<T>
        where T: unmanaged
    {
        public abstract T Add(T x, T y);

        public abstract T Subtract(T x, T y);

        public abstract T Mutiply(T x, T y);

        public abstract T Divide(T x, T y);

        public abstract T Mod(T x, T y);

        public abstract T Pow(T x, T y);
    }

    public sealed class RealArithmetic : Arithmetic<double>
    {
        public override double Add(double x, double y)      => x + y;
        public override double Subtract(double x, double y) => x - y;
        public override double Mutiply(double x, double y)  => x * y;
        public override double Divide(double x, double y)   => x / y;
        public override double Mod(double x, double y)      => x % y;
        public override double Pow(double x, double y)      => Math.Pow(x, y);
    }

    public sealed class IntegerArithmetic : Arithmetic<long>
    {
        public override long Add(long x, long y)         => x + y;
        public override long Subtract(long x, long y)    => x - y;
        public override long Mutiply(long x, long y)     => x * y;
        public override long Divide(long x, long y)      => x / y;
        public override long Mod(long x, long y)         => x % y;
        public override long Pow(long x, long y)         => (long)Math.Pow(x, y);
    }

    public sealed class Int32Arithmetic : Arithmetic<Int32>
    {
        public override int Add(int x, int y)        => x + y;
        public override int Subtract(int x, int y)   => x - y;
        public override int Mutiply(int x, int y)    => x * y;
        public override int Divide(int x, int y)     => x / y;
        public override int Mod(int x, int y)        => x % y;
        public override int Pow(int x, int y)        => (int)Math.Pow(x, y);
    }           

    public sealed class Int64Arithmetic : Arithmetic<long>
    {
        public override long Add(long x, long y) => x + y;
        public override long Subtract(long x, long y) => x - y;
        public override long Mutiply(long x, long y) => x * y;
        public override long Divide(long x, long y) => x / y;
        public override long Mod(long x, long y) => x % y;
        public override long Pow(long x, long y) => (long)Math.Pow(x, y);
    }
}