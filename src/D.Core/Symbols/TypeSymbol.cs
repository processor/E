namespace D
{
    public class TypeSymbol : Symbol
    {
        public static readonly TypeSymbol Object = new TypeSymbol("Object");    // Object | None
        public static readonly TypeSymbol String = new TypeSymbol("String");
        public static readonly TypeSymbol Number = new TypeSymbol("Number");
        public static readonly TypeSymbol Void   = new TypeSymbol("Void");   // unit '()' in rust

        public TypeSymbol(string name, SymbolFlags flags = SymbolFlags.None)
            : base(name, flags) { }

        public TypeSymbol(string module, string name, Symbol[] arguments)
            : base(module, name, arguments) { }

        public TypeSymbol(string name, params Symbol[] arguments)
            : base(name, arguments) { }

        public override SymbolType SymbolType => SymbolType.Type;
    }
}

// Array        [ Type ]
// Dictionary   [ key`Type: value`Type ]
// Function     (parameter`Type) -> return`Type 
// Tuple        ()
// Optional     Type?       Optional<T>
// Any

// physics::Momentum
// Array<String>
// Array<physics::Momentum>