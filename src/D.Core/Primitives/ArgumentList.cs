using System.Collections;
using System.Collections.Generic;

namespace D
{
    public sealed class ArgumentList : IArguments
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
                    if (arg.Name is not null && arg.Name == name)
                    {
                        return arg.Value;
                    }
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
