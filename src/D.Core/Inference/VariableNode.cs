// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;

namespace E.Inference;

public sealed class VariableNode(string name, IType? type = null) : INode
{
    public string Name { get; } = name;

    public IType? Type { get; } = type;

    public override string ToString() => Name;

    public IType Infer(Environment env, IReadOnlyList<IType> types)
    {
        return TypeSystem.Fresh(env[Name], types);
    }
}

// AKA identity node...