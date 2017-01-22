namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override void VisitVariableDeclaration(VariableDeclaration declaration)
        {
            Indent(level);

            if (declaration.Value is MatchExpression)
            {
                WriteVariableDeclarationWithMatch(declaration);

                return;
            }
           
            WriteTypeSymbol(declaration.Type != null ? _env.GetType(declaration.Type) : new Type(declaration.Value.Kind));
         
            Emit(" ");
            Emit(declaration.Name);
            Emit(" = ");
            Visit(declaration.Value);
            Emit(";");
        }


        private void WriteVariableDeclarationWithMatch(VariableDeclaration expression)
        {
            WriteTypeSymbol(expression.Type ?? Symbol.Any);

            Emit(" ");

            EmitLine(expression.Name + ";");

            EmitLine();

            var match = (MatchExpression)expression.Value;
          
            Emit("switch (");
            Emit(match.Expression.ToString());
            writer.WriteLine(")");
            writer.WriteLine("{");

            foreach (var c in match.Cases)
            {
                Indent(1);

                if (c.Pattern is AnyPattern)
                {
                    Emit("default");
                }
                else
                {
                    Emit("case ");
                    Visit(c.Pattern);
                }

                Emit(": ");
                Emit(expression.Name + " = ");
                Visit(c.Body.Expression);
                Emit(";");

                EmitLine(" break;");
            }

            Emit("}");
        }
    }
}