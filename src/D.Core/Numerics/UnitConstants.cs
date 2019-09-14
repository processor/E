using System;

namespace D.Units
{
    internal static class UnitConstants<T>
    {
        public static readonly T One = (T)Convert.ChangeType(1, typeof(T));

    }
}

// 1s   = (1)(1)second
// m³   = (1) m^3  AREA
// ms   = (1/1000) * (1) s


// A dimension is a measure of a physical variable (without numerical values),
// while a unit is a way to assign a number or measurement to that dimension.