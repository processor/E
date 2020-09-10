using System.Collections.Generic;

namespace D.Symbols
{
    public class SymbolAliases
    {
        public readonly Dictionary<string, TypeSymbol> items = new Dictionary<string, TypeSymbol>
        {
            { "i32",  TypeSymbol.Int32 },
            { "i64",  TypeSymbol.Int64 },
            { "f32",  TypeSymbol.Float32 },
            { "f64",  TypeSymbol.Float64 },
            { "none", TypeSymbol.Void }
        };

        public bool TryGet(string name, out TypeSymbol symbol)
        {
            return items.TryGetValue(name, out symbol);
        }
    }
}
