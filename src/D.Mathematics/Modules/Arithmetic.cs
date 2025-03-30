using System.Numerics;

using E.Units;

namespace E.Mathematics;

public static class Arithmetic
{
    // abs
    // intergrate
    // ceiling
    // floor

    public static Arithmetic<T> GetProvider<T>()
        where T: unmanaged, INumber<T>
    {
        if (typeof(T) == typeof(Number<double>)) return (Arithmetic<T>)((object)F64Arithmetic.Default);
        if (typeof(T) == typeof(double))         return (Arithmetic<T>)((object)F64Arithmetic.Default);
        if (typeof(T) == typeof(int))            return (Arithmetic<T>)((object)I32Arithmetic.Default);
        if (typeof(T) == typeof(long))           return (Arithmetic<T>)((object)I64Arithmetic.Default);

        throw new Exception($"No arithmetic provider for {typeof(T).Name}");
    }

    public static INumberObject Multiply(INumberObject x, INumberObject y)
    {
        if (x is not IQuantity && y is not IQuantity)
        {
            return new Number<double>(x.As<double>() * y.As<double>());
        }

        var l = (IQuantity)x;
        var r = (y as IQuantity<double>)?.To<double>(l.Unit) ?? y.As<double>();

        return y is IQuantity yValue
            ? Quantity.Create(l.As<double>() * r, type: l.Unit.WithExponent(l.Unit.Exponent + yValue.Unit.Exponent))
            : Quantity.Create(l.As<double>() * r, l.Unit);
                
    }
    
    public static INumberObject Add(INumberObject x, INumberObject y)
    {
        if (!(x is IQuantity) && !(y is IQuantity))
        {
            return new Number<double>(x.As<double>() + y.As<double>());
        }

        var l = (IQuantity)x;
        var r = (y as IQuantity)?.To<double>(l.Unit) ?? y.As<double>();

        return Quantity.Create(l.As<double>() + r, l.Unit);
    }

    public static INumberObject Subtract(INumberObject x, INumberObject y)
    {
        if (x is not IQuantity && y is not IQuantity)
        {
            return new Number<double>(x.As<double>() - y.As<double>());
        }

        var l = (IQuantity)x;
        var r = (y as IQuantity)?.To<double>(l.Unit) ?? y.As<double>();

        return Quantity.Create(l.As<double>() - r, l.Unit);
    }

    public static INumberObject Divide(INumberObject x, INumberObject y)
    {
        if (x is not IQuantity && y is not IQuantity)
        {
            return new Number<double>(x.As<double>() / y.As<double>());
        }

        var l = (IQuantity)x;
        var r = (y as IQuantity)?.To<double>(l.Unit) ?? y.As<double>();
            
        return Quantity.Create(l.As<double>() / r, l.Unit);
    }

    public static INumberObject Pow(INumberObject x, INumberObject y)
    {
        var result = Math.Pow(x.As<double>(), y.As<double>());

        if (x is not IQuantity && y is not IQuantity)
        {
            return new Number<double>(result);
        }
        else
        {
            var unit = (IQuantity)x;

            return new Quantity<double>(
                value : Math.Pow(x.As<double>(), y.As<double>()),
                unit  : unit.Unit.WithExponent(unit.Unit.Exponent + ((int)y.As<double>() - 1))
            );
        }
    }

    public static INumberObject Modulus(INumberObject x, INumberObject y)
    {
        // TODO: Consider units

        return new Number<double>(x.As<double>() % y.As<double>());
    }
}