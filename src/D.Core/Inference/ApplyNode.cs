// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;
using System.Linq;

namespace D.Inference
{
    public class ApplyNode : Node
    {
        private bool IsFunction(IType type)
        {
            if (type == null) return false;

            return type.Constructor != null ? type.Constructor == TypeSystem.Function : IsFunction(type.Self);
        }

        private Node ToFormal(Environment env, IReadOnlyList<IType> types, Node arg)
        {
            IType typed(VariableNode var)
            {
                return env[var.Id];
            }

            return arg is VariableNode argVar ? Define(argVar, Const(typed(argVar))) : arg;
        }

        private IType AsAnnotationType(Environment env, IReadOnlyList<IType> types)
        {
            if (Spec is Node spec && spec.Type is IType ctor && !IsFunction(ctor))
            {
                Arguments.Select((arg, i) => arg is VariableNode argVar
                    ? TypeSystem.Infer(env, Define(argVar, Const(ctor.Arguments[i])), types)
                    : null
                ).ToArray();

                return TypeSystem.NewType(ctor, Arguments.Select(arg => TypeSystem.Infer(env, ToFormal(env, types, arg), types)).ToArray());
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            var args = string.Join(", ", Arguments.Select(arg => arg.ToString()).ToArray());

            return $"{Spec} ({args})";
        }

        public override IType Infer(Environment env, IReadOnlyList<IType> types)
        {
            if (Type == null && AsAnnotationType(env, types) is IType annotation)
            {
                return annotation;
            }

            List<IType> args = Arguments.Select(arg => TypeSystem.Infer(env, ToFormal(env, types, arg), types)).ToList();

            var expression = (Node)Spec;

            var self = TypeSystem.Infer(env, expression, types);

            IType @out;

            if (Type != null)
            {
                var ctor = Type as IType ?? env[(string)Type];
                @out = TypeSystem.Infer(env, Apply(Var(ctor.Id, ctor), args.Select(arg => Const(arg)).ToArray()), types);
            }
            else
            {
                @out = TypeSystem.NewGeneric();
                args.Add(@out);
                TypeSystem.Unify(TypeSystem.NewType(TypeSystem.Function, args.ToArray()), self);
            }
            return @out;
        }
    }
}