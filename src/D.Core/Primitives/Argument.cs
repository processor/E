using System;
using System.Collections;
using System.Collections.Generic;

using D.Symbols;

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

        public readonly object this[int index] => index >= 0 ? Value : throw new ArgumentOutOfRangeException(nameof(index), "Out of range");

        public readonly object this[string name] => Name is not null && Name == name 
            ? Value 
            : throw new ArgumentNullException(nameof(name));

        public readonly int Count => 1;

        #region IEnumerable

        public readonly IEnumerator<Argument> GetEnumerator()
        {
            yield return this;
        }

        readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
        #endregion
    }
}
