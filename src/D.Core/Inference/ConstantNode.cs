﻿// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;
using System.Collections.Generic;

namespace E.Inference;

public sealed class ConstantNode : Node
{
    public ConstantNode(IType spec)
    {
        Spec = spec;
    }

    public override IType Infer(Environment env, IReadOnlyList<IType> types)
    {
        return Spec switch
        {
            IType type => type,
            string name => env[name],
            _ => throw new Exception("ConstantNode must be a type or name")
        };
    }

    public override string ToString() => "{ " + Spec + " }";
}
