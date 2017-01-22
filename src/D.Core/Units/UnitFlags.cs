using System;

namespace D.Units
{
    [Flags]
    public enum UnitFlags
    {
        None     = 0,
        SIBase   = 1 << 1,
        Derived  = 1 << 2,

        // Other Systems
        Imperial = 1 << 3
    }
}
