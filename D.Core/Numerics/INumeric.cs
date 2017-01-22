using System;

namespace D
{
    using Expressions;

    public interface INumeric<T> : INumber
        where T : struct, IComparable<T>
    {
        T Value { get; }
    }

    public interface INumber : IExpression
    {
        double Real { get; } // Quantity

        T As<T>() where T: IEquatable<T>, IFormattable;
    }

    public enum NumericKind
    {
        Integer     = Kind.Integer,           // {…, -2, -1, 0, 1, 2,…}
        Rational    = Kind.Rational,          // ratio (i.e. 1/3)
        Float       = Kind.Float,             // represents a physical quantity along a continuous line

        // Irrational  = Kind.Irrational,
        // Imaginary   = Kind.Imaginary,
        // Complex     = Kind.Complex
    }

    // PI is an irrational number
}


// 
/*
BigInteger
Complex
Decimal
Fixed point
Floating point
Double precision 
Extended precision
Half precision 
Minifloat 
Octuple precision       // https://en.wikipedia.org/wiki/Octuple-precision_floating-point_format
Quadruple precision
Single precision 
Integer signedness
Interval 
Rational
*/
