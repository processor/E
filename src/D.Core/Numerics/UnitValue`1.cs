﻿using System;
using System.Globalization;
using System.Numerics;

using E.Syntax;

namespace E.Units;

public readonly struct UnitValue<T>(T value, UnitInfo unit) : IUnitValue<T>, IEquatable<UnitValue<T>>, ISpanFormattable
    where T : unmanaged, INumberBase<T>
{
    // 1
    public T Value { get; } = value;

    // g
    public UnitInfo Unit { get; } = unit;

    #region With

    public readonly UnitValue<T> With(T quantity) => new (quantity, Unit);

    public readonly UnitValue<T> With(T quantity, UnitInfo type) => new (quantity, type);

    #endregion

    #region Conversions

    public readonly double To(UnitInfo targetUnit)
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

        double q = Convert.ToDouble(Value);

        return q * (
            (Unit.Prefix.Value * Unit.DefinitionValue) /
            (targetUnit.Prefix.Value * targetUnit.DefinitionValue)
        );
    }

    #endregion

    #region INumber

    ObjectType IObject.Kind => ObjectType.UnitValue;

    double INumber.Real
    {
        get
        {
            var result = Convert.ToDouble(Value);

            if (Unit.DefinitionUnit is Number definitionUnit)
            {
                result *= definitionUnit.Value;
            }

            return result;
        }
    }

    T1 INumber.As<T1>() => (T1)Convert.ChangeType(Value, typeof(T1));

    #endregion

    // No space between units...

    public static UnitValue<T> Wrap(T value)
    {
        return new UnitValue<T>(value, UnitInfo.None);
    }        

    public static UnitValue<T> Parse(string text)
    {
        if ((char.IsDigit(text[0]) || text[0] == '-'))
        {
            var syntax = Parsing.Parser.Parse(text);

            if (syntax is UnitValueSyntax unitValue)
            {

                // 1 g
                // 1g
                // 1.1g
                // 1px

                double value = double.Parse(((NumberLiteralSyntax)unitValue.Expression).Text);

                var type = UnitInfo.TryParse(unitValue.UnitName, out UnitInfo? unitType)
                    ? unitType!
                    : new UnitInfo(unitValue.UnitName);

                return new UnitValue<T>((T)Convert.ChangeType(value, typeof(T)), type!.WithExponent(unitValue.UnitPower));
            }
            else if (syntax is NumberLiteralSyntax number)
            {
                var value = (T)Convert.ChangeType(double.Parse(number.Text), typeof(T));

                return new UnitValue<T>(value, UnitInfo.None);
            }
            else
            {
                throw new Exception($"Invalid unit. Was {text}");
            }
        }
        else
        {
            UnitInfo type = UnitInfo.TryParse(text, out UnitInfo? unitType)
                ? unitType!
                : new UnitInfo(text);

            return new UnitValue<T>(UnitConstants<T>.One, type);
        }
    }

    public void Deconstruct(out T value, out string unitName)
    {
        (value, unitName) = (Value, Unit.Name);
    }

    public bool Equals(UnitValue<T> other)
    {
        return Value.Equals(other.Value)
            && Unit.Equals(other.Unit);
    }

    public override bool Equals(object? obj)
    {
        return obj is UnitValue<T> other && Equals(other);
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

    public static bool operator ==(UnitValue<T> left, UnitValue<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UnitValue<T> left, UnitValue<T> right)
    {
        return !left.Equals(right);
    }
}

// 1s   = (1)(1)second
// m³   = (1) m^3  AREA
// ms   = (1/1000) * (1) s


// A dimension is a measure of a physical variable (without numerical values),
// while a unit is a way to assign a number or measurement to that dimension.