using System;

namespace D
{
    [Flags]
    public enum VariableFlags
    {
        None    = 0,
        Mutable = 1 << 1
    }
}
