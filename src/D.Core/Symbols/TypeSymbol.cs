namespace E.Symbols;

public sealed class TypeSymbol : Symbol
{
    public static readonly TypeSymbol Object  = new ("Object"); // Object | None
    public static readonly TypeSymbol String  = new ("String");
    public static readonly TypeSymbol Number  = new ("Number");
    public static readonly TypeSymbol Void    = new ("Void");    // unit '()' in rust
    public static readonly TypeSymbol Int32   = new ("Int32");   // i32
    public static readonly TypeSymbol Int64   = new ("Int64");   // i64
    public static readonly TypeSymbol Float16 = new ("Float16"); // f16
    public static readonly TypeSymbol Float32 = new ("Float32"); // f32
    public static readonly TypeSymbol Float64 = new ("Float64"); // f64

    public TypeSymbol(string name)
        : base(name, SymbolFlags.None) { }

    public TypeSymbol(string name, Symbol[] arguments)
        : base(name, arguments) { }

    public TypeSymbol(ModuleSymbol? module, string name, Symbol[] arguments)
        : base(module, name, arguments) { }

    public TypeSymbol? BaseType { get; set; }

    public Type? ResolvedType { get; private set; }

    public void Initialize(Type type)
    {
        ResolvedType = type;

        Status = SymbolStatus.Resolved;
    }

    // Protocols       
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