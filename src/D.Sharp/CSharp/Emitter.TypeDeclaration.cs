using System.Collections.Generic;

namespace D.Compilation
{
    public partial class CSharpEmitter
    {
        public void WriteProperties(IEnumerable<Property> properties)
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

                Emit(ToPascalCase(property.Name));

                Emit(" { get; ");

                if (property.IsMutable)
                {
                    Emit("set; ");
                }

                Emit('}');
            }
        }

        public void VisitConstructor(string name, IEnumerable<Property> properties)
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

            writer.WriteLine(")");

            EmitLine("{", level);

            level++;

            foreach (var member in properties)
            {
                Indent(level);

                Emit(ToPascalCase(member.Name));
                Emit(" = ");
                Emit(member.Name);
                EmitLine(";");
            }

            level--;

            Emit("}", level);
        }
    }
}