// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;
using System.Collections.Generic;
using System.Linq;

namespace E.Inference;

public sealed class ApplyNode(
    VariableNode variable,
    INode[] arguments,
    IType? type = null) : INode
{
    public VariableNode Variable { get; } = variable;

    public INode[] Arguments { get; } = arguments;

    public IType? Type { get; } = type;

    private static bool IsFunction(IType? type)
    {
        if (type is null) return false;

        return type.BaseType is not null ? type.BaseType == TypeSystem.Function : IsFunction(type.Self);
    }

    private static INode ToFormal(Environment env, ReadOnlySpan<IType> types, INode arg)
    {
        return arg is VariableNode argVar 
            ? new DefineNode(argVar, new ConstantNode(env[argVar.Name])) 
            : arg;
    }

    private IType? AsAnnotationType(Environment env, ReadOnlySpan<IType> types)
    {
        if (Variable.Type is IType ctor && !IsFunction(ctor))
        {
            for (int i = 0; i < Arguments.Length; i++)
            {
                var arg = Arguments[i];

                if (arg is VariableNode argVar)
                {
                    TypeSystem.Infer(env, new DefineNode(argVar, new ConstantNode(ctor.ArgumentTypes[i])), types);
                }
            }

           var inferredArgs = new IType[Arguments.Length];

            for (int i = 0; i < Arguments.Length; i++)
            {
                inferredArgs[i] = TypeSystem.Infer(env, ToFormal(env, types, Arguments[i]), types);
            }

            return TypeSystem.NewType(ctor, inferredArgs);
        }
        else
        {
            return null;
        }
    }

    public override string ToString()
    {
        var args = string.Join(", ", Arguments.Select(arg => arg));

        return $"{Variable} ({args})";
    }

    public IType Infer(Environment env, ReadOnlySpan<IType> types)
    {
        if (Type is null && AsAnnotationType(env, types) is IType annotation)
        {
            return annotation;
        }

        var args = new List<IType>(Arguments.Length);

        for (int i = 0; i < Arguments.Length; i++)
        {
            args.Add(TypeSystem.Infer(env, ToFormal(env, types, Arguments[i]), types));
        }

        var expression = Variable;

        var self = TypeSystem.Infer(env, expression, types);

        IType result;

        if (Type is not null)
        {
            var ctor = Type;
            result = TypeSystem.Infer(env, Node.Apply(new VariableNode(ctor.Name, ctor), args.Select(arg => new ConstantNode(arg)).ToArray()), types);
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