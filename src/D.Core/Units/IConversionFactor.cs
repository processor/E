using System;
using System.Numerics;

namespace E.Units;

public interface IConversionFactor
{
    UnitInfo Unit { get; }

    ReadOnlySpan<byte> Value { get; }

    Func<T, T> Compile<T>()
        where T : INumberBase<T>;
}
