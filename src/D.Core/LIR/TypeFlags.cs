using System;

namespace D
{
    [Flags]
    public enum TypeFlags : byte
    {
        None   = 0,
        Actor  = 1 << 0,
        Record = 1 << 1,
        Event  = 1 << 2,
        Class  = 1 << 3,
        Struct = 1 << 4
    }
}