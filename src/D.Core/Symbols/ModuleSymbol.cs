using System;
using System.Collections.Generic;

namespace D
{
    public sealed class ModuleSymbol : Symbol
    {
        private readonly Dictionary<string, Symbol> lookup = new Dictionary<string, Symbol>();

        public ModuleSymbol(string name, ModuleSymbol? parent = null)
            : base(name, Array.Empty<Symbol>())
        {
            Parent = parent;
        }
        
        public ModuleSymbol? Parent { get; }

        public IEnumerable<Symbol> Children => lookup.Values;

        public override void Add(Symbol child)
        {
            lookup[child.Name] = child;
        }

        public override bool TryGetValue(string name, out Symbol value)
        {
            return lookup.TryGetValue(name, out value);
        }
    }
}