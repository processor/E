using System;

namespace E;

public interface INumeric<T> : INumberObject
    where T : struct, IComparable<T>, IEquatable<T>
{
    T Value { get; }

    // Operators (Add, Subtract, Multiply, Divide)
    // Comparisons
}