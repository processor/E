using E.Units;

namespace E.Mathematics;

public static class Arithmetic
{
    // abs
    // intergrate
    // ceiling
    // floor

    public static Arithmetic<T> GetProvider<T>()
        where T: unmanaged
    {
        if (typeof(T) == typeof(Number))  return (Arithmetic<T>)((object)RealArithmetic.Default);
        if (typeof(T) == typeof(int))     return (Arithmetic<T>)((object)Int32Arithmetic.Default);
        if (typeof(T) == typeof(long))    return (Arithmetic<T>)((object)Int64Arithmetic.Default);

        throw new Exception($"No arithmetic provider for {typeof(T).Name}");
    }

    public static INumber Multiply(INumber x, INumber y)
    {
        if (x is not IUnitValue && y is not IUnitValue)
        {
            return new Number(x.Real * y.Real);
        }

        var l = (IUnitValue)x;
        var r = (y as IUnitValue)?.To(l.Unit) ?? y.Real;

        return y is IUnitValue yValue
            ? UnitValue.Create(l.Real * r, type: l.Unit.WithExponent(l.Unit.Power + yValue.Unit.Power))
            : UnitValue.Create(l.Real * r, l.Unit);
                
    }
    
    public static INumber Add(INumber x, INumber y)
    {
        if (!(x is IUnitValue) && !(y is IUnitValue))
        {
            return new Number(x.Real + y.Real);
        }

        var l = (IUnitValue)x;
        var r = (y as IUnitValue)?.To(l.Unit) ?? y.Real;

        return UnitValue.Create(l.Real + r, l.Unit);
    }

    public static INumber Subtract(INumber x, INumber y)
    {
        if (x is not IUnitValue && y is not IUnitValue)
        {
            return new Number(x.Real - y.Real);
        }

        var l = (IUnitValue)x;
        var r = (y as IUnitValue)?.To(l.Unit) ?? y.Real;

        return UnitValue.Create(l.Real - r, l.Unit);
    }

    public static INumber Divide(INumber x, INumber y)
    {
        if (x is not IUnitValue && y is not IUnitValue)
        {
            return new Number(x.Real / y.Real);
        }

        var l = (IUnitValue)x;
        var r = (y as IUnitValue)?.To(l.Unit) ?? y.Real;
            
        return UnitValue.Create(l.Real / r, l.Unit);
    }

    public static INumber Pow(INumber x, INumber y)
    {
        var result = Math.Pow(x.Real, y.Real);

        if (x is not IUnitValue && y is not IUnitValue)
        {
            return new Number(result);
        }
        else
        {
            var unit = (IUnitValue)x;

            return new UnitValue<double>(
                value : Math.Pow(x.Real, y.Real),
                unit  : unit.Unit.WithExponent(unit.Unit.Power + ((int)y.Real - 1))
            );
        }
    }

    public static INumber Modulus(INumber x, INumber y) => new Number(x.Real % y.Real);
}