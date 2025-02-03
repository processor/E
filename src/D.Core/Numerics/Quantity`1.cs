using System;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

using E.Parsing;
using E.Syntax;

namespace E.Units;

public readonly struct Quantity<T>(T value, UnitInfo unit) : IQuantity<T>, IEquatable<Quantity<T>>, ISpanFormattable
    where T : unmanaged, INumberBase<T>
{
    // | 1
    [JsonPropertyName("value")]
    public T Value { get; } = value;

    // | g
    public UnitInfo Unit { get; } = unit;

    #region With

    public readonly Quantity<T> With(T value) => new(value, Unit);

    public readonly Quantity<T> With(T value, UnitInfo unit) => new(value, unit);

    #endregion

    #region Conversions

    public readonly T To(UnitInfo targetUnit)
    {
        return UnitConverterFactory<T>.Default.Get(Unit, targetUnit)(T.CreateChecked(Value)); 
    }

    public readonly T To(UnitInfo targetUnit, UnitConverterFactory<T> converterFactory)
    {
        return converterFactory.Get(Unit, targetUnit)(Value);
    }

    public readonly T1 To<T1>(UnitInfo targetUnit)
        where T1 : INumberBase<T1>
    {
        return UnitConverterFactory<T1>.Default.Get(Unit, targetUnit)(T1.CreateChecked(Value));
    }

    #endregion

    #region INumber

    ObjectType IObject.Kind => ObjectType.UnitValue;

    double INumberObject.Real
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

    T1 INumberObject.As<T1>() => T1.CreateChecked(Value);

    #endregion

    // No space between units...

    public static Quantity<T> Wrap(T value)
    {
        return new Quantity<T>(value, UnitInfo.None);
    }        

    public static Quantity<T> Parse(string text)
    {
        if ((char.IsDigit(text[0]) || text[0] is '-'))
        {
            var syntax = Parser.Parse(text);

            if (syntax is QuantitySyntax { Expression: NumberLiteralSyntax numberLiteral } unitValue)
            {
                // 1 g
                // 1g
                // 1.1g
                // 1px

                T value = T.Parse(numberLiteral.Text, NumberStyles.Float, CultureInfo.InvariantCulture);

                var type = UnitInfo.TryParse(unitValue.UnitName, out UnitInfo? unit)
                    ? unit
                    : UnitInfo.Create(unitValue.UnitName);

                return new Quantity<T>(value, type.WithExponent(unitValue.UnitExponent));
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
            UnitInfo type = UnitInfo.TryParse(text, out UnitInfo? unit)
                ? unit
                : UnitInfo.Create(text);

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