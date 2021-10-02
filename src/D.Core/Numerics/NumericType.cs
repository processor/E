namespace E;

public enum NumericType
{
    Integer     = ObjectType.Int64,       // {…, -2, -1, 0, 1, 2,…}
    Rational    = ObjectType.Rational,    // ratio (i.e. 1/3)
    Float       = ObjectType.Float64,     // represents a physical quantity along a continuous line

    // Irrational  = Kind.Irrational,
    // Imaginary   = Kind.Imaginary,
    // Complex     = Kind.Complex
}

// PI is an irrational number

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
