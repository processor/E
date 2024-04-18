using System.Numerics;

using E.Expressions;

namespace E;

public interface INumber : IExpression
{
    double Real { get; } // Quantity

    T As<T>() where T : INumberBase<T>;
}