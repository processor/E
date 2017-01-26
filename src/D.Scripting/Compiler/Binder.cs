using System;

using D.Expressions;

namespace D.Compilation
{
    public partial class Compiler
    {
         public Type GetReturnType(BlockStatement block)
        {
            foreach (var x in block.Statements)
            {
                if (x is ReturnStatement)
                {
                    var returnStatement = (ReturnStatement)x;

                    return GetType(returnStatement.Expression);
                }
            }

            throw new Exception("Block has no return statement");
        }

        public Type GetType(IExpression expression)
        {
            if (expression is Symbol)
            {
                IObject obj;

                if (scope.TryGet((Symbol)expression, out obj))
                {
                    return (Type)obj;
                }
            }

            switch (expression.Kind)
            {
                case Kind.Number                       : return Type.Get(Kind.Number); // Double
                case Kind.Integer                      : return Type.Get(Kind.Integer);
                case Kind.Decimal                      : return Type.Get(Kind.Decimal);
                case Kind.String                       : return Type.Get(Kind.String);
                case Kind.InterpolatedStringExpression : return Type.Get(Kind.String);
                case Kind.MatrixLiteral                : return Type.Get(Kind.Matrix);
            }

            if (expression is BinaryExpression || expression is Symbol || expression is CallExpression || expression is UnaryExpression)
            {
                // TODO: Infer
                return Type.Get(Kind.Any);
            }

            if (expression.Kind == Kind.TypeInitializer)
            {
                var initializer = (TypeInitializer)expression;

                return scope.Get<Type>(initializer.Type);
            }

            // Any
            if (expression is IndexAccessExpression || expression is MatchExpression)
            {
                return Type.Get(Kind.Any);
            }

            throw new Exception("Unexpected expression:" + expression.Kind + "/" + expression.ToString());
        }

        public Type GetType(LambdaExpression lambda)
        {
            return GetType(lambda.Expression);
        }
    }
}
