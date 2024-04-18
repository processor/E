using System;
using System.Numerics;

namespace E;

public readonly struct Complex<T>(T real, T imaginary) : INumber
    where T : unmanaged, INumberBase<T>
{
    public T Real { get; } = real;

    public T Imaginary { get; } = imaginary;

    readonly ObjectType IObject.Kind => ObjectType.Complex;

    #region INumeric

    readonly double INumber.Real => throw new Exception("Complexes may not be cast");

    readonly TA INumber.As<TA>() => throw new Exception("Complexes may not be cast");

    #endregion
}