using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

using E.Expressions;
using E.Symbols;

namespace E.Units;

using static Dimension;
using static Expression;
using static UnitFlags;

public sealed class UnitInfo : IEquatable<UnitInfo>, IObject
{
    public static readonly UnitInfo None = new (string.Empty, Dimension.None);

    #region Angles (Plane & Solid)

    private static readonly Symbol π = Symbol.Variable("π");

    public static readonly UnitInfo Radian    = new ("rad", Angle, Base);
    public static readonly UnitInfo Steradian = new ("sr",  SolidAngle, Base);

    public static readonly UnitInfo Degree    = new ("deg",  Angle, 1,   Divide(π, UnitValue.Create(180, Radian))); // π / 180 rad
    public static readonly UnitInfo Gradian   = new ("grad", Angle, 0.9, Degree); // 400 per circle
    public static readonly UnitInfo Turn      = new ("turn", Angle, 360, Degree); // 1 per circle

    #endregion

    #region Electromagism / Energy

    // Defined Under ElectromagismUnits

    #endregion

    #region Frequency

    public static readonly UnitInfo Hertz = new ("Hz", Frequency, SI);
    public static readonly UnitInfo kHz   = Hertz.WithPrefix(SIPrefix.k); // kHz

    // rpm

    #endregion

    #region Length

    public static readonly UnitInfo Meter = new ("m", Length, SI | Base);  // m
    public static readonly UnitInfo Mm    = Meter.WithPrefix(SIPrefix.m);  // mm
    public static readonly UnitInfo Cm    = Meter.WithPrefix(SIPrefix.c);  // cm

    public static readonly UnitInfo Inch  = new ("in", Length, Imperial);

    public static readonly UnitInfo Parsec           = new ("parsec", Length, Base);
    public static readonly UnitInfo AstronomicalUnit = new ("au",     Length);

    #endregion

    #region Mass

    public static readonly UnitInfo Gram     = new ("g", Mass, SI | Base);
    public static readonly UnitInfo Kilogram = Gram.WithPrefix(SIPrefix.k);

    // Standard is KG

    public static readonly UnitInfo Pound = new ("lb", Mass, 453.592d); // lb = 453.592g

    #endregion

    public static readonly UnitInfo Mole = new ("mol", AmountOfSubstance, SI | Base);


    // Luminocity -

    public static readonly UnitInfo Candela = new ("cd", LuminousIntensity, SI | Base);

    #region Time

    // 5.39 x 10−44 s

    public static readonly UnitInfo Second  = new ("s",    Time, SI | Base);  // s
    public static readonly UnitInfo Minute  = new ("min",  Time, 60d);
    public static readonly UnitInfo Hour    = new ("h",    Time, 60d * 60d);
    public static readonly UnitInfo Week    = new ("wk",   Time, 60d * 60d * 24 * 7);

    #endregion

    // Pressure - 
    public static readonly UnitInfo Pascal = new ("Pa", Pressure);
     
    // Volume - 
    public static readonly UnitInfo Liter = new ("L", Length); //  1,000 cubic centimeters


    public static readonly UnitInfo Katal = new ("kat", CatalyticActivity);

    public static readonly UnitInfo SquareMeters = new UnitInfo("m", Length).WithExponent(2);

    // Dimensionless

    public static readonly UnitInfo Percentage = new ("%", Dimension.None, 1, new Number(0.01)); // 1/100

    public UnitInfo(string name) // e.g. px
    {
        Name = name;
        Dimension = Dimension.None;
        DefinitionValue = 1;
    }

    public UnitInfo(string symbol, int exponent) 
    {
        Name      = symbol;
        Dimension   = Dimension.None;
        DefinitionValue = 1;
        Power    = exponent;
    }

    public UnitInfo(string name, Dimension id, UnitFlags flags = UnitFlags.None)
    {
        Name = name;
        Dimension = id;
        Flags = flags;
        DefinitionValue = 1;
    }

    public UnitInfo(string symbol, Dimension dimension, double definitionValue)
    {
        Name       = symbol;
        Dimension  = dimension;
        DefinitionValue = definitionValue;
    }

    public UnitInfo(SIPrefix prefix, string name, Dimension id, double definitionValue, int power)
    {
        Prefix          = prefix;
        Name            = name;
        Dimension       = id;
        DefinitionValue = definitionValue;
        Power           = power;
    }

    public UnitInfo(string name, Dimension id, double definitionValue, IObject definationUnit)
    {
        Name            = name;
        Dimension       = id;
        DefinitionValue = definitionValue;
        DefinitionUnit  = definationUnit;
    }

    public UnitInfo(string name, Dimension id, IObject definationUnit)
    {
        Name = name;
        Dimension = id;
        DefinitionValue = 1;
        DefinitionUnit = definationUnit;
    }

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

    public UnitInfo WithPrefix(SIPrefix prefix)
    {
        return new UnitInfo(prefix, Name, Dimension, DefinitionValue, Power);
    }

    public UnitInfo WithExponent(int exponent)
    {
        if (this.Power == exponent) return this;

        return new UnitInfo(Prefix, Name, Dimension, DefinitionValue, exponent);
    }

    public static bool TryParse(ReadOnlySpan<char> name, [NotNullWhen(true)] out UnitInfo? type)
    {
        if (UnitSet.Default.TryGet(name, out type))
        {
            return true;
        }
        else if (SIPrefix.TryParseSymbol(name, out SIPrefix prefix))
        {
            var unitName = name.Slice(prefix.Length);

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

        if (Prefix.Value != 1)
        {
            sb.Append(Prefix.Name); // e.g. k
        }

        sb.Append(Name);   // e.g. g

        if (Power is not 1)
        {
            new Superscript(Power).WriteTo(ref sb);
        }

        return sb.ToString();
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