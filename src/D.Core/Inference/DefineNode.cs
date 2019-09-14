// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;

namespace D.Inference
{
    public sealed class DefineNode : Node
    {
        public DefineNode(Node spec, Node body)
        {
            Spec = spec;
            Body = body;
        }

        public override string ToString() => $"{Spec} = {Body}";

        public override IType Infer(Environment env, IReadOnlyList<IType> types)
        {
            var known = new List<IType>(types);
            var type = TypeSystem.NewGeneric();
            var varNode = (VariableNode)Spec;
            
            env[varNode.Id] = type;

            known.Add(type);

            TypeSystem.Unify(type, TypeSystem.Infer(env, Body, known));

            /*
            if (type.Value.IsConstructor)
            {
                type.Value.Bind(varNode.Id);
            }
            */

            return env[varNode.Id] = type.Value;
        }
    }

    // Assign?
}