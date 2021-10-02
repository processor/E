using System;

namespace E;

[Flags]
public enum Visibility : byte
{
    Public    = 1 << 0,
    Private   = 1 << 1,
    Internal  = 1 << 2,
    Protected = 1 << 3
}