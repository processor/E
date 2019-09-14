﻿using System;
using System.Collections.Generic;
using System.IO;

namespace D.Compilation
{
    using Expressions;

    public partial class CSharpEmitter : ExpressionVisitor
    {
        private int level = 0;
        private readonly TextWriter writer;

        public CSharpEmitter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Emit(string text, int level)
        {
            Indent(level);

            writer.Write(text);
        }
        
        public void Emit(char text)
        {
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
        {
           return text.Substring(0, 1).ToUpper() + text.Substring(1);
        }

        public override IExpression VisitSymbol(Symbol symbol)
        {
            if (symbol.Name[0] == '$')
            {
                Emit('_');
                Emit(symbol.Name.Substring(1));
            }
            else if (symbol.Name == "π")
            {
                Emit("Math.PI");
            }
            else
            {
                Emit(symbol.Name);
            }

            return symbol;
        }

        public void WriteString(string text)
        {
            writer.Write('"');
            Emit(text);
            writer.Write('"');
        }
        
        public void Indent(int level)
        {
            for (int i = 0; i < level; i++)
            {
                Emit("    ");
            }
        }

        public override IExpression VisitConstant(IExpression value)
        {
            switch (value.Kind)
            {
                case Kind.String:
                    WriteString(value.ToString());

                    break;

                default:
                    writer.Write(value.ToString());
                    break;
            }

            return value;
        }

        private static readonly Node graph = new Node();

        public void WriteTypeSymbol(Symbol symbol)
        {
            if (symbol == null) throw new ArgumentNullException(nameof(symbol));

            WriteTypeSymbol(graph.GetType(symbol));
        }

        public void WriteTypeSymbol(Type type)
        {
            // Determine if the domain is being used

            if (type.Arguments != null && type.Arguments.Length > 0)
            {
                var i = 0;

                if (type.Name == "Array")
                {
                    Emit("List");
                }
                else
                {
                    Emit(type.Name);
                }

                Emit('<');

                foreach (var arg in type.Arguments)
                {
                    if (i > 0) Emit(',');

                    WriteTypeSymbol(arg);

                    i++;
                }

                Emit('>');

                return;
            }

            switch (type.Name)
            {
                case "Object"     : Emit("object");     break;
                case "Decimal"    : Emit("decimal");    break;
                case "Int16"      : Emit("short");      break;
                case "Int32"      : Emit("int");        break;
                case "Int64"      : Emit("long");       break;
                case "Float"      : Emit("double");     break;
                case "Float32"    : Emit("float");      break;
                case "Float64"    : Emit("double");     break;
                case "String"     : Emit("string");     break;
                case "Number"     : Emit("double");     break;
                case "Percentage" : Emit("double");     break;
                default           : Emit(type.Name);    break;
            }
        }
              
    }
}