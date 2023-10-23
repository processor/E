using System;

using E.Expressions;
using E.Symbols;

namespace E;

public partial class Compiler
{
    public Type GetReturnType(BlockExpression block)
    {
        foreach (var expression in block.Statements)
        {
            if (expression is ReturnStatement returnStatement)
            {
                return GetType(returnStatement.Expression);
            }
        }

        throw new Exception($"Block has no return statement. Was {block[0]}");
    }

    public Type GetReturnType(LambdaExpression lambda)
    {
        return GetType(lambda.Expression);
    }

    public Type GetType(IObject expression)
    {
        if (expression is Symbol name)
        {
            return _env.Get<Type>(flow.Infer(name.Name).Name);

            /*
            if (env.TryGetValue(name, out Type obj))
            {
                return obj;
            }
            */
        }

        switch (expression)
        {
            case MemberAccessExpression access:
                Type type = GetType(access.Left) ?? throw new Exception("no type found for left");

                var member = type.GetProperty(access.MemberName);

                if (member is null) return Type.Get(ObjectType.Object);

                // if (member is null) throw new Exception($"{type.Name}::{access.MemberName} not found");

                return member.Type;

            case CallExpression call:
                return call.ReturnType ?? Type.Get(ObjectType.Object);

            case InterpolatedStringExpression _:
                return Type.Get(ObjectType.String);

            case TypeSymbol symbol:
                return new Type(symbol.Name, Type.Get(ObjectType.Object), null, null);

            case BinaryExpression b:
                if (b.Operator.IsComparison || b.Operator.IsLogical)
                {
                    return Type.Get(ObjectType.Boolean);
                }
                else
                {
                    // throw new Exception($"{b.Left.GetType()}/{b.Right.GetType()}");

                    var lhsType = GetType(b.Left);
                    var rhsType = GetType(b.Right);

                    /*
                    var apply = D.Inference.Node.Apply(D.Inference.Node.Variable("operator-symbol"), [
                        D.Inference.Node.Constant(lhs), // lhs
                        D.Inference.Node.Constant(rhs)  // rhs
                    ]);

                    var r = flow.Infer(apply);
                    */

                    if (ReferenceEquals(lhsType, rhsType))
                    {
                        return lhsType;
                    }
                    else
                    {
                        return Type.Get(ObjectType.Object);
                    }
                }


            case UnaryExpression _:
            case IndexAccessExpression _:
            case Symbol _:
            case MatchExpression _:
                return Type.Get(ObjectType.Object);

            case TypeInitializer initializer:
                return _env.Get<Type>(initializer.Type);

            case TupleExpression tuple:
                {
                    var args = new Type[tuple.Elements.Length];

                    for (var i = 0; i < tuple.Elements.Length; i++)
                    {
                        args[i] = GetType(tuple.Elements[i]);
                    }

                    return new Type(ObjectType.Tuple, args);
                }

            case ArrayInitializer array:
                return new Type(ObjectType.Array, array.ElementType!);
        }

        if (expression.Kind is not ObjectType.Object)
        {
            return Type.Get(expression.Kind);
        }

        throw new Exception($"Unexpected expression:{expression.Kind}/{expression}");
    }
}