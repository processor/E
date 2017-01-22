using System.Collections.Generic;

namespace D.Compiler
{
    public partial class CSharpTranspiler
    {        
        public void WriteImplementation(Type type, List<Implementation> implementations)
        {
            Indent(level);
            Emit($"public class {type.Name}");
            
            var where = false;
            var ii = 0;

            if (type.GenericParameters.Length > 0)
            {
                Emit("<");

                foreach (var generic in type.GenericParameters)
                {
                    if (++ii != 1) Emit(", ");

                    if (generic.Type.Name != "Any") where = true;

                    Emit(generic.Name);
                }

                Emit(">");
            }

            var a = 0;

            if (type.Constructor != null)
            {
                Emit(" : ");

                Emit(type.Constructor.Name);

                a++;
            }

            foreach (var impl in implementations)
            {
                if (impl.Protocal != null)
                {
                    Emit(a == 0 ? " : " : ",");

                    Emit(impl.Protocal.Name);

                    a++;
                }
            }

            if (where)
            {
                Emit(" where ");

                ii = 0;

                foreach (var generic in type.GenericParameters)
                {
                    if (++ii != 1) Emit(", ");

                    Emit(generic.Name);

                    Emit(" : ");

                    WriteTypeSymbol(generic.Type);
                }
            }

            
            EmitLine();
            EmitLine("{", level);

            level++;

            var i = 0;
            
            if (type.Properties.Length > 0)
            {
                VisitConstructor(type.Name, type.Properties);

                EmitLine();
                EmitLine();

                i++;
            }

            foreach (var impl in implementations)
            {
                foreach (var method in impl.Methods)
                {
                    if (!method.IsInitializer) continue;

                    WriteConstructor(method);

                    EmitLine();
                    EmitLine();
                }
            }

            if (type.Properties.Length > 0)
            {
                WriteProperties(type.Properties);

                i++;
            }

            foreach (var impl in implementations)
            {
                /*
                foreach (var method in impl.Variables)
                {
                    if (++i != 1)
                    {
                        EmitLine();
                        EmitLine();
                    }
                }
                */

                foreach (var method in impl.Methods)
                {
                    if (method.IsInitializer) continue;

                    if (++i != 1)
                    {
                        EmitLine();
                        EmitLine();
                    }

                    if (impl.Protocal != null)
                    {
                        WriteProtocalFunction(impl.Protocal, method);
                    }
                    else
                    {
                        VisitFunction(method);
                    }
                }
            }

            level--;

            EmitLine();

            Emit("}", level);
        }
    }
}
