namespace D.Compiler
{ 
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override IExpression VisitLambda(LambdaExpression lambda)
        {
            Indent(level);

            Emit("=> ");

            Visit(lambda.Expression);

            return lambda;
        }

        public void WriteParameters(Parameter[] parameters, string start = "(", string end = ")")
        {
            Emit(start);

            var i = 0;

            foreach (var parameter in parameters)
            {
                if (i > 0) Emit(", ");

                WriteTypeSymbol(parameter.Type);

                Emit(" ");

                Emit(parameter.Name);

                i++;
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

                if (statement is ReturnStatement)
                {
                    var type = (ObjectInitializer)((ReturnStatement)statement).Expression;

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

        private void WriteTypeInitializerBody(ObjectInitializer type, int level)
        {
            var i = 0;

            foreach (var member in type.Properties)
            {
                if (++i != 1) EmitLine();

                Indent(level);
                Emit(ToPascalCase(member.Name));
                Emit(" = ");
                Visit(member.Value);
                Emit(";");
            }
        }

        // public double this[int integer] => this.elements[index];

        public void WriteIndexer(FunctionExpression func)
        {
            Indent(level);

            Emit("public ");

            WriteTypeSymbol(func.ReturnType);

            Emit(" this");
            
            WriteParameters(func.Parameters, "[", "]");

            WriteFunctionBody((BlockExpression)func.Body);
        }

        public void WriteProtocalFunction(Protocal protocal, FunctionExpression func)
        {
            Indent(level);
            
            WriteTypeSymbol(func.ReturnType);

            Emit(" ");

            Emit(protocal.Name);
            Emit(".");
            Emit(ToPascalCase(func.Name));

            if (func.GenericParameters.Length > 0)
            {
                Emit("<");

                Emit(func.GenericParameters[0].Name);

                Emit(">");
            }

            if (!func.IsProperty)
            {
                WriteParameters(func.Parameters, "(", ")");
            }

            WriteFunctionBody((BlockExpression)func.Body);
        }

        public void VisitFunction(FunctionExpression func)
        {
            if (func.IsAnonymous)
            {
                Emit(" => ");

                Visit(func.Body);

                return;
            }

            if (func.IsIndexer)
            {
                WriteIndexer(func);

                return;
            }

            Indent(level);

            Emit("public ");

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

            Emit(ToPascalCase(func.Name));

            if (func.GenericParameters.Length > 0)
            {
                Emit("<");

                Emit(func.GenericParameters[0].Name);

                Emit(">");
            }

            if (!func.IsProperty)
            {
                WriteParameters(func.Parameters, "(", ")");
            }

            WriteFunctionBody((BlockExpression)func.Body);
        }

        private void WriteFunctionBody(BlockExpression body)
        {
            // May need to nest in GET statement if it's a property

            if (body[0].Kind == Kind.ReturnStatement)
            {
                var returnStatement = (ReturnStatement)body[0];

                Emit(" => ");

                Visit(returnStatement.Expression);

                Emit(";");
            }
            else
            {
                EmitLine();

                VisitBlock(body);
            }
        }
    }
}
