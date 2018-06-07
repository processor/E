using System;
using System.Text;

namespace D.Units
{
    public readonly struct Percentage : INumeric<double>, IEquatable<Percentage>
    {        
        public Percentage(double value)
        {
            Value = value;
        }
        
        public double Value { get; } 
        
        #region INumber

        Kind IObject.Kind => Kind.Percentage;

        double INumber.Real => Value;

        T1 INumber.As<T1>() => (T1)Convert.ChangeType(Value, typeof(T1));

        #endregion

        // No space between units...

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Value * 100);

            sb.Append('%');
    
            return sb.ToString();
        }
        
        public bool Equals(Percentage other) => Value == other.Value;
    }
}