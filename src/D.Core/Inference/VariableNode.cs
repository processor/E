// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;

namespace E.Inference;

public sealed class VariableNode : INode
{
    public VariableNode(string name, IType? type = null)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; }

    public IType? Type { get; }

    public override string ToString() => Name;

    public IType Infer(Environment env, IReadOnlyList<IType> types)
    {
        return TypeSystem.Fresh(env[Name], types);
    }
}

// AKA identity node...