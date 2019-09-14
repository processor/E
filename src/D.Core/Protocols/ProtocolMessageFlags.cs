﻿namespace D.Protocols
{
    public enum ProtocolMessageFlags : byte
    {
        None        = 0,
        Repeats     = 1 << 1,
        Fallthrough = 1 << 2,
        Optional    = 1 << 3,
        End         = 1 << 4
    }
}