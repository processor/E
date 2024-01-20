// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;

namespace E.Inference;

public sealed class ConstantNode : INode
{
    public ConstantNode(IType type)
    {
        Value = type;
    }

    public ConstantNode(string name)
    {
        Value = name;
    }

    // IType | string
    public object? Value { get; }

    public IType Infer(Environment env, ReadOnlySpan<IType> types)
    {
        return Value switch
        {
            IType type => type,
            string name => env[name],
            _ => throw new Exception("ConstantNode must be a type or name")
        };
    }

    public override string ToString() => "{ " + Value + " }";
}
