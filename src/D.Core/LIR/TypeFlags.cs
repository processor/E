using System;

namespace E
{
    [Flags]
    public enum TypeFlags
    {
        None        = 0,
      
        Class       = 1 << 1,
        Struct      = 1 << 2,
        Actor       = 1 << 3,
        Record      = 1 << 4,
        Event       = 1 << 5,
        Certificate = 1 << 6,
        Role        = 1 << 7
    }
}