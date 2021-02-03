// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;

namespace E.Inference
{
    public sealed class VariableNode : Node
    {
        public VariableNode(string name, IType type)
        {
            Spec = name;
            Type = type;
        }

        public override string ToString() => Id;

        public override IType Infer(Environment env, IReadOnlyList<IType> types)
        {
            return TypeSystem.Fresh(env[Id], types);
        }

        public string Id => (string)Spec;
    }

    // AKA identity node...
}