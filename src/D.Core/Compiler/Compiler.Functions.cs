using System;

namespace D
{
    using Expressions;
    using Syntax;

    public partial class Compiler
    {
        public FunctionExpression VisitFunctionDeclaration(FunctionDeclarationSyntax f, Type declaringType = null)
        {
            env = env.Nested(f.Name); // Create a nested scope...

            var paramaters = ResolveParameters(f.Parameters);

            foreach (var p in paramaters)
            {
                env.Add(p.Name, p.Type);
            }

            var b = Visit(f.Body);

            var returnType = f.ReturnType != null
                ? env.Get<Type>(f.ReturnType)
                : b.Kind == Kind.LambdaExpression
                    ? GetReturnType((LambdaExpression)b)
                    : GetReturnType((BlockExpression)b);

            BlockExpression body;

            if (f.Body == null)
            {
                body = null; // protocol functions are abstract and do not define a body
            }
            else if (f.Body is BlockSyntax blockSyntax)
            {
                body = VisitBlock(blockSyntax);
            }
            else if (f.Body is LambdaExpressionSyntax lambdaSyntax)
            {
                var lambda = VisitLambda(lambdaSyntax);

                body = new BlockExpression(new ReturnStatement(lambda.Expression));
            }
            else
            {
                throw new Exception("unexpected function body type:" + f.Body.Kind);
            }

            var result = new FunctionExpression(f.Name, returnType, paramaters)
            {
                GenericParameters = ResolveParameters(f.GenericParameters),
                Flags = f.Flags,
                Body = body,
                DeclaringType = declaringType
            };

            LeaveScope();
            
            if (result.Name != null)
            {
                env.Add(result.Name, result);
            }

            return result;
        }
    }
}
