﻿namespace E.Compilation;

public partial class CSharpEmitter
{
    public void WriteImplementation(Type type)
    {
        Emit("public ", level);

        Emit(type.Flags.HasFlag(TypeFlags.Struct) ? "struct " : "class ");

        Emit(type.Name);

        var needsWhere = false;
        var ii = 0;

        if (type.GenericParameters is { Length: > 0 })
        {
            Emit('<');

            foreach (var generic in type.GenericParameters)
            {
                if (++ii != 1) Emit(", ");

                if (generic.Type.Id != (long)ObjectType.Object)
                {
                    needsWhere = true;
                }

                Emit(generic.Name);
            }

            Emit('>');
        }

        var a = 0;

        if (type.BaseType is not null)
        {
            Emit(" : ");

            Emit(type.BaseType.Name);

            a++;
        }

        foreach (var impl in type.Implementations)
        {
            if (impl.Protocol is not null)
            {
                Emit(a == 0 ? " : " : ",");

                Emit(impl.Protocol.Name.Name);

                a++;
            }
        }

        if (needsWhere)
        {
            Emit(" where ");

            ii = 0;

            foreach (Parameter generic in type.GenericParameters!)
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

        if (type.Properties is { Length: > 0 })
        {
            VisitConstructor(type.Name, type.Properties);

            EmitLine();
            EmitLine();

            i++;
        }

        foreach (var impl in type.Implementations)
        {
            foreach (var method in impl.Methods)
            {
                if (!method.IsInitializer) continue;

                WriteConstructor(method);

                EmitLine();
                EmitLine();
            }
        }

        if (type.Properties is { Length: > 0 })
        {
            WriteProperties(type.Properties);

            i++;
        }

        foreach (var impl in type.Implementations)
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

                if (impl.Protocol is not null)
                {
                    WriteProtocolFunction(impl.Protocol, method);
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