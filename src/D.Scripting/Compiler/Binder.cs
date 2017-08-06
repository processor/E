using System;

using D.Expressions;

namespace D.Compilation
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

        public Type GetType(IExpression expression)
        {
            if (expression is Symbol name && scope.TryGet(name, out IObject obj))
            {
                return (Type)obj;
            }

            switch (expression)
            {
                case InterpolatedStringExpression _:
                    return Type.Get(Kind.String);

                case TypeSymbol symbol:
                    return new Type(symbol.Name, Type.Get(Kind.Object), null, null);

                case BinaryExpression _:
                case CallExpression _:
                case UnaryExpression _:
                case IndexAccessExpression _:
                case Symbol _:
                case MatchExpression _:
                    return Type.Get(Kind.Object);

              

                case ArrayInitializer array:
                    return new Type(Kind.List, (Type)array.ElementType);
            }

            if (expression.Kind == Kind.ObjectInitializer)
            {
                var initializer = (ObjectInitializer)expression;

                return scope.Get<Type>(initializer.Type);
            }

            if (expression.Kind != Kind.Object)
            {
                return Type.Get(expression.Kind);
            }

            throw new Exception("Unexpected expression:" + expression.Kind + "/" + expression.ToString());
        }

        public Type GetType(LambdaExpression lambda) =>  GetType(lambda.Expression);
    }
}
