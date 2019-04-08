using System;

namespace D
{
    using Expressions;
    using Syntax;

    public partial class Compiler
    {
        public FunctionExpression VisitFunctionDeclaration(FunctionDeclarationSyntax syntax, Type declaringType = null)
        {
            // TODO: create a nested scope...

            var paramaters = ResolveParameters(syntax.Parameters);

            foreach (var p in paramaters)
            {
                flow.Define(p.Name, p.Type);
            }

            // NOTE: protocol functions are abstract and do not define a body
            var body = Visit(syntax.Body);
            
            Type returnType = null;

            if (syntax.ReturnType != null)
            {
                returnType = env.Get<Type>(syntax.ReturnType);
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
                throw new Exception("unexpected function body type:" + syntax.Body.Kind);
            }

            var result = new FunctionExpression(syntax.Name, returnType, paramaters) {
                GenericParameters = ResolveParameters(syntax.GenericParameters),
                Flags = syntax.Flags,
                Body = body,
                DeclaringType = declaringType
            };

            // TODO: Leave scope

            if (result.Name != null)
            {
                env.Add(result.Name, result);
            }

            return result;
        }
    }
}
