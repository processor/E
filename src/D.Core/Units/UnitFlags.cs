using System;

namespace D.Units
{
    [Flags]
    public enum UnitFlags
    {
        None     = 0,
        SI       = 0 << 1,
        Base     = 1 << 2,
        Derived  = 1 << 3,

        // Other Systems
        Imperial = 1 << 4,

        Relative = 1 << 10
    }
}
