﻿using System;

using E.Expressions;

namespace E;

public interface INumber : IExpression
{
    double Real { get; } // Quantity

    T As<T>() where T : struct, IComparable<T>, IEquatable<T>;
}