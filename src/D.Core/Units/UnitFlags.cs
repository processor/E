using System;

namespace E.Units;

[Flags]
public enum UnitFlags
{
    None     = 0,
    SI       = 1 << 0,
    Base     = 1 << 1,
    Derived  = 1 << 2,

    // Other Systems
    Imperial = 1 << 3,

    Relative = 1 << 5
}