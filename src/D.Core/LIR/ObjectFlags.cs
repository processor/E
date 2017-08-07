using System;

namespace D
{
    [Flags]
    public enum ObjectFlags
    {
        None        = 0,
        Public      = 1 << 0, // default? 
        Private     = 1 << 1,
        Internal    = 1 << 2,
        Protected   = 1 << 3,
        Abstract    = 1 << 4,
        Instance    = 1 << 5,
        Operator    = 1 << 6,
        Anonymous   = 1 << 7, // a => a + 1
        Initializer = 1 << 8,
        Converter   = 1 << 9,
        Indexer     = 1 << 10,
        Property    = 1 << 11

        // Lambda = 1 << 3
    }

    // Modifer Flags?
}
