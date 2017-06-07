namespace D
{
    public enum SymbolType
    {
        Variable    = 1 << 0,
        Argument    = 1 << 1,
        Member      = 1 << 2,
        Type        = 1 << 3,
        Function    = 1 << 4,
        Module      = 1 << 5,
        Label       = 1 << 6,
        Operator    = 1 << 7,
    }
}