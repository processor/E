using System.IO;

using E.Parsing;

namespace E.Compilation.Tests;

public static class Helper
{
    public static string Transpile(string source, string moduleName = null)
    {
        var module = CompileModule(source, moduleName);

        using var writer = new StringWriter();

        var csharp = new CSharpEmitter(writer);

        if (moduleName is not null)
        {
            csharp.WriteModule(module);
        }
        else
        {
            csharp.WriteModuleMembers(module);
        }

        return writer.ToString();
    }

    public static Module CompileModule(string source, string moduleName = null)
    {
        var compilier = new Compiler();

        using var parser = new Parser(source);

        return compilier.Compile(parser.Enumerate(), moduleName).Expressions[0] as Module;
    }
}
