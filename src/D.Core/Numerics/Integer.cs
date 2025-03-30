using System.Numerics;
using System.Runtime.CompilerServices;

namespace E;

public readonly struct Integer<T>(T value) : IObject, INumberObject
    where T : IBinaryNumber<T>
{
    public T Value { get; } = value;

    public int BitCount => Unsafe.SizeOf<T>() * 8;

    readonly ObjectType IObject.Kind => ObjectType.Int64;

    public static implicit operator int(Integer<T> value) => int.CreateChecked(value.Value);

    public static implicit operator long(Integer<T> value) => long.CreateChecked(value.Value);

    #region INumeric

    readonly TResult INumberObject.As<TResult>() => TResult.CreateChecked(Value);

    #endregion

    public readonly override string ToString() => Value.ToString()!;
}


// The LLVM language specifies integer types as iN, where N is the bit-width of the integer, and ranges from 1 to 2^23-1

// no distinction between signed and unsigned
