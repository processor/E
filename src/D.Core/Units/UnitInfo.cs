using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

using E.Expressions;
using E.Symbols;

namespace E.Units;

using static Dimension;
using static UnitFlags;

public sealed class UnitInfo : IEquatable<UnitInfo>, IObject, ISpanFormattable
{
    public static readonly UnitInfo None = new(0, Dimension.None, string.Empty);

    #region Angles (Plane & Solid)

    private static readonly Symbol π = Symbol.Variable("π");

    public static readonly UnitInfo Radian    = new(UnitType.Radian,    Angle,      "rad", flags: Base);
    public static readonly UnitInfo Steradian = new(UnitType.Steradian, SolidAngle, "sr",  flags: Base);

    public static readonly UnitInfo Degree    = new(UnitType.Degree,  Angle, "deg",  Expression.Divide(π, Quantity.Create(180d, Radian))); // π / 180 rad         
    public static readonly UnitInfo Gradian   = new(UnitType.Gradian, Angle, "grad", new ConversionFactor(0.9m, Degree)); // 400 per circle
    public static readonly UnitInfo Turn      = new(UnitType.Turn,    Angle, "turn", new ConversionFactor(360,  Degree)); // 1 per circle

    #endregion

    #region Frequency

    public static readonly UnitInfo Hertz = new(UnitType.Hz, Frequency, "Hz", flags: SI);
    public static readonly UnitInfo kHz   = Hertz.WithPrefix(SIPrefix.k, UnitType.kHz);

    // rpm

    #endregion

    #region Length

    public static readonly UnitInfo Meter = new(UnitType.Meter, Length, "m", flags: SI | Base);  // m
    public static readonly UnitInfo Mm    = Meter.WithPrefix(SIPrefix.m, UnitType.Millimeter);   // mm
    public static readonly UnitInfo Cm    = Meter.WithPrefix(SIPrefix.c, UnitType.Centimeter);   // cm

    public static readonly UnitInfo Inch  = new(UnitType.Inch, Length, "in", flags: Imperial);
    public static readonly UnitInfo Foot  = new(UnitType.Foot, Length, "ft", new ConversionFactor(12, Inch));

    public static readonly UnitInfo Parsec           = new(UnitType.Parsec,           Length, "parsec", flags: Base);
    public static readonly UnitInfo AstronomicalUnit = new(UnitType.AstronomicalUnit, Length, "au");

    #endregion

    #region Mass

    // The SI base unit of mass is KG, however - we use the gram

    public static readonly UnitInfo Gram     = new(UnitType.Gram, Mass, "g", flags: SI | Base);
    public static readonly UnitInfo Kilogram = Gram.WithPrefix(SIPrefix.k, UnitType.Kilogram);

    public static readonly UnitInfo Pound = new(UnitType.Pound, Mass, "lb", 453.592d); // lb = 453.592g

    #endregion

    public static readonly UnitInfo Mole = new(UnitType.Mole, AmountOfSubstance, "mol", flags: SI | Base);

    #region Luminosity

    public static readonly UnitInfo Candela = new(UnitType.Candela, LuminousIntensity, "cd", flags: SI | Base);

    #endregion

    #region Time

    // 5.39 x 10−44 s

    public static readonly UnitInfo Second  = new(UnitType.Second,  Time, "s",   flags: SI | Base);  // s
    public static readonly UnitInfo Minute  = new(UnitType.Minute,  Time, "min", 60d);
    public static readonly UnitInfo Hour    = new(UnitType.Hour,    Time, "h",   60d * 60d);
    public static readonly UnitInfo Day     = new(UnitType.Day,     Time, "h",   86400);
    public static readonly UnitInfo Week    = new(UnitType.Week,    Time, "wk",  60d * 60d * 24 * 7);

    #endregion

    // Pressure - 
    public static readonly UnitInfo Pascal = new(UnitType.Pascal, Pressure, "Pa");
     
    // Volume - 
    public static readonly UnitInfo Liter = new(UnitType.Liter, Volume, "L"); //  1,000 cubic centimeters

    public static readonly UnitInfo Katal = new(UnitType.Katal, CatalyticActivity, "kat");

    public static readonly UnitInfo SquareMetre = Meter.WithExponent(2, UnitType.SquareMeter);
    public static readonly UnitInfo CubicMetre  = Meter.WithExponent(3, UnitType.CubicMetre);

    public static readonly UnitInfo Percent = new(UnitType.Percent, Dimension.None, "%", definitionUnit: new Number(0.01)); // 1/100

    public UnitInfo(UnitType id, Dimension dimension, string name, double scale = 1, int exponent = 1, UnitFlags flags = UnitFlags.None)
    {
        Id = (int)id;
        Name = name;
        Dimension = dimension;
        Scale = scale;
        Exponent = exponent;
        Flags = flags;
    }

    public UnitInfo(UnitType id, Dimension dimension, string name, params ConversionFactor[] converters)
    {
        Id = (int)id;
        Name = name;
        Dimension = dimension;
        Exponent = 1;
        Converters = converters;
        Scale = 1;
    }

    public UnitInfo(UnitType id, Dimension dimension, string name, IObject definitionUnit)
    {
        Id              = (int)id;
        Name            = name;
        Dimension       = dimension;
        DefinitionUnit  = definitionUnit;
    }

    public int Id { get; }

    public Dimension Dimension { get; }

    // relative to base unit
    public double Scale { get; }

    public bool IsBaseUnit => Flags.HasFlag(Base);

    public string Name { get; }

    public int Exponent { get; } = 1;

    public IObject? DefinitionUnit { get; }

    public Func<double, double> BaseConverter => (double source) => source * Scale;

    public Func<TType, TType> GetBaseConverter<TType>()
        where TType: INumberBase<TType>
    {
        var s = TType.CreateChecked(Scale);

        return (TType source) => source * s;
    }

    private UnitFlags Flags { get; }

    ObjectType IObject.Kind => ObjectType.Unit;

    public ConversionFactor[]? Converters { get; }

    public bool IsMetric => Flags.HasFlag(SI);

    public bool HasDimension => Dimension != Dimension.None;

    public UnitInfo WithPrefix(SIPrefix scale, UnitType type = default)
    {
        return new UnitInfo(type, Dimension, Name, scale: scale.Value, exponent: Exponent);
    }

    public UnitInfo WithExponent(int exponent, UnitType type = default)
    {
        if (Exponent == exponent) return this;

        return new UnitInfo(type, Dimension, Name, scale: Scale, exponent: exponent);
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
        if (Scale is 1 && Exponent is 1)
        {
            return Name;
        }

        var sb = new ValueStringBuilder(stackalloc char[16]);

        WriteTo(ref sb);

        return sb.ToString();
    }

    private void WriteTo(ref ValueStringBuilder sb)
    {
        if (Scale != 1)
        {
            if (SIPrefix.TryGetFromScale(Scale, out var si))
            {
                sb.Append(si.Name); // e.g. k
            }
        }

        sb.Append(Name);   // e.g. g

        if (Exponent is not 1)
        {
            new Superscript(Exponent).WriteTo(ref sb);
        }
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        if (Scale is 1 && Exponent is 1)
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

        return Scale.Equals(other.Scale) 
            && string.Equals(Name, other.Name, StringComparison.Ordinal) 
            && Exponent == other.Exponent;
    }

    public override bool Equals(object? obj)
    {
        return obj is UnitInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Scale, Name, Exponent);
    }

    public static UnitInfo Create(string name)
    {
        return new UnitInfo(
            id        : (UnitType)UnitId.Next(),
            dimension : Dimension.None,
            name      : name
        );
    }
}