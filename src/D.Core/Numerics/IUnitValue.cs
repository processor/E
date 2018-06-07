using System;

namespace D.Units
{
    // 8.314 m^3 Pa / mol / K
    // 8.314 (m^3 Pa) / (mol K)
    // 8.314 (m^3 * Pa) / (mol * K)

    // Unit 8.314 (m^3 Pa) / (mol K)

    public interface IUnitValue : INumber
    {
        UnitType Type { get; }

        double To(IUnitValue unit);

        double To(UnitType unit);

        UnitValue<T> With<T>(T quantity)
            where T : unmanaged, IComparable<T>, IEquatable<T>;

        UnitValue<T> With<T>(T quantity, UnitType type)
           where T : unmanaged, IComparable<T>, IEquatable<T>;
    }
}