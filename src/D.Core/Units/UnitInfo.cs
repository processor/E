using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
    public static readonly UnitInfo Gradian   = new(UnitType.Gradian, Angle, "grad", [new ConversionFactor(0.9m, Degree)]); // 400 per circle
    public static readonly UnitInfo Turn      = new(UnitType.Turn,    Angle, "turn", [new ConversionFactor(360,  Degree)]); // 1 per circle

    #endregion

    #region Frequency

    public static readonly UnitInfo Hertz = new(UnitType.Hz,  Frequency, "Hz", flags: SI);
    public static readonly UnitInfo kHz   = new(UnitType.kHz, "kHz", new(MetricPrefix.k, Hertz), []);

    // rpm

    #endregion

    #region Mass

    // The SI base unit of mass is KG, however - we use the gram

    public static readonly UnitInfo Gram     = new(UnitType.Gram, Mass, "g", flags: SI | Base);
    public static readonly UnitInfo Kilogram = new(UnitType.Gram, "kg", new(MetricPrefix.k, Gram), []);


    #endregion

    public static readonly UnitInfo Mole = new(UnitType.Mole, AmountOfSubstance, "mol", flags: SI | Base);

    #region Luminosity

    public static readonly UnitInfo Candela = new(UnitType.Candela, LuminousIntensity, "cd", flags: SI | Base);

    #endregion

    // Pressure - 
    public static readonly UnitInfo Pascal = new(UnitType.Pascal, Pressure, "Pa");
     
    // Volume - 

    public static readonly UnitInfo Katal = new(UnitType.Katal, CatalyticActivity, "kat");

    public static readonly UnitInfo SquareMetre = LengthUnits.Meter.WithExponent(2, UnitType.SquareMeter);

    public static readonly UnitInfo Percent = new(UnitType.Percent, Dimension.None, "%", definitionUnit: new Number(0.01)); // 1/100

   
    public UnitInfo(UnitType id, Dimension dimension, string name, BaseUnitConversionFactor? baseConverter = null, int exponent = 1, UnitFlags flags = UnitFlags.None)
    {
        Id = (int)id;
        Name = name;
        Dimension = dimension;
        BaseConverter = baseConverter;
        Exponent = exponent;
        Flags = flags;
    }
    

    public UnitInfo(UnitType id, Dimension dimension, string name, IConversionFactor[] converters)
    {
        Id = (int)id;
        Name = name;
        Dimension = dimension;
        Exponent = 1;
        Converters = converters;
        BaseConverter = null;
    }

    public UnitInfo(UnitType id, string name, BaseUnitConversionFactor baseUnit, ReadOnlySpan<ConversionFactor> converters, UnitFlags flags = default)
    {
        Id = (int)id;
        Name = name;
        Dimension = baseUnit.Unit.Dimension;
        Exponent = 1;
        Converters = [baseUnit, ..converters];
        BaseConverter = baseUnit;
        Flags = flags;
    }

    public UnitInfo(UnitType id, Dimension dimension, string name, IObject definitionUnit)
    {
        Id = (int)id;
        Name = name;
        Dimension = dimension;
        DefinitionUnit = definitionUnit;
        BaseConverter = null;
    }

    public int Id { get; }

    public Dimension Dimension { get; }

    public BaseUnitConversionFactor? BaseConverter { get; }

    public bool IsBaseUnit => Flags.HasFlag(Base);

    public string Name { get; }

    public int Exponent { get; } = 1;

    public IObject? DefinitionUnit { get; }

    private UnitFlags Flags { get; }

    ObjectType IObject.Kind => ObjectType.Unit;

    public IConversionFactor[]? Converters { get; }

    public bool IsMetric => Flags.HasFlag(SI);

    public bool HasDimension => Dimension != Dimension.None;

    public UnitInfo WithExponent(int exponent, UnitType type = default)
    {
        if (Exponent == exponent) return this;

        return new UnitInfo(type, Dimension, Name, baseConverter: BaseConverter, exponent: exponent);
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
        if (UnitFactory.Default.TryGet(name, out type))
        {
            return true;
        }
        else if (MetricPrefix.TryParseSymbol(name, out MetricPrefix siScale))
        {
            var unitName = name[siScale.Length..];

            if (UnitFactory.Default.TryGet(unitName, out var siBaseType) && siBaseType.IsMetric)
            {
                var baseUnitConversionFactor = new BaseUnitConversionFactor(siScale, siBaseType);

                // if the metric base type is defined as another derived typed
                if (siBaseType.BaseConverter is { } siBaseTypeBaseConverter)
                {
                    var newFactor = siBaseTypeBaseConverter.Compile<decimal>()((decimal)siScale.Value);

                    baseUnitConversionFactor = new(newFactor, siBaseTypeBaseConverter.Unit, siBaseTypeBaseConverter.Flags);
                }

                type = new UnitInfo((UnitType)UnitId.Next(), siBaseType.Dimension, name.ToString(), baseUnitConversionFactor, exponent: siBaseType.Exponent);

                UnitFactory.Default.Add(type.Name, type);

                return true;
            }
        }

        type = null;

        return false;
    }

    [SkipLocalsInit]
    public override string ToString()
    {
        if (BaseConverter is null && Exponent is 1)
        {
            return Name;
        }

        var sb = new ValueStringBuilder(stackalloc char[16]);

        WriteTo(ref sb);

        return sb.ToString();
    }

    private void WriteTo(ref ValueStringBuilder sb)
    {
        sb.Append(Name);   // e.g. g

        if (Exponent is not 1)
        {
            new Superscript(Exponent).WriteTo(ref sb);
        }
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        if (Exponent is 1)
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

        if (BaseConverter != null && other.BaseConverter != null)
        {
            if (!BaseConverter.Value.Equals(other.BaseConverter.Value))
            {
                return false;
            }
        }

        return Name == other.Name
            && Exponent == other.Exponent;
    }

    public override bool Equals(object? obj)
    {
        return obj is UnitInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BaseConverter, Name, Exponent);
    }

    public static UnitInfo Create(string name)
    {
        return new UnitInfo(
            id        : (UnitType)UnitId.Next(),
            dimension : Dimension.None,
            name      : name
        );
    }

    internal T GetBaseUnitConversionFactor<T>()
        where T: INumberBase<T>
    {
        if (BaseConverter is null)
        {
            return T.One;
        }

        return T.Parse(BaseConverter.Value.Value, CultureInfo.InvariantCulture);
    }
}