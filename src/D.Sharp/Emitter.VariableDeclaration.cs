namespace E.Compilation;

using Expressions;

public partial class CSharpEmitter
{
    public override IExpression VisitVariableDeclaration(VariableDeclaration declaration)
    {
        Indent(level);

        if (declaration.Value is MatchExpression)
        {
            WriteVariableDeclarationWithMatch(declaration);

            return declaration;
        }

        WriteTypeSymbol(declaration.Type ?? Type.Get(ObjectType.Object));

        Emit(' ');
        Emit(declaration.Name);
        Emit(" = ");
        Visit(declaration.Value);
        Emit(';');

        return declaration;
    }

    private void WriteVariableDeclarationWithMatch(VariableDeclaration expression)
    {
        WriteTypeSymbol(expression.Type ?? Type.Get(ObjectType.Object));

        Emit(' ');

        EmitLine(expression.Name + ";");

        EmitLine();

        var match = (MatchExpression)expression.Value;

        Emit("switch (");
        Visit(match.Expression);
        EmitLine(")");
        EmitLine("{");

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
            Emit(';');

            EmitLine(" break;");
        }

        Emit('}');
    }
}
