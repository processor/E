using System;

namespace E.Symbols
{
    public sealed class PropertySymbol : Symbol
    {
        public PropertySymbol(string name)
            : base(name) { }

        public PropertySymbol(ModuleSymbol module, string name)
            : base(module, name, Array.Empty<Symbol>()) { }
    }
}