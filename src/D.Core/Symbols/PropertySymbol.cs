using System;

namespace D
{
    public sealed class PropertySymbol : Symbol
    {
        public PropertySymbol(string name)
            : base(name) { }

        public PropertySymbol(string module, string name)
            : base(module, name, Array.Empty<Symbol>()) { }
    }
}