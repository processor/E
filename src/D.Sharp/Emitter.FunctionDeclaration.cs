namespace E.Compilation
{
    using System.Collections.Generic;

    using Expressions;

    public partial class CSharpEmitter
    {
        public override IExpression VisitLambda(LambdaExpression lambda)
        {
            Indent(level);

            Emit("=> ");

            Visit(lambda.Expression);

            return lambda;
        }

        public void WriteParameters(IReadOnlyList<Parameter> parameters, char start = '(', char end = ')')
        {
            Emit(start);

            for (int i = 0; i < parameters.Count; i++)
            {
                Parameter parameter = parameters[i];

                if (i > 0) Emit(", ");

                WriteTypeSymbol(parameter.Type);

                Emit(' ');

                Emit(parameter.Name ?? "_" + i);
            }

            Emit(end);
        }
 
        public void WriteConstructor(FunctionExpression func)
        {
            Indent(level);
            Emit("public ");

            Emit(func.DeclaringType.Name);

            WriteParameters(func.Parameters);

            EmitLine();

            var block = (BlockExpression)func.Body;

            Emit("{", level);

            level++;

            foreach (var statement in block.Statements)
            {
                EmitLine();

                if (statement is ReturnStatement returnStatement)
                {
                    var type = (TypeInitializer)returnStatement.Expression;

                    WriteTypeInitializerBody(type, level);
                }
                else
                {
                    Indent(level);

                    Visit(statement);
                }

                EmitLine();
            }

            level--;

            Emit("}", level);
        }

        private void WriteTypeInitializerBody(TypeInitializer type, int level)
        {
            var i = 0;

            foreach (var member in type.Arguments)
            {
                if (++i != 1) EmitLine();

                Indent(level);
                EmitPascalCase(member.Name);
                Emit(" = ");
                Visit((IObject)member.Value);
                Emit(";");
            }
        }

        public void WriteProtocolFunction(ProtocolExpression protocol, FunctionExpression func)
        {
            Indent(level);

            if (func.Visibility == Visibility.Private)
            {
                Emit("private ");
            }

            WriteTypeSymbol(func.ReturnType);

            Emit(" ");

            // Protocols may contain helper functions...

            if (protocol.Contains(func.Name))
            {
                Emit(protocol.Name.Name);
                Emit(".");
            }

            EmitPascalCase(func.Name);

            if (func.GenericParameters is { Count: > 0 })
            {
                WriteGenericParameters(func.GenericParameters);
            }

            if (!func.IsProperty)
            {
                WriteParameters(func.Parameters, '(', ')');
            }

            WriteFunctionBody((BlockExpression)func.Body);
        }

        public override IExpression VisitFunction(FunctionExpression func)
        {
            if (func.IsAnonymous)
            {
                WriteParameters(func.Parameters, '(', ')');

                Emit(" => ");

                Visit(func.Body);

                return func;
            }

            Indent(level);

            Emit(func.Visibility == Visibility.Private ? "private " : "public ");

            if (func.IsIndexer)
            {
                // public double this[int integer] => this.elements[index];

                WriteTypeSymbol(func.ReturnType);

                Emit(" this");

                WriteParameters(func.Parameters, '[', ']');

                WriteFunctionBody((BlockExpression)func.Body);

                return func;
            }

            if (func.IsStatic && !func.IsProperty)
            {
                Emit("static ");
            }

            WriteTypeSymbol(func.ReturnType);

            Emit(" ");

            if (func.IsOperator)
            {
                Emit("operator ");
            }

            EmitPascalCase(func.Name);

            if (func.GenericParameters.Count > 0)
            {
                WriteGenericParameters(func.GenericParameters);
            }

            if (!func.IsProperty)
            {
                WriteParameters(func.Parameters, '(', ')');
            }

            WriteFunctionBody((BlockExpression)func.Body);

            return func;
        }

        private void WriteGenericParameters(IReadOnlyList<Parameter> parameters)
        {
            Emit('<');

            for (var i = 0; i < parameters.Count; i++)
            {
                if (i > 0) Emit(",");

                Emit(parameters[i].Name);
            }

            Emit('>');
        }

        private void WriteFunctionBody(BlockExpression body)
        {
            // May need to nest in GET statement if it's a property

            if (body[0].Kind == ObjectType.ReturnStatement)
            {
                var returnStatement = (ReturnStatement)body[0];

                Emit(" => ");

                Visit(returnStatement.Expression);

                Emit(';');
            }
            else
            {
                EmitLine();

                VisitBlock(body);
            }
        }
    }
}
