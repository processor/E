using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace E.Units;

public sealed class UnitConverterFactory<T>
    where T: unmanaged, INumberBase<T>
{
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
            var func = sourceUnit.GetBaseConverter<T>();

            // TODO: thread safety for reads

            lock (_lock)
            {
                _instances.Add(key, func);
            }

            return func;
        }
        else
        {
            throw new Exception($"No converter from {sourceUnit.Id} -> {targetUnit.Id}");
        }
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