// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace E.Inference;

public sealed class AbstractNode(VariableNode[] arguments, INode body, IType? type = null) : INode
{
    public VariableNode[] Arguments { get; } = arguments;

    public INode Body { get; } = body;

    public IType? Type { get; } = type;

    public override string ToString()
    {
        var args = string.Join<INode>(", ", Arguments);

        string type = Type?.ToString() ?? string.Empty;

        return $"({args}) {Body} -> {type}";
    }

    public IType Infer(Environment env, ReadOnlySpan<IType> types)
    {
        var scope = env.Nested();
        var known = new List<IType>();
        known.AddRange(types);

        var args = new List<IType>();

        foreach (var arg in Arguments)
        {
            IType type;
            if (arg is VariableNode variable)
            {
                if (variable.Type is not null)
                {
                    type = variable.Type;
                }
                else
                {
                    type = TypeSystem.NewGeneric();
                    known.Add(type);
                }

                scope[variable.Name] = type;
            }
            else
            {
                throw new Exception("Define nodes not supported");

                /*
                var spec = ((DefineNode)arg).Body;
                variable = (VariableNode)((DefineNode)arg).Spec;
                type = TypeSystem.NewGeneric();
                TypeSystem.Unify(type, TypeSystem.Infer(scope, spec, known));
                scope[variable.Id] = type;
                known.Add(type);
                */
            }

            args.Add(type);

            /*
            if (!type.Value.IsConstructor)
            {
                type.Value.Bind(variable.Id);
            }
            */
        }

        args.Add(TypeSystem.Infer(scope, Body is LetNode letBody ? letBody.Arguments[^1] : Body, CollectionsMarshal.AsSpan(known)));

        if (Type is not null)
        {
            TypeSystem.Unify(args[^1], Type);
        }

        return TypeSystem.NewType(TypeSystem.Function, [.. args]);
    }
}
