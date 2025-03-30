using System.Numerics;

using E.Expressions;

namespace E;

public interface INumberObject : IExpression
{
    T As<T>() where T : INumberBase<T>;
}