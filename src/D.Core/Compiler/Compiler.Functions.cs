using System;

using E.Expressions;
using E.Syntax;

namespace E;

public partial class Compiler
{
    public FunctionExpression VisitFunctionDeclaration(FunctionDeclarationSyntax syntax, Type? declaringType = null)
    {
        // TODO: create a nested scope...

        var paramaters = ResolveParameters(syntax.Parameters);

        for (int i = 0; i < paramaters.Length; i++)
        {
            Parameter p = paramaters[i];

            flow.Define(p.Name ?? $"_{i}", p.Type);
        }

        // NOTE: protocol functions are abstract and do not define a body
        IExpression? body = syntax.Body is not null ? Visit(syntax.Body) : null;
            
        Type? returnType;

        if (syntax.ReturnType is not null)
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
            throw new Exception($"unexpected function body type. Was {syntax.Body?.Kind}");
        }

        var result = new FunctionExpression(
            name              : syntax.Name,
            genericParameters : ResolveParameters(syntax.GenericParameters),
            parameters        : paramaters,
            returnType        : returnType,
            body              : body,
            flags             : syntax.Flags) {

            DeclaringType = declaringType
        };

        // TODO: Leave scope

        if (result.Name is not null)
        {
            env.Add(result.Name, result);
        }

        return result;
    }
}