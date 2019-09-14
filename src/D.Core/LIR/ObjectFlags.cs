using System;

namespace D
{
    [Flags]
    public enum ObjectFlags
    {
        None        = 0,
        Public      = 1 << 0, // default? 
        Internal    = 1 << 1,
        Private     = 1 << 2,
        Protected   = 1 << 3,
        Abstract    = 1 << 4,
        Static      = 1 << 5,
        Instance    = 1 << 6,
        Operator    = 1 << 7,
        Anonymous   = 1 << 8, // a => a + 1
        Initializer = 1 << 9,
        Converter   = 1 << 10,
        Indexer     = 1 << 11,
        Property    = 1 << 12,

        Mutable     = 1 << 13,
        Mutating    = 1 << 14,
        Lazy        = 1 << 15,

        Lambda      = 1 << 20
    }

    // Declaration Modifiers?
}