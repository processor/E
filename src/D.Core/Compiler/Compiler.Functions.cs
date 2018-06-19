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

            // NOTE: protocol functions are abstract and do not define a body
            var body = Visit(f.Body);


            Type returnType = null;

            if (f.ReturnType != null)
            {
                returnType = env.Get<Type>(f.ReturnType);
            }
            else if (body is LambdaExpression lambda)
            {
                returnType = GetReturnType(lambda);

                body = new BlockExpression(new ReturnStatement(lambda.Expression));
            }
            else if (body is BlockExpression block)
            {
                returnType = GetReturnType(block);
            }
            else
            {
                throw new Exception("unexpected function body type:" + f.Body.Kind);
            }

            var result = new FunctionExpression(f.Name, returnType, paramaters) {
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
