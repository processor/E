﻿namespace E.Compilation;

using Expressions;

public partial class CSharpEmitter
{
    public override IExpression VisitIndexAccess(IndexAccessExpression expression)
    {
        Visit(expression.Left);
        Emit('[');
        Visit((IObject)expression.Arguments[0]);
        Emit(']');

        return expression;
    }

    public override IExpression VisitMemberAccess(MemberAccessExpression expression)
    {
        Visit(expression.Left);
        Emit('.');
        EmitPascalCase(expression.MemberName);

        return expression;
    }
}