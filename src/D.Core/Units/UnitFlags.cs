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
    Metric   = 1 << 3,
    Imperial = 1 << 4,

    Relative = 1 << 5
}