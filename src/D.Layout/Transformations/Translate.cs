using System.Numerics;

namespace E.Transformations;

public readonly struct Translate<T>(
    T x,
    T y,
    T z) : ITransform where T : unmanaged, INumberBase<T>
{
    // 10%
    // 100vh
    // 100%

    public T X { get; } = x;

    public T Y { get; } = y;

    public T Z { get; } = z;
}

// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-function/translate