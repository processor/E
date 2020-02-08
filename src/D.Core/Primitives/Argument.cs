using System;
using System.Collections;
using System.Collections.Generic;

namespace D
{
    public readonly struct Argument : IArguments
    {
        public Argument(object value)
        {
            Name = null;
            Value = value;
        }

        public Argument(Symbol? name, object value)
        {
            Name = name;
            Value = value;
        }

        public Symbol? Name { get; }

        public object Value { get; }
        
        // NameIsImplict?

        public object this[int i] => i >= 0 ? Value : throw new ArgumentOutOfRangeException("Out of range");

        public object this[string name] => Name != null && Name == name 
            ? Value 
            : throw new ArgumentNullException(nameof(name));

        public int Count => 1;

        #region IEnumerable

        public IEnumerator<Argument> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
        #endregion
    }
}
