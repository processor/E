/*
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace D.Roslyn.Compiler
{
    public class RosylnCompiler
    {
        public void X()
        {
            var root = CSharpSyntaxTree.ParseText(@"
using System;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
            Console.WriteLine(Add(1, 2).ToString());
            Console.ReadLine();
        }

        public static int Add(int a, int b) { return a + b; }
    }
}");



            Compile("hi.exe", root);
        }

        public void Y()
        {
            var root = CSharpSyntaxTree.ParseText(@"
using System;

namespace D
{
    public static class Math
    {
        public static int Add(int a, int b) 
        { 
            return a + b;
        }

        public static long Add(long a, long b) 
        { 
            return a + b;
        }
    }
}");



            Compile("math.dll", root);
        }


        public CSharpCompilation Compile(string name, SyntaxTree root)
        {
            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

            var mscorelib = MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "mscorlib.dll"));
            // var system    = MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.dll"));
            //  var corelib   = MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Core.dll"));
            var runtime = MetadataReference.CreateFromFile(typeof(string).Assembly.Location);
            var console = MetadataReference.CreateFromFile(typeof(System.Console).Assembly.Location);


            // var extensions = MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.Extensions.dll"));

            var references = new[] { mscorelib, runtime, console };

            var options = new CSharpCompilationOptions(name.EndsWith(".exe") 
                ? OutputKind.ConsoleApplication
                : OutputKind.DynamicallyLinkedLibrary
            );
            // , deterministic: true

            var compilation = CSharpCompilation.Create(name,
                new[] { root }).WithOptions(options).AddReferences(references);

            var result = compilation.Emit(
                "Z:/workspace/compliations/" + name);

                // "Z:/workspace/compliations/" + name.Replace(".exe", ".pdb").Replace(".dll", ".pdb"));

            
            var errors = new List<string>();

            foreach (Diagnostic diagnostic in result.Diagnostics)
            {
                errors.Add(string.Format("{0}: {1}", diagnostic.Id, diagnostic.GetMessage()));
            }

            if (!result.Success)
            {
                // throw new System.Exception(JsonArray.FromObject(errors).ToString());
            }

            return compilation;
        }


        public void GetIL(string text, string methodName)
        {
           // TODO
        }
    }
}

*/