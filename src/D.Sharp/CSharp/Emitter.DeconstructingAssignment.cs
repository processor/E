﻿using System;

using D.Expressions;

namespace D.Compilation
{
    public partial class CSharpEmitter
    {
        public override IExpression VisitDestructuringAssignment(DestructuringAssignment expression)
        {
            var i = 0;

            foreach (var a in expression.Variables)
            {
                if (a.Name.Equals("_", StringComparison.Ordinal)) continue;

                if (i != 0) EmitLine();

                Indent(level);                
                Emit("var ");

                Emit(a.Name);

                Emit(" = ");

                Visit(expression.Expression);
                Emit('.');
                Emit(ToPascalCase(a.Name));
                Emit(';');

                i++;
            }

            return expression;
        }
    }
}
