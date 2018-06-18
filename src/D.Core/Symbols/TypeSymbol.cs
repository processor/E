namespace D
{
    public sealed class TypeSymbol : Symbol
    {
        public static readonly TypeSymbol Object  = new TypeSymbol("Object"); // Object | None
        public static readonly TypeSymbol String  = new TypeSymbol("String");
        public static readonly TypeSymbol Number  = new TypeSymbol("Number");
        public static readonly TypeSymbol Void    = new TypeSymbol("Void");   // unit '()' in rust
        public static readonly TypeSymbol Int32   = new TypeSymbol("Int32");
        public static readonly TypeSymbol Int64   = new TypeSymbol("Int64");
        public static readonly TypeSymbol Float32 = new TypeSymbol("Float32");
        public static readonly TypeSymbol Float64 = new TypeSymbol("Float64");

        public TypeSymbol(string name)
            : base(name, SymbolFlags.None) { }

        public TypeSymbol(string name, params Symbol[] arguments)
           : base(name, arguments) { }

        public TypeSymbol(ModuleSymbol module, string name, Symbol[] arguments)
            : base(module, name, arguments) { }

       
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