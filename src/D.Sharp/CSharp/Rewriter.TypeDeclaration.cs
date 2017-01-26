using System.Collections.Generic;

namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public void WriteProperties(IEnumerable<Property> properties)
        {
            var i = 0;

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

                Emit(" ");

                Emit(ToPascalCase(property.Name));

                Emit(" { get; ");

                if (property.IsMutable)
                {
                    Emit("set; ");
                }

                Emit("}");
            }
        }

        public void VisitConstructor(string name, IEnumerable<Property> properties)
        {
            Indent(level);

            Emit("public " + name);

            Emit("(");

            var i = 0;

            foreach (var property in properties)
            {
                if (++i != 1) Emit(", ");

                WriteTypeSymbol(property.Type);
                Emit(" ");
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
       
        public override IExpression VisitTypeInitializer(TypeInitializer type)
        {
            Emit("new ");

            WriteTypeSymbol(type.Type);
            
            Emit("(");
            
            var i = 0;

            foreach (var member in type.Members)
            {
                if (++i != 1) Emit(", ");

                Emit(member.Name);

                Emit(": ");

                Visit(member.Value);
            }

            Emit(")");

            return type;
        }
    }
}