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
        if (typeof(T) == typeof(Number)) return (Arithmetic<T>)((object)F64Arithmetic.Default);
        if (typeof(T) == typeof(double)) return (Arithmetic<T>)((object)F64Arithmetic.Default);
        if (typeof(T) == typeof(int))    return (Arithmetic<T>)((object)I32Arithmetic.Default);
        if (typeof(T) == typeof(long))   return (Arithmetic<T>)((object)I64Arithmetic.Default);

        throw new Exception($"No arithmetic provider for {typeof(T).Name}");
    }

    public static INumberObject Multiply(INumberObject x, INumberObject y)
    {
        if (x is not IQuantity && y is not IQuantity)
        {
            return new Number(x.Real * y.Real);
        }

        var l = (IQuantity)x;
        var r = (y as IQuantity<double>)?.To<double>(l.Unit) ?? y.Real;

        return y is IQuantity yValue
            ? Quantity.Create(l.Real * r, type: l.Unit.WithExponent(l.Unit.Exponent + yValue.Unit.Exponent))
            : Quantity.Create(l.Real * r, l.Unit);
                
    }
    
    public static INumberObject Add(INumberObject x, INumberObject y)
    {
        if (!(x is IQuantity) && !(y is IQuantity))
        {
            return new Number(x.Real + y.Real);
        }

        var l = (IQuantity)x;
        var r = (y as IQuantity)?.To<double>(l.Unit) ?? y.Real;

        return Quantity.Create(l.Real + r, l.Unit);
    }

    public static INumberObject Subtract(INumberObject x, INumberObject y)
    {
        if (x is not IQuantity && y is not IQuantity)
        {
            return new Number(x.Real - y.Real);
        }

        var l = (IQuantity)x;
        var r = (y as IQuantity)?.To<double>(l.Unit) ?? y.Real;

        return Quantity.Create(l.Real - r, l.Unit);
    }

    public static INumberObject Divide(INumberObject x, INumberObject y)
    {
        if (x is not IQuantity && y is not IQuantity)
        {
            return new Number(x.Real / y.Real);
        }

        var l = (IQuantity)x;
        var r = (y as IQuantity)?.To<double>(l.Unit) ?? y.Real;
            
        return Quantity.Create(l.Real / r, l.Unit);
    }

    public static INumberObject Pow(INumberObject x, INumberObject y)
    {
        var result = Math.Pow(x.Real, y.Real);

        if (x is not IQuantity && y is not IQuantity)
        {
            return new Number(result);
        }
        else
        {
            var unit = (IQuantity)x;

            return new Quantity<double>(
                value : Math.Pow(x.Real, y.Real),
                unit  : unit.Unit.WithExponent(unit.Unit.Exponent + ((int)y.Real - 1))
            );
        }
    }

    public static INumberObject Modulus(INumberObject x, INumberObject y) => new Number(x.Real % y.Real);
}