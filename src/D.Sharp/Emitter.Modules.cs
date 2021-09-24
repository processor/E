using System;

using E.Expressions;

namespace E.Compilation;

public partial class CSharpEmitter
{
    public void WriteModule(Module module)
    {
        EmitLine("namespace " + module.Name, level);
        EmitLine("{", level);

        level++;

        WriteModuleMembers(module);

        level--;

        EmitLine();

        Emit("}", level);
    }

    public void WriteModuleMembers(Module module)
    {
        int i = 0;

        foreach (var statement in module.Statements)
        {
            if (statement is ImplementationExpression) continue;

            if (++i > 1)
            {
                EmitLine();
                EmitLine();
            }

            switch (statement)
            {
                case ProtocolExpression protocol: 
                    VisitProtocal(protocol); 
                    break;
                case E.Type type: 
                    WriteImplementation(type);
                    break;
                case FunctionExpression func:
                    VisitFunction(func);
                    break;
                case Module mod:
                    WriteModule(mod);

                    break;

                default: throw new NotImplementedException(statement.GetType().Name);
            }
        }
    }
}
