// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;

namespace D.Inference
{
    public sealed class AbstractNode : Node
    {
        public override string ToString()
        {
            var args = string.Join<Node>(", ", Arguments);

            string type = Type?.ToString() ?? string.Empty;

            return $"({args}) {Body} -> {type}";
        }

        public override IType Infer(Environment env, IReadOnlyList<IType> types)
        {
            var scope = env.Nested();
            var known = new List<IType>(types);
            var args = new List<IType>();

            foreach (var arg in Arguments)
            {
                IType type;
                if (arg is VariableNode varNode)
                {
                    if (varNode.Type != null)
                    {
                        type = varNode.Type as IType ?? scope[(string)varNode.Type];
                    }
                    else
                    {
                        type = TypeSystem.NewGeneric();
                        known.Add(type);
                    }

                    scope[varNode.Id] = type;
                }
                else
                {
                    var spec = ((DefineNode)arg).Body;
                    varNode = (VariableNode)((DefineNode)arg).Spec;
                    type = TypeSystem.NewGeneric();
                    TypeSystem.Unify(type, TypeSystem.Infer(scope, spec, known));
                    scope[varNode.Id] = type;
                    known.Add(type);
                }

                args.Add(type);

                if (!type.Value.IsConstructor)
                {
                    type.Value.Bind(varNode.Id);
                }
            }

            args.Add(TypeSystem.Infer(scope, Body is LetNode ? Body.Arguments[Body.Arguments.Length - 1] : Body, known));

            if (Type != null)
            {
                TypeSystem.Unify(args[args.Count - 1], Type as IType ?? scope[(string)Type]);
            }

            return TypeSystem.NewType(TypeSystem.Function, args.ToArray());
        }
    }
}