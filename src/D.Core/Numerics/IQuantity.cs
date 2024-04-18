using System.Numerics;

namespace E.Units;

public interface IQuantity : INumber 
{
    UnitInfo Unit { get; }

    T1 To<T1>(UnitInfo unit) where T1 : INumberBase<T1>;
}

public interface IQuantity<T> : IQuantity
    where T : unmanaged, INumberBase<T>
{
    Quantity<T> With(T quantity);

    Quantity<T> With(T quantity, UnitInfo type);
}


// 8.314 m^3 Pa / mol / K
// 8.314 (m^3 Pa) / (mol K)
// 8.314 (m^3 * Pa) / (mol * K)

// Unit 8.314 (m^3 Pa) / (mol K)