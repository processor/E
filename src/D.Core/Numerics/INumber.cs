using System;

using D.Expressions;

namespace D
{
    public interface INumber : IExpression
    {
        double Real { get; } // Quantity

        T As<T>() where T: struct, IComparable<T>, IEquatable<T>;
    }
}