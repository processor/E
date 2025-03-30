using System;
using System.Numerics;

namespace E;

public readonly struct Complex<T>(T real, T imaginary) : INumberObject
    where T : unmanaged, INumberBase<T>
{
    public T Real { get; } = real;

    public T Imaginary { get; } = imaginary;

    readonly ObjectType IObject.Kind => ObjectType.Complex;

    #region INumeric

    readonly TA INumberObject.As<TA>() => throw new Exception("Complexes may not be cast");

    #endregion
}