using System;

namespace D
{
    [Flags]
    public enum TypeFlags
    {
        None   = 0,
        Record = 1 << 3,
        Event  = 1 << 4,
        Class  = 1 << 5,
        Struct = 1 << 6
    }
}