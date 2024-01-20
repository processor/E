// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;
using System.Collections.Generic;

namespace E.Inference;

public sealed class LetNode(DefineNode[] arguments, INode body) : INode
{
    public INode[] Arguments { get; } = arguments;

    public INode Body { get; } = body;

    public override string ToString()
    {
        var args = string.Join<INode>("; ", Arguments);

        return $"({args}) {Body}";
    }

    public IType Infer(Environment env, ReadOnlySpan<IType> types)
    {
        env = env.Nested();

        var args = new List<IType>(arguments.Length + 1);

        foreach (var define in Arguments)
        {
            args.Add(TypeSystem.Infer(env, define, types));
        }


        args.Add(TypeSystem.Infer(env, Body, types));

        return args[^1];;
    }
}

// let a = TYPE