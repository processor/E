using System;

namespace D.Units
{
    public interface IUnitValue : INumber
    {
        UnitInfo Unit { get; }

        double To(UnitInfo unit);
    }

    public interface IUnitValue<T> : IUnitValue
        where T : unmanaged, IComparable<T>, IEquatable<T>, IFormattable
    {
        UnitValue<T> With(T quantity);

        UnitValue<T> With(T quantity, UnitInfo type);
    }
}

// 8.314 m^3 Pa / mol / K
// 8.314 (m^3 Pa) / (mol K)
// 8.314 (m^3 * Pa) / (mol * K)

// Unit 8.314 (m^3 Pa) / (mol K)