using System;
using System.Buffers.Text;
using System.Globalization;
using System.Numerics;

namespace E.Units;

public readonly struct ConversionFactor
{
    private readonly byte[] _value;

    public ConversionFactor(byte[] value, UnitInfo targetUnit)
    {
        _value = value;
        Unit = targetUnit;
    }

    public ConversionFactor(long value, UnitInfo targetUnit)
    {
        Span<byte> buffer = stackalloc byte[16]; // Allocate on the stack

        _ = Utf8Formatter.TryFormat(value, buffer, out int bytesWritten);

        _value = buffer[..bytesWritten].ToArray();
        Unit = targetUnit;
    }

    public ConversionFactor(decimal value, UnitInfo targetUnit)
    {
        Span<byte> buffer = stackalloc byte[32]; // Allocate on the stack

        _ = Utf8Formatter.TryFormat(value, buffer, out int bytesWritten);

        _value = buffer[..bytesWritten].ToArray();
        Unit = targetUnit;
    }

    // 1 of source = x of target

    public ReadOnlySpan<byte> Value => _value;

    public UnitInfo Unit { get; }

    public T Convert<T>(T source)
        where T: INumber<T>
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

    public static ConversionFactor CubicMetre(ReadOnlySpan<byte> value)
    {
        return new ConversionFactor(value.ToArray(), UnitInfo.CubicMetre);
    }

    public static ConversionFactor CubicMetre(decimal value)
    {
        Span<byte> buffer = stackalloc byte[32]; // Allocate on the stack

        _ = Utf8Formatter.TryFormat(value, buffer, out int bytesWritten);

        return new ConversionFactor(buffer[..bytesWritten].ToArray(), UnitInfo.CubicMetre);
    }
}