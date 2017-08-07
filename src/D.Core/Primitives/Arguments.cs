using System;
using System.Collections;
using System.Collections.Generic;

namespace D
{
    public interface IArguments : IEnumerable<Argument>
    {
        IObject this[int i] { get; }

        IObject this[string name] { get; }

        int Count { get;  }
    }

    // TODO: arguments may have names...

    public static class Arguments
    {
        public static readonly IArguments None = new ArgumentList(Array.Empty<Argument>());

        public static IArguments Create(IList<Argument> items)
        {
            switch (items.Count)
            {
                case 0  : return None;
                case 1  : return items[0];
                default : return new ArgumentList(items);
            }
        }

        public static IArguments Create(IObject item)
        {
            return new Argument(null, item);
        }

        public static IArguments Create(params IObject[] items)
        {
            var args = new Argument[items.Length];

            for (var i = 0; i < items.Length; i++)
            {
                args[i] = new Argument(null, items[i]);
            }

            return new ArgumentList(args);
        }
    }

    public struct Argument : IArguments
    {
        public Argument(IObject value)
        {
            Name = null;
            Value = value;
        }

        public Argument(Symbol name, IObject value)
        {
            Name = name;
            Value = value;
        }

        public Symbol Name { get; }

        public IObject Value { get; }

        public IObject this[int i]
            => i >= 0 ? Value : throw new ArgumentOutOfRangeException("Out of range");

        public IObject this[string name]
            => Name == name ? Value: throw new Exception("not found");

        public int Count => 1;

        #region IEnumerable

        public IEnumerator<Argument> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
        #endregion
    }

    internal class ArgumentList : IArguments
    {
        private readonly IList<Argument> items = new List<Argument>();

        public ArgumentList(IList<Argument> items)
        {
            this.items = items;
        }

        public IObject this[int i] => items[i].Value;

        public IObject this[string name]
        {
            get
            {
                foreach (var arg in items)
                {
                    if (arg.Name == name) return arg.Value;
                }

                throw new Exception("not found: " + name);
            }
        }

        public int Count => items.Count;

        #region IEnumerable

        public IEnumerator<Argument> GetEnumerator()
            => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => items.GetEnumerator();

        #endregion
    }
}
