using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace D
{
    // The LLVM language specifies integer types as iN, where N is the bit-width of the integer, and ranges from 1 to 2^23-1

    // no distiction between signed and unsigned

    [StructLayout(LayoutKind.Sequential, Size = 8)]
    public struct Integer : IObject, INumber
    {
        public Integer(long value)
        {
            Value = value;
        }

        public long Value { get; }

        public int BitCount => 64;

        Kind IObject.Kind => Kind.Integer;

        public static implicit operator int(Integer value)
          => (int)value.Value;

        public static implicit operator long(Integer value)
            => value.Value;

        #region INumeric

        T INumber.As<T>() => (T)Convert.ChangeType(Value, typeof(T));

        double INumber.Real => Value;

        #endregion

        public override string ToString() => Value.ToString();
    }
    
    
    public struct HugeInteger : IObject, INumber
    {
        public HugeInteger(BigInteger value)
        {
            Value = value;
        }

        public BigInteger Value { get; }

        public int BitCount => Value.ToByteArray().Length * 8;

        Kind IObject.Kind => Kind.BigInteger; // TODO

        /*
        -1 (FF)                                     -> 1 bytes: FF
        1 (01)                                      -> 1 bytes: 01
        0 (00)                                      -> 1 bytes: 00
        120 (78)                                    -> 1 bytes: 78
        128 (0080)                                  -> 2 bytes: 80 00
        255 (00FF)                                  -> 2 bytes: FF 00
        1024 (0400)                                 -> 2 bytes: 00 04
        -9223372036854775808 (8000000000000000)     -> 8 bytes: 00 00 00 00 00 00 00 80
        9223372036854775807 (7FFFFFFFFFFFFFFF)      -> 8 bytes: FF FF FF FF FF FF FF 7F
        90123123981293054321 (04E2B5A7C4A975E971)   -> 9 bytes: 71 E9 75 A9 C4 A7 B5 E2 04
        */

        #region INumeric

        T INumber.As<T>() => (T)Convert.ChangeType(Value, typeof(T));

        double INumber.Real => (double)Value;

        #endregion

        public override string ToString() => Value.ToString();
    }
}
