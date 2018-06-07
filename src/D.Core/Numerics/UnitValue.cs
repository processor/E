using System;

namespace D.Units
{
    public static class UnitValue
    {
        public static UnitValue<T> Create<T>(T value, UnitType type)
            where T : unmanaged, IComparable<T>, IEquatable<T>, IFormattable
        {
            return new UnitValue<T>(value, type);
        }

        public static UnitValue<T> Create<T>(UnitType type)
            where T : unmanaged, IComparable<T>, IEquatable<T>, IFormattable
        {
            return new UnitValue<T>(default, type);
        }

        public static UnitValue<double> Parse(string text) => UnitValue<double>.Parse(text);
    }
}