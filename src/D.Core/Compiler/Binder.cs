using System;

using D.Expressions;

namespace D
{
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

            throw new Exception("Block has no return statement");
        }

        public Type GetReturnType(LambdaExpression lambda)
        {
            return GetType(lambda.Expression);
        }

        public Type GetType(IExpression expression)
        {
            if (expression is Symbol name && env.TryGetValue(name, out IObject obj))
            {
                return (Type)obj;
            }

            switch (expression)
            {
                case CallExpression call:
                    return call.ReturnType ?? Type.Get(Kind.Object);

                case InterpolatedStringExpression _:
                    return Type.Get(Kind.String);

                case TypeSymbol symbol:
                    return new Type(symbol.Name, Type.Get(Kind.Object), null, null);

                case BinaryExpression _:
                case UnaryExpression _:
                case IndexAccessExpression _:
                case Symbol _:
                case MatchExpression _:
                    return Type.Get(Kind.Object);

                case TypeInitializer initializer:
                    return env.Get<Type>(initializer.Type);

                case ArrayInitializer array:
                    return new Type(Kind.Array, (Type)array.ElementType);
            }

            if (expression.Kind != Kind.Object)
            {
                return Type.Get(expression.Kind);
            }

            throw new Exception("Unexpected expression:" + expression.Kind + "/" + expression.ToString());
        }
    }
}