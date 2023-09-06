namespace E.Compilation;

public partial class CSharpEmitter
{
    public void WriteProperties(ReadOnlySpan<Property> properties)
    {
        int i = 0;

        foreach (var property in properties)
        {
            if (++i != 1)
            {
                EmitLine();
                EmitLine();
            }

            Indent(level);

            Emit("public ");

            WriteTypeSymbol(property.Type);

            Emit(' ');

            EmitPascalCase(property.Name);

            Emit(" { get; ");

            if (property.IsMutable)
            {
                Emit("set; ");
            }

            Emit('}');
        }
    }

    public void VisitConstructor(string name, ReadOnlySpan<Property> properties)
    {
        Indent(level);

        Emit("public " + name);

        Emit('(');

        int i = 0;

        foreach (var property in properties)
        {
            if (++i != 1) Emit(", ");

            WriteTypeSymbol(property.Type);
            Emit(' ');
            Emit(property.Name);
        }

        writer.Write(')');
        writer.Write('\n');

        EmitLine("{", level);

        level++;

        foreach (var member in properties)
        {
            Indent(level);

            EmitPascalCase(member.Name);
            Emit(" = ");
            Emit(member.Name);
            EmitLine(";");
        }

        level--;

        Emit("}", level);
    }
}
