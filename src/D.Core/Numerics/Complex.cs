using System;

namespace D
{
    public readonly struct Complex<T> : INumber
        where T: unmanaged, IComparable<T>
    {
        public Complex(T real, T imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public T Real { get; }

        public T Imaginary { get; }
   
        Kind IObject.Kind => Kind.Complex;

        #region INumeric

        double INumber.Real => Convert.ToDouble(Real);

        TA INumber.As<TA>()
        {
            throw new Exception("Complexes may not be cast");
        }

        #endregion
    }
}