namespace D
{
    public enum SymbolType
    {
        Variable    = 1 << 0,
        Argument    = 1 << 1,
        Property    = 1 << 2,
        Type        = 1 << 3, // class | struct
        Function    = 1 << 4,
        Module      = 1 << 5,
        Label       = 1 << 6, // State?
        Operator    = 1 << 7,
    }
}