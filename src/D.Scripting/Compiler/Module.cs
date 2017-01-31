using System;
using System.Collections.Concurrent;

namespace D
{
    public class Module
    {
        private readonly ConcurrentDictionary<string, Symbol> symbols = new ConcurrentDictionary<string, Symbol>();

        public Module(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public Symbol Get(string name)
            => symbols[name];
        

        public void Add(Symbol symbol)
            => symbols[symbol.Name] = symbol;
    }
}
