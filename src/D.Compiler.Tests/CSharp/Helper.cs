using System.IO;
using System.Text;

namespace D.Compilation.Tests
{
    using Parsing;

    public static class Helper
    {
        public static string Transpile(string source, string moduleName = null)
        {
            var sb = new StringBuilder();

            var module = CompileModule(source, moduleName);

            using (var writer = new StringWriter(sb))
            {
                var csharp = new CSharpEmitter(writer);

                if (moduleName != null)
                {
                    csharp.WriteModule(module);
                }
                else
                {
                    csharp.WriteModuleMembers(module);
                }
            }

            return sb.ToString();
        }

        public static Module CompileModule(string source, string moduleName = null)
        {
            var compilier = new Compiler();

            using (var parser = new Parser(source))
            {
                return compilier.Compile(parser.Enumerate(), moduleName).Expressions[0] as Module;
            }
        }
    }
}