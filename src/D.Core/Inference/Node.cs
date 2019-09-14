// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;

namespace D.Inference
{
    public abstract class Node
    {
        public static ConstantNode Constant(IType value)
        {
            return new ConstantNode(value);
        }

        public static VariableNode Variable(string name) => Variable(name, null);

        public static VariableNode Variable(string name, IType? type)
        {
            return new VariableNode(name, type);
        }

        public static ApplyNode Apply(Node expr, Node[] args) => Apply(expr, args, null);

        public static ApplyNode Apply(Node expr, Node[] args, IType ctor)
        {
            return new ApplyNode { Spec = expr, Arguments = args, Type = ctor };
        }

        public static AbstractNode Abstract(Node[] args, Node body) => Abstract(args, null, body);

        public static AbstractNode Abstract(Node[] args, IType type, Node body) => new AbstractNode {
            Arguments = args,
            Body = body,
            Type = type
        };

        public static DefineNode Define(VariableNode var, Node body) => new DefineNode(spec: var, body: body);

        public static LetNode Let(DefineNode[] defs, Node body) => new LetNode { Arguments = defs, Body = body };

        public abstract IType Infer(Environment env, IReadOnlyList<IType> types);

        public object? Spec { get; protected set; }

        public Node[] Arguments { get; private set; }

        public Node Body { get; protected set; }

        public IType Type { get; protected set; }
    }
}