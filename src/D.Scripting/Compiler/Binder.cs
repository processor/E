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

            switch (expression.Kind)
            {
                case Kind.Number                       : return Type.Get(Kind.Number); // Double
                case Kind.Decimal                      : return Type.Get(Kind.Decimal);
                case Kind.String                       : return Type.Get(Kind.String);
                case Kind.Matrix                       : return Type.Get(Kind.Matrix);
                case Kind.InterpolatedStringExpression : return Type.Get(Kind.String);
            }

            switch (expression)
            {
                case BinaryExpression _:
                case Symbol _:
                case CallExpression _:
                case UnaryExpression _:
                case IndexAccessExpression _:
                case MatchExpression _:
                case ArrayInitializer _:
                    // TODO: Infer
                    return Type.Get(Kind.Object);
            }

            if (expression.Kind == Kind.ObjectInitializer)
            {
                var initializer = (ObjectInitializer)expression;

                return scope.Get<Type>(initializer.Type);
            }

          
            throw new Exception("Unexpected expression:" + expression.Kind + "/" + expression.ToString());
        }

        public Type GetType(LambdaExpression lambda)
            =>  GetType(lambda.Expression);
    }
}
