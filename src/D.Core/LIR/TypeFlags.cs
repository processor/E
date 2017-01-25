using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D
{
    [Flags]
    public enum TypeFlags
    {
        None = 0,
        Primitive = 1 << 1,
        Record = 1 << 3,
        Event = 1 << 4
    }
}
