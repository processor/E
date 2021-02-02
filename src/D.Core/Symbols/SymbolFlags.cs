using System;

namespace D.Symbols
{
    [Flags]
    public enum SymbolFlags
    {
        None     = 0,
      
        Property = 1 << 1, // type member

        Infix    = 1 << 2,
        Postfix  = 1 << 3,

        // Scope
        Local    = 1 << 4
    }
}