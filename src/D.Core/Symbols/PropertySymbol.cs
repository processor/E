using System;

namespace D
{
    public class PropertySymbol : Symbol
    {
        public PropertySymbol(string name)
            : base(name) { }

        public PropertySymbol(string module, string name)
            : base(module, name, Array.Empty<Symbol>()) { }

        public override SymbolType SymbolType => SymbolType.Property;
    }
}