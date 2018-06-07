using System;

namespace D.Units
{
    public interface IUnitValue : INumber
    {
        UnitType Type { get; }

        double To(IUnitValue unit);

        double To(UnitType unit);
    }

    public interface IUnitValue<T> : IUnitValue
        where T : struct, IComparable<T>, IEquatable<T>, IFormattable
    {
        UnitValue<T> With(T quantity);

        UnitValue<T> With(T quantity, UnitType type);
    }
}

// 8.314 m^3 Pa / mol / K
// 8.314 (m^3 Pa) / (mol K)
// 8.314 (m^3 * Pa) / (mol * K)

// Unit 8.314 (m^3 Pa) / (mol K)