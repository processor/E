// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

namespace E.Inference;

public static class Node
{
    public static ApplyNode Apply(VariableNode variable, INode[] args)
    {
        return new ApplyNode(variable, args);
    }

    public static ApplyNode Apply(VariableNode variable, INode[] args, IType? ctor)
    {
        return new ApplyNode(variable, args, ctor);
    }

    public static AbstractNode Abstract(VariableNode[] args, INode body, IType? type = null)
    {
        return new AbstractNode(args, body, type);
    }

    public static LetNode Let(DefineNode[] defs, INode body) => new LetNode(defs, body);
}
