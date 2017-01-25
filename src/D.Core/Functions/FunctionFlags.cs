using System;

namespace D
{
    [Flags]
    public enum FunctionFlags
    {
        None        = 0,
        Abstract    = 1 << 1,
        Instance    = 1 << 2,
        Operator    = 1 << 3,
        Anonymous   = 1 << 4,  // a => a + 1
        Initializer = 1 << 5,
        Converter   = 1 << 6,
        Indexer     = 1 << 7,
        Property    = 1 << 8

        // Lambda = 1 << 3
    }
}
