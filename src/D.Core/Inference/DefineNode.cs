﻿// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace E.Inference;

public sealed class DefineNode(VariableNode variable, INode body) : INode
{
    public VariableNode Variable { get; } = variable;

    public INode Body { get; } = body;

    public override string ToString() => $"{Variable} = {Body}";

    public IType Infer(Environment env, ReadOnlySpan<IType> types)
    {
        var known = new List<IType>(types.Length);

        known.AddRange(types);

        var type = TypeSystem.NewGeneric();
        var varNode = Variable;
            
        env[varNode.Name] = type;

        known.Add(type);

        TypeSystem.Unify(type, TypeSystem.Infer(env, Body, CollectionsMarshal.AsSpan(known)));

        /*
        if (type.Value.IsConstructor)
        {
            type.Value.Bind(varNode.Id);
        }
        */

        return env[varNode.Name] = type.Value;
    }
}

// Assign?