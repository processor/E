﻿using System;

namespace E;

[Flags]
public enum ParameterFlags
{
    None     = 0,
    Optional = 1 << 0,
    ReadOnly = 1 << 1
}