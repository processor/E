namespace D
{
    public enum SymbolFlags
    {
        None        = 0,
      
        // Variables
        Property    = 1 << 9, // type member

        Infix       = 1 << 9,
        Postfix     = 1 << 10,

        // Variable Scope...
        BlockScoped = 1 << 11,
        Local       = 1 << 12
    }
}