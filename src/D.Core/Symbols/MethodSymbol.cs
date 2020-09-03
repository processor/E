using System;
using System.Collections.Generic;

namespace D.Symbols
{
    public sealed class MethodSymbol : Symbol
    {
        public MethodSymbol(ModuleSymbol? module, string name)
           : base(module, name, Array.Empty<Symbol>()) { }

        public MethodSymbol(string name)
            : base(name) { }
       
        public IReadOnlyList<ParameterSymbol>? Parameters { get; set; }

        public TypeSymbol? ReturnType { get; set; }
    }

    // A method may be a Constructor, Destructor, or Function
}