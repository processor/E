// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;
using System.Linq;

namespace D.Inference
{
    public sealed class ApplyNode : Node
    {
        private bool IsFunction(IType type)
        {
            if (type is null) return false;

            return type.BaseType is not null ? type.BaseType == TypeSystem.Function : IsFunction(type.Self);
        }

        private static Node ToFormal(Environment env, IReadOnlyList<IType> types, Node arg)
        {
            return arg is VariableNode argVar 
                ? Define(argVar, Constant(env[argVar.Id])) 
                : arg;
        }

        private IType? AsAnnotationType(Environment env, IReadOnlyList<IType> types)
        {
            if (Spec is Node spec && spec.Type is IType ctor && !IsFunction(ctor))
            {
                for (int i = 0; i < Arguments.Length; i++)
                {
                    var arg = Arguments[i];

                    if (arg is VariableNode argVar)
                    {
                        TypeSystem.Infer(env, Define(argVar, Constant(ctor.ArgumentTypes[i])), types);
                    }
                }


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
            if (Type is null && AsAnnotationType(env, types) is IType annotation)
            {
                return annotation;
            }

            List<IType> args = Arguments.Select(arg => TypeSystem.Infer(env, ToFormal(env, types, arg), types)).ToList();

            var expression = (Node)Spec!;

            var self = TypeSystem.Infer(env, expression, types);

            IType result;

            if (Type is not null)
            {
                var ctor = Type;
                result = TypeSystem.Infer(env, Apply(Variable(ctor.Name, ctor), args.Select(arg => Constant(arg)).ToArray()), types);
            }
            else
            {
                result = TypeSystem.NewGeneric();

                args.Add(result);

                TypeSystem.Unify(TypeSystem.NewType(TypeSystem.Function, args.ToArray()), self);
            }
            return result;
        }
    }
}