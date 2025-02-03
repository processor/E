using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace E.Units;

public sealed class UnitConverterFactory<T>
    where T: INumberBase<T>
{
    public static readonly UnitConverterFactory<T> Default = new();

    private readonly Lock _lock = new();
    private readonly Dictionary<ulong, Func<T, T>> _instances = [];

    public Func<T, T> Get(UnitInfo sourceUnit, UnitInfo targetUnit)
    {
        var key = CombineToInt64((uint)sourceUnit.Id, (uint)targetUnit.Id);

        if (_instances.TryGetValue(key, out var converter))
        {
            return converter;
        }
        else if (sourceUnit.Dimension == targetUnit.Dimension)
        {
            var conversionFactor = sourceUnit.GetBaseUnitConversionFactor<T>() / targetUnit.GetBaseUnitConversionFactor<T>();

            Func<T, T> func = ConverterFactorCompiler.Compile(conversionFactor);

            // TODO: thread safety for reads
            lock (_lock)
            {
                _instances.TryAdd(key, func);
            }

            return func;
        }
        else if (sourceUnit.Converters != null)
        {
            foreach (var c in sourceUnit.Converters)
            {
                if (targetUnit.Id == c.Unit.Id)
                {
                    Func<T, T> func = ConverterFactorCompiler.Compile(T.Parse(c.Value, NumberStyles.Number, CultureInfo.InvariantCulture));

                    // TODO: thread safety for reads
                    lock (_lock)
                    {
                        _instances.TryAdd(key, func);
                    }

                    return func;
                }
            }
        }

        throw new Exception($"No converter from {sourceUnit} ({sourceUnit.Id}) -> {targetUnit} ({targetUnit.Id})");
        
        // kg   = 1000
        // g    = 0
        // mg   = 0.001

        // kg -> mg = 1,000,000     kg.units / mg.units
        // mg -> kg = .0000001      mg.units / kg.units

        // Type Conversions ft -> m, etc 
    }

    public Func<T, T> Get(UnitType sourceUnit, UnitType targetUnit)
    {
        var key = CombineToInt64(Unsafe.BitCast<UnitType, uint>(sourceUnit), Unsafe.BitCast<UnitType, uint>(targetUnit));

        return _instances[key];
    }

    public void Add(UnitType sourceUnit, UnitType targetUnit, Func<T, T> converter)
    {
        var value = CombineToInt64(Unsafe.BitCast<UnitType, uint>(sourceUnit), Unsafe.BitCast<UnitType, uint>(targetUnit));

        _instances.Add(value, converter);
    }

    public void Add(UnitType sourceUnit, ConversionFactor factor)
    {
        var value = CombineToInt64(Unsafe.BitCast<UnitType, uint>(sourceUnit), (uint)(factor.Unit.Id));

        _instances.Add(value, factor.Compile<T>());
    }

    private static ulong CombineToInt64(uint high, uint low)
    {
        return ((ulong)high << 32) | low;
    }
}