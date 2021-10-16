// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;
using System.Linq;

namespace E.Inference;

public sealed class LetNode : INode
{
    public LetNode(DefineNode[] arguments, INode body)
    {
        Arguments = arguments;
        Body = body;
    }

    public INode[] Arguments { get; }

    public INode Body { get; }

    public override string ToString()
    {
        var args = string.Join<INode>("; ", Arguments);

        return $"({args}) {Body}";
    }

    public IType Infer(Environment env, IReadOnlyList<IType> types)
    {
        env = env.Nested();

        return Arguments.Select(define => TypeSystem.Infer(env, define, types)).Concat(new[] {
            TypeSystem.Infer(env, Body, types)
        }).Last();
    }
}

// let a = TYPE