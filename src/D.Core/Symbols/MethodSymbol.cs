namespace E.Symbols;

public sealed class MethodSymbol : Symbol
{
    public MethodSymbol(ModuleSymbol? module, string name)
        : base(module, name, []) { }

    public MethodSymbol(string name)
        : base(name) { }
       
    public ParameterSymbol[]? Parameters { get; set; }

    public TypeSymbol? ReturnType { get; set; }
}

// A method may be a Constructor, Destructor, or Function