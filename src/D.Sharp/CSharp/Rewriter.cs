using System;
using System.Collections.Generic;
using System.IO;

namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler : ExpressionVisitor
    {
        private int level = 0;
        private readonly TextWriter writer;

        public CSharpTranspiler(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Emit(string text, int level)
        {
            Indent(level);

            writer.Write(text);
        }

        public void Emit(string text)
        {
            writer.Write(text);
        }

        public void EmitLine()
        {
            writer.WriteLine();
        }

        public void EmitLine(string text)
        {
            writer.WriteLine(text);
        }

        public void EmitLine(string text, int level)
        {
            Indent(level);

            writer.WriteLine(text);
        }

        public void Visit(IEnumerable<IExpression> expressions)
        {
            var i = 0;

            foreach(var statement in expressions)
            {
                if (++i != 1) EmitLine();

                Visit(statement);
            }
        }

        public static string ToPascalCase(string text)
            => text.Substring(0, 1).ToUpper() + text.Substring(1);

        public override void VisitSymbol(Symbol symbol)
        {
            if (symbol.Name[0] == '$')
            {
                Emit("_" + symbol.Name.Substring(1));
            }
            else if (symbol.Name == "π")
            {
                Emit("Math.PI");
            }
            else
            {
                Emit(symbol.Name);
            }
        }

        public void WriteString(StringLiteral text)
        {
            writer.Write('"');
            Emit(text.Value);
            writer.Write('"');
        }

        public override void VisitReturn(ReturnStatement type)
        {
            Indent(level);
            Emit("return ");

            Visit(type.Expression);

            Emit(";");
        }

        public override void VisitIf(IfStatement type)
        {
            Indent(level);
            Emit("if (");

            Visit(type.Condition);

            writer.WriteLine(")");

            VisitBlock(type.Body);

            if (type.ElseBranch != null)
            {
                EmitLine();

                Visit(type.ElseBranch);
            }
        }

        public override void VisitElseIf(ElseIfStatement type)
        {
            Indent(level);
            Emit("else if (");

            Visit(type.Condition);

            writer.WriteLine(")");

            VisitBlock(type.Body);

            if (type.ElseBranch != null)
            {
                writer.WriteLine();

                Visit(type.ElseBranch);
            }
        }

        public override void VisitElse(ElseStatement type)
        {
            Emit("else");
            writer.WriteLine();

            VisitBlock(type.Body);
        }
        
        public void Indent(int level)
        {
            for (var i = 0; i < level; i++)
            {
                writer.Write("    ");
            }
        }

        public override void VisitConstant(IObject value)
        {
            switch (value.Kind)
            {
                case Kind.String:
                    WriteString((StringLiteral)value);

                    break;
                default:
                    writer.Write(value.ToString());
                    break;
            }
        }

        private static readonly Env _env = new Env();

        public void WriteTypeSymbol(Symbol symbol)
        {
            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol));

            WriteTypeSymbol(_env.GetType(symbol));
        }

        public void WriteTypeSymbol(IType type)
        {
            // Determine if the domain is being used

            if (type.Arguments != null && type.Arguments.Length > 0)
            {
                var i = 0;

                Emit(type.Name);

                Emit("<");

                foreach (var arg in type.Arguments)
                {
                    if (i > 0) Emit(",");

                    WriteTypeSymbol(arg);

                    i++;
                }

                Emit(">");

                return;
            }

            switch (type.Name)
            {
                case "Any"      : Emit("object");     break;
                case "Decimal"  : Emit("decimal");    break;
                case "Int16"    : Emit("short");      break;
                case "Int32"    : Emit("int");        break;
                case "Int64"    : Emit("long");       break;
                case "Float32"  : Emit("float");      break;
                case "Integer"  : Emit("long");       break;
                case "Float"    : Emit("double");     break;
                case "Float64"  : Emit("double");     break;
                case "String"   : Emit("string");     break;
                case "Number"   : Emit("double");     break;
                default         : Emit(type.Name);    break;
            }
        }
              
    }
}
