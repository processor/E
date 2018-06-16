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
                if (arg is VariableNode variable)
                {
                    if (variable.Type != null)
                    {
                        type = variable.Type as IType;
                    }
                    else
                    {
                        type = TypeSystem.NewGeneric();
                        known.Add(type);
                    }

                    scope[variable.Id] = type;
                }
                else
                {
                    var spec = ((DefineNode)arg).Body;
                    variable = (VariableNode)((DefineNode)arg).Spec;
                    type = TypeSystem.NewGeneric();
                    TypeSystem.Unify(type, TypeSystem.Infer(scope, spec, known));
                    scope[variable.Id] = type;
                    known.Add(type);
                }

                args.Add(type);

                /*
                if (!type.Value.IsConstructor)
                {
                    type.Value.Bind(variable.Id);
                }
                */
            }

            args.Add(TypeSystem.Infer(scope, Body is LetNode ? Body.Arguments[Body.Arguments.Length - 1] : Body, known));

            if (Type != null)
            {
                TypeSystem.Unify(args[args.Count - 1], Type);
            }

            return TypeSystem.NewType(TypeSystem.Function, args.ToArray());
        }
    }
}