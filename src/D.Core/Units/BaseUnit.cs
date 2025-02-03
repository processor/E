using System;
using System.Buffers.Text;
using System.Globalization;
using System.Numerics;

namespace E.Units;

public readonly struct BaseUnitConversionFactor : IConversionFactor, IEquatable<BaseUnitConversionFactor>
{
    private readonly byte[] _value;

    public BaseUnitConversionFactor(byte[] value, UnitInfo targetUnit)
    {
        _value = value;
        Unit = targetUnit;
    }

    public BaseUnitConversionFactor(long value, UnitInfo targetUnit)
    {
        Span<byte> buffer = stackalloc byte[16]; // Allocate on the stack

        _ = Utf8Formatter.TryFormat(value, buffer, out int bytesWritten);

        _value = buffer[..bytesWritten].ToArray();
        Unit = targetUnit;
    }

    public BaseUnitConversionFactor(decimal value, UnitInfo targetUnit, ConversionFactorFlags flags = default)
    {
        Span<byte> buffer = stackalloc byte[32]; // Allocate on the stack

        _ = Utf8Formatter.TryFormat(value, buffer, out int bytesWritten);

        _value = buffer[..bytesWritten].ToArray();
        Unit = targetUnit;
        Flags = flags;
    }

    public BaseUnitConversionFactor(MetricPrefix value, UnitInfo targetUnit)
    {
        Span<byte> buffer = stackalloc byte[16]; // Allocate on the stack

        _ = Utf8Formatter.TryFormat(value.Value, buffer, out int bytesWritten);

        _value = buffer[..bytesWritten].ToArray();
        Unit = targetUnit;
        Flags = ConversionFactorFlags.Exact;
    }

    // 1 of source = x of target

    public ReadOnlySpan<byte> Value => _value;

    public UnitInfo Unit { get; }

    public ConversionFactorFlags Flags { get; }

    public T Convert<T>(T source)
        where T : INumber<T>
    {
        return source * T.Parse(_value, CultureInfo.InvariantCulture);
    }

    public Func<T, T> Compile<T>()
        where T : INumberBase<T>
    {
        // parse the conversion factor once
        T factor = T.Parse(_value, CultureInfo.InvariantCulture);

        // return a function that multiplies the input by the parsed factor
        return source => source * factor;
    }

    public bool Equals(BaseUnitConversionFactor other)
    {
        return Value.SequenceEqual(other.Value) && Unit.Id == other.Unit.Id;
    }
}