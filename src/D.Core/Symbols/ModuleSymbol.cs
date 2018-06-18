using System;

namespace D
{
    public sealed class ModuleSymbol : Symbol
    {
        public ModuleSymbol(string name, ModuleSymbol parent = null)
            : base(name, Array.Empty<Symbol>())
        {
            Parent = parent;
        }
        
        public ModuleSymbol Parent { get; }
    }
}