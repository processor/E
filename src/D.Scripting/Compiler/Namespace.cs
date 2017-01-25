using System;
using System.Collections.Concurrent;

namespace D
{
    public class Namespace
    {
        private readonly ConcurrentDictionary<string, Symbol> symbols = new ConcurrentDictionary<string, Symbol>();

        public Namespace(string name)
        {
            #region Preconditions

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            #endregion

            Name = name;
        }

        public string Name { get; }

        public Symbol Get(string name)
        {
            return symbols[name];
        }

        public void Add(Symbol symbol)
        {
            symbols[symbol.Name] = symbol;
        }
    }
}
