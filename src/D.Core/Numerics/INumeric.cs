using System;

namespace D
{
    public interface INumeric<T> : INumber
        where T : struct, IComparable<T>, IEquatable<T>
    {
        T Value { get; }

        // Operators (Add, Subtract, Multiply, Divide)
        // Comparisions
    }
}