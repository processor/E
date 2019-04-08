namespace D
{
    public enum SymbolFlags
    {
        None        = 0,
      
        Property    = 1 << 1, // type member

        Infix       = 1 << 2,
        Postfix     = 1 << 3,

        // Scope
        BlockScoped = 1 << 4, // { } 
        Local       = 1 << 5
    }

    public enum SymbolKind

    {
        TypeParameter,
        Label,
        Module,
        Property,
        Method, // e.g. Function
    }
}