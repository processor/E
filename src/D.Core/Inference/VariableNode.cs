// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;

namespace D.Inference
{
    public sealed class VariableNode : Node
    {
        public override string ToString() => Id;

        public override IType Infer(Environment env, IReadOnlyList<IType> types)
        {
            return TypeSystem.Fresh(env[Id], types);
        }

        public string Id => (string)Spec;

        public bool IsConstructor => Type is IType type && type.IsConstructor;
    }

    // AKA identity node...
}