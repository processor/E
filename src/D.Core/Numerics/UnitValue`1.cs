using System;
using System.Globalization;
using System.Numerics;

using E.Syntax;

namespace E.Units;

// AKA quantity
public readonly struct Quantity<T>(T value, UnitInfo unit) : IQuantity<T>, IEquatable<Quantity<T>>, ISpanFormattable
    where T : unmanaged, INumberBase<T>
{
    // 1
    public T Value { get; } = value;

    // g
    public UnitInfo Unit { get; } = unit;

    #region With

    public readonly Quantity<T> With(T value) => new (value, Unit);

    public readonly Quantity<T> With(T value, UnitInfo unit) => new (value, unit);

    #endregion

    #region Conversions

    public readonly T To(UnitInfo targetUnit)
    {
        if (Unit.Dimension != targetUnit.Dimension)
        {
            throw new Exception($"Must be the same dimension. Was {targetUnit.Dimension}.");
        }

        var newValue = T.CreateChecked(Value) * (
            (T.CreateChecked(Unit.Prefix.Value) * T.CreateChecked(Unit.DefinitionValue)) /
            (T.CreateChecked(targetUnit.Prefix.Value) * T.CreateChecked(targetUnit.DefinitionValue)
        ));

        return newValue;
    }

    public readonly T1 To<T1>(UnitInfo targetUnit) where T1 : INumberBase<T1>
    {
        if (Unit.Dimension != targetUnit.Dimension)
        {
            throw new Exception($"Must be the same dimension. Was {targetUnit.Dimension}.");
        }

        // kg   = 1000
        // g    = 0
        // mg   = 0.001

        // kg -> mg = 1,000,000     kg.units / mg.units
        // mg -> kg = .0000001      mg.units / kg.units

        // Type Conversions ft -> m, etc 


        var newValue = T1.CreateChecked(Value) * (
            (T1.CreateChecked(Unit.Prefix.Value) * T1.CreateChecked(Unit.DefinitionValue)) /
            (T1.CreateChecked(targetUnit.Prefix.Value) * T1.CreateChecked(targetUnit.DefinitionValue)
        ));

        return newValue;
    }

    #endregion

    #region INumber

    ObjectType IObject.Kind => ObjectType.UnitValue;

    double INumber.Real
    {
        get
        {
            var result = double.CreateChecked(Value);

            if (Unit.DefinitionUnit is Number definitionUnit)
            {
                result *= definitionUnit.Value;
            }

            return result;
        }
    }

    T1 INumber.As<T1>() => T1.CreateChecked(Value);

    #endregion

    // No space between units...

    public static Quantity<T> Wrap(T value)
    {
        return new Quantity<T>(value, UnitInfo.None);
    }        

    public static Quantity<T> Parse(string text)
    {
        if ((char.IsDigit(text[0]) || text[0] == '-'))
        {
            var syntax = Parsing.Parser.Parse(text);

            if (syntax is UnitValueSyntax { Expression: NumberLiteralSyntax numberLiteral } unitValue)
            {

                // 1 g
                // 1g
                // 1.1g
                // 1px

                T value = T.Parse(numberLiteral.Text, NumberStyles.Float, CultureInfo.InvariantCulture);

                var type = UnitInfo.TryParse(unitValue.UnitName, out UnitInfo? unitType)
                    ? unitType
                    : new UnitInfo(unitValue.UnitName);

                return new Quantity<T>(value, type!.WithExponent(unitValue.UnitPower));
            }
            else if (syntax is NumberLiteralSyntax number)
            {
                T value = T.Parse(number.Text, NumberStyles.Float, CultureInfo.InvariantCulture);

                return new Quantity<T>(value, UnitInfo.None);
            }
            else
            {
                throw new Exception($"Invalid unit. Was {text}");
            }
        }
        else
        {
            UnitInfo type = UnitInfo.TryParse(text, out UnitInfo? unitType)
                ? unitType
                : new UnitInfo(text);

            return new Quantity<T>(T.One, type);
        }
    }

    public void Deconstruct(out T value, out string unitName)
    {
        (value, unitName) = (Value, Unit.Name);
    }

    public bool Equals(Quantity<T> other)
    {
        return Value.Equals(other.Value)
            && Unit.Equals(other.Unit);
    }

    public override bool Equals(object? obj)
    {
        return obj is Quantity<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Unit);
    }

    public readonly override string ToString()
    {
        return string.Create(CultureInfo.InvariantCulture, $"{Value}{Unit}");
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return destination.TryWrite(CultureInfo.InvariantCulture, $"{Value}{Unit}", out charsWritten);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString();
    }

    public static bool operator ==(Quantity<T> left, Quantity<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Quantity<T> left, Quantity<T> right)
    {
        return !left.Equals(right);
    }
}

// 1s   = (1)(1)second
// m³   = (1) m^3  AREA
// ms   = (1/1000) * (1) s


// A dimension is a measure of a physical variable (without numerical values),
// while a unit is a way to assign a number or measurement to that dimension.