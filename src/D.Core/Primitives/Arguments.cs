using System;
using System.Collections;
using System.Collections.Generic;

namespace D
{
    public interface IArguments : IEnumerable<Argument>
    {
        object this[int i] { get; }

        object this[string name] { get; }

        int Count { get;  }
    }

    // TODO: arguments may have names...

    public static class Arguments
    {
        public static readonly IArguments None = new ArgumentList(Array.Empty<Argument>());

        public static IArguments Create(Argument[] items)
        {
            return items.Length switch
            {
                0 => None,
                1 => items[0],
                _ => new ArgumentList(items)
            };
        }

        public static IArguments Create(object item)
        {
            return new Argument(null, item);
        }

        public static IArguments Create(params object[] items)
        {
            var args = new Argument[items.Length];

            for (var i = 0; i < items.Length; i++)
            {
                args[i] = new Argument(null, items[i]);
            }

            return new ArgumentList(args);
        }
    }

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

        public object this[string name] => Name == name ? Value : throw new ArgumentNullException(nameof(name));

        public int Count => 1;

        #region IEnumerable

        public IEnumerator<Argument> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
        #endregion
    }

    internal sealed class ArgumentList : IArguments
    {
        private readonly Argument[] items;

        public ArgumentList(Argument[] items)
        {
            this.items = items;
        }

        public object this[int i] => items[i].Value;

        public object this[string name]
        {
            get
            {
                foreach (var arg in items)
                {
                    if (arg.Name == name) return arg.Value;
                }

                throw new KeyNotFoundException(name ?? "null");
            }
        }

        public int Count => items.Length;

        #region IEnumerable

        public IEnumerator<Argument> GetEnumerator() => ((IList<Argument>)items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

        #endregion
    }
}
