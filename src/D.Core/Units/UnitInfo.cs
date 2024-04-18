using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

using E.Expressions;
using E.Symbols;

namespace E.Units;

using static Dimension;
using static UnitFlags;

public sealed class UnitInfo : IEquatable<UnitInfo>, IObject, ISpanFormattable
{
    public static readonly UnitInfo None = new(0, string.Empty, Dimension.None);

    #region Angles (Plane & Solid)

    private static readonly Symbol π = Symbol.Variable("π");

    public static readonly UnitInfo Radian    = new(UnitType.Radian,    "rad", Angle, Base);
    public static readonly UnitInfo Steradian = new(UnitType.Steradian, "sr",  SolidAngle, Base);

    public static readonly UnitInfo Degree    = new(UnitType.Degree,  "deg",  Angle, 1,   Expression.Divide(π, Quantity.Create(180d, Radian))); // π / 180 rad
    public static readonly UnitInfo Gradian   = new(UnitType.Gradian, "grad", Angle, 0.9, Degree); // 400 per circle
    public static readonly UnitInfo Turn      = new(UnitType.Turn,    "turn", Angle, 360, Degree); // 1 per circle

    #endregion

    #region Electromagism / Energy

    // Defined Under Electromagism Units

    #endregion

    #region Frequency

    public static readonly UnitInfo Hertz = new(UnitType.Hz, "Hz", Frequency, SI);
    public static readonly UnitInfo kHz   = Hertz.WithPrefix(SIPrefix.k, UnitType.kHz);

    // rpm

    #endregion

    #region Length

    public static readonly UnitInfo Meter = new(UnitType.Meter, "m", Length, SI | Base);  // m
    public static readonly UnitInfo Mm    = Meter.WithPrefix(SIPrefix.m, UnitType.Millimeter);  // mm
    public static readonly UnitInfo Cm    = Meter.WithPrefix(SIPrefix.c, UnitType.Centimeter);  // cm

    public static readonly UnitInfo Inch  = new(UnitType.Inch, "in", Length, Imperial);
    public static readonly UnitInfo Foot  = new(UnitType.Foot, "ft", Length, 12, Inch);

    public static readonly UnitInfo Parsec           = new(UnitType.Parsec,           "parsec", Length, Base);
    public static readonly UnitInfo AstronomicalUnit = new(UnitType.AstronomicalUnit, "au",     Length);

    #endregion

    #region Mass

    public static readonly UnitInfo Gram     = new(UnitType.Gram, "g", Mass, SI | Base);
    public static readonly UnitInfo Kilogram = Gram.WithPrefix(SIPrefix.k, UnitType.Kilogram);

    // Standard is KG

    public static readonly UnitInfo Pound = new(UnitType.Pound, "lb", Mass, 453.592d); // lb = 453.592g

    #endregion

    public static readonly UnitInfo Mole = new(UnitType.Mole, "mol", AmountOfSubstance, SI | Base);

    // Luminocity -

    public static readonly UnitInfo Candela = new(UnitType.Candela, "cd", LuminousIntensity, SI | Base);

    #region Time

    // 5.39 x 10−44 s

    public static readonly UnitInfo Second  = new(UnitType.Second,  "s",   Time, SI | Base);  // s
    public static readonly UnitInfo Minute  = new(UnitType.Minute,  "min", Time, 60d);
    public static readonly UnitInfo Hour    = new(UnitType.Hour,    "h",   Time, 60d * 60d);
    public static readonly UnitInfo Week    = new(UnitType.Week,    "wk",  Time, 60d * 60d * 24 * 7);

    #endregion

    // Pressure - 
    public static readonly UnitInfo Pascal = new(UnitType.Pascal, "Pa", Pressure);
     
    // Volume - 
    public static readonly UnitInfo Liter = new(UnitType.Liter, "L", Length); //  1,000 cubic centimeters


    public static readonly UnitInfo Katal = new(UnitType.Katal, "kat", CatalyticActivity);

    public static readonly UnitInfo SquareMeters = Meter.WithExponent(2, UnitType.SquareMeters);

    // Dimensionless

    public static readonly UnitInfo Percent = new(UnitType.Percent, "%", Dimension.None, 1, new Number(0.01)); // 1/100

    public UnitInfo(string name) // e.g. px
    {
        Name = name;
        Dimension = Dimension.None;
        DefinitionValue = 1;
    }

    public UnitInfo(string symbol, int exponent) 
    {
        Name            = symbol;
        Dimension       = Dimension.None;
        DefinitionValue = 1;
        Power           = exponent;
    }

    public UnitInfo(string name, Dimension dimension, UnitFlags flags = UnitFlags.None)
    {
        Name = name;
        Dimension = dimension;
        Flags = flags;
        DefinitionValue = 1;
    }

    public UnitInfo(UnitType type, string name, Dimension dimension, UnitFlags flags = UnitFlags.None)
    {
        Id = (int)type;
        Name = name;
        Dimension = dimension;
        Flags = flags;
        DefinitionValue = 1;
    }

    public UnitInfo(string symbol, Dimension dimension, double definitionValue)
    {
        Name            = symbol;
        Dimension       = dimension;
        DefinitionValue = definitionValue;
    }

    public UnitInfo(UnitType id, string symbol, Dimension dimension, double definitionValue)
    {
        Id              = (int)id;
        Name            = symbol;
        Dimension       = dimension;
        DefinitionValue = definitionValue;
    }

    public UnitInfo(UnitType id, SIPrefix prefix, string name, Dimension dimension, double definitionValue, int power)
    {
        Id              = (int)id;
        Prefix          = prefix;
        Name            = name;
        Dimension       = dimension;
        DefinitionValue = definitionValue;
        Power           = power;
    }

    public UnitInfo(string name, Dimension dimension, double definitionValue, IObject definitionUnit)
    {
        Name            = name;
        Dimension       = dimension;
        DefinitionValue = definitionValue;
        DefinitionUnit  = definitionUnit;
    }

    public UnitInfo(UnitType id, string name, Dimension dimension, double definitionValue, IObject definitionUnit)
    {
        Id              = (int)id;
        Name            = name;
        Dimension       = dimension;
        DefinitionValue = definitionValue;
        DefinitionUnit  = definitionUnit;
    }

    public UnitInfo(string name, Dimension id, IObject definitionUnit)
    {
        Name = name;
        Dimension = id;
        DefinitionValue = 1;
        DefinitionUnit = definitionUnit;
    }

    public int Id { get; }

    public SIPrefix Prefix { get; } = SIPrefix.None;

    public bool IsBaseUnit => Flags.HasFlag(Base);

    public Dimension Dimension { get; }

    public string Name { get; }

    public int Power { get; } = 1;
                
    public double DefinitionValue { get; }

    public IObject? DefinitionUnit { get; }

    public IConverter<double, double> BaseConverter => new UnitConverter(DefinitionValue);

    private UnitFlags Flags { get; }

    ObjectType IObject.Kind => ObjectType.Unit;

    public bool IsMetric => Flags.HasFlag(SI);

    public bool HasDimension => Dimension != Dimension.None;

    public UnitInfo WithPrefix(SIPrefix prefix, UnitType type = default)
    {
        return new UnitInfo(type, prefix, Name, Dimension, DefinitionValue, Power);
    }

    public UnitInfo WithExponent(int exponent, UnitType type = default)
    {
        if (Power == exponent) return this;

        return new UnitInfo(0, Prefix, Name, Dimension, DefinitionValue, exponent);
    }

    public static UnitInfo Get(ReadOnlySpan<char> name)
    {
        if (!TryParse(name, out var unit))
        {
            throw new Exception($"Unit '{name}' was not found");            
        }

        return unit;
    }

    public static bool TryParse(ReadOnlySpan<char> name, [NotNullWhen(true)] out UnitInfo? type)
    {
        if (UnitSet.Default.TryGet(name, out type))
        {
            return true;
        }
        else if (SIPrefix.TryParseSymbol(name, out SIPrefix prefix))
        {
            var unitName = name[prefix.Length..];

            if (UnitSet.Default.TryGet(unitName, out var unitType) && unitType.IsMetric)
            {
                type = unitType.WithPrefix(prefix);

                return true;
            }
        }

        type = null;

        return false;
    }

    [SkipLocalsInit]
    public override string ToString()
    {
        if (Prefix.Value is 1 && Power is 1)
        {
            return Name;
        }

        var sb = new ValueStringBuilder(stackalloc char[16]);

        WriteTo(ref sb);

        return sb.ToString();
    }

    private void WriteTo(ref ValueStringBuilder sb)
    {
        if (Prefix.Value != 1)
        {
            sb.Append(Prefix.Name); // e.g. k
        }

        sb.Append(Name);   // e.g. g

        if (Power is not 1)
        {
            new Superscript(Power).WriteTo(ref sb);
        }
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        if (Prefix.Value is 1 && Power is 1)
        {
            if (destination.Length >= Name.Length)
            {
                Name.CopyTo(destination);

                charsWritten = Name.Length;

                return true;
            }
            else
            {
                charsWritten = 0;

                return false;
            }
        }

        var sb = new ValueStringBuilder(destination);

        WriteTo(ref sb);

        if (destination.Length >= sb.Length)
        {
            charsWritten = sb.Length;

            sb.Dispose();

            return true;
        }
        else
        {
            charsWritten = destination.Length;

            sb.Dispose();

            return false;
        }
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString();
    }

    public bool Equals(UnitInfo? other)
    {
        if (other is null) return this is null;

        if (ReferenceEquals(this, other)) return true;

        return Prefix.Equals(other.Prefix) 
            && string.Equals(Name, other.Name, StringComparison.Ordinal) 
            && Power == other.Power;
    }

    public override bool Equals(object? obj)
    {
        return obj is UnitInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Prefix, Name, Power);
    }
}

// Note: Only SI units may be prefixed.