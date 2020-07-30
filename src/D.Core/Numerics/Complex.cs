using System;

namespace D
{
    public readonly struct Complex<T> : INumber
        where T: unmanaged, IComparable<T>, IEquatable<T>
    {
        public Complex(T real, T imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public T Real { get; }

        public T Imaginary { get; }

        readonly ObjectType IObject.Kind => ObjectType.Complex;

        #region INumeric

        readonly double INumber.Real => Convert.ToDouble(Real);

        readonly TA INumber.As<TA>() => throw new Exception("Complexes may not be cast");

        #endregion
    }
}