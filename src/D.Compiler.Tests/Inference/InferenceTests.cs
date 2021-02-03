using System.Collections.Generic;
using System.Linq;

using E.Expressions;
using E.Parsing;
using E.Syntax;

using Xunit;

namespace E.Compilation.Inference.Tests
{
    public class InferenceTests
    {
        [Fact]
        public void Mutiple()
        {
            var script = "b1 function(t: Number) => t * t * t;";

            var node = ParseFunction(script);

            Assert.Equal("b1", node.Name);
            Assert.Equal("t", node.Parameters[0].Name);
            Assert.Equal("Number", node.Parameters[0].Type.Name);

            var block           = node.Body as BlockExpression;
            var returnStatement = block.Statements[0] as ReturnStatement;
            var binary          = returnStatement.Expression as BinaryExpression;
            
            Assert.Equal("Number", node.ReturnType.Name);
        }

        [Fact]
        public void Mutiple2()
        {
            var script = "b1 function(t: Number, x: i32) => x * t * t;";

            var node = ParseFunction(script);

            Assert.Equal("b1",     node.Name);
            Assert.Equal("t",      node.Parameters[0].Name);
            Assert.Equal("Number", node.Parameters[0].Type.Name);

            Assert.Equal("x",     node.Parameters[1].Name);
            Assert.Equal("Int32", node.Parameters[1].Type.Name);

            var block           = (BlockExpression)node.Body;
            var returnStatement = (ReturnStatement)block.Statements[0];
            var binary          = (BinaryExpression)returnStatement.Expression;

            Assert.Equal("Object", node.ReturnType.Name);
        }

        [Fact]
        public void ParameterFlow()
        {
            var script = "same function(a: Number) { return a }";

            var node = ParseFunction(script);

            Assert.Equal("same",   node.Name);
            Assert.Equal("a",      node.Parameters[0].Name);
            Assert.Equal("Number", node.Parameters[0].Type.Name);

            Assert.Equal("Number", node.ReturnType.Name);
        }

        [Fact]
        public void ParameterFlow2()
        {
            var script = "same function(a: Number, b: Number) => a == b";

            var node = ParseFunction(script);

            Assert.Equal("same", node.Name);
            Assert.Equal("a", node.Parameters[0].Name);
            Assert.Equal("Number", node.Parameters[0].Type.Name);

            Assert.Equal("b", node.Parameters[1].Name);
            Assert.Equal("Number", node.Parameters[1].Type.Name);

            var body = (BlockExpression)node.Body;
            var returnStatement = (ReturnStatement)body[0];

            var b = (BinaryExpression)returnStatement.Expression;

            Assert.Equal(ObjectType.EqualsExpression, b.Operator.OpKind);
            Assert.True(b.Operator.IsComparision);

            Assert.Equal("Boolean", node.ReturnType.Name);
        }

        [Fact]
        public void InlineVariableFlow()
        {
            var script = "one function() { var one = 1; return one; }";

            var node = ParseFunction(script);

            Assert.Equal("one", node.Name);
            Assert.Equal("Int64", node.ReturnType.Name);
        }

        [Fact]
        public void InlineVariableFlow2()
        {
            var script = "one ƒ() { var one: Float32 = 1; let x = one; let y = x; return y; }";

            var node = ParseFunction(script);

            Assert.Equal("Float32", node.ReturnType.Name);
        }

        [Fact]
        public void InlineVariable_Alias()
        {
            var script = "one ƒ() { var one: f32 = 1; let x = one; let y = x; return y; }";

            var node = ParseFunction(script);

            Assert.Equal("Float32", node.ReturnType.Name);
        }

        [Fact]
        public void InlineVariableFlow3()
        {
            var script = "one ƒ() { var one: f64 = 1; let x = one; let y = x; return y * y; }";

            var node = ParseFunction(script);

            Assert.Equal("Float64", node.ReturnType.Name);
        }

        [Fact]
        public void LambdaLiteralFlow()
        {
            Assert.Equal("String",        ParseFunction("a ƒ() => \"a\"").ReturnType.Name);
            Assert.Equal("Int64",         ParseFunction("a ƒ() => 1").ReturnType.Name);
            Assert.Equal("Array<Int64>",  ParseFunction("a ƒ() => [ 1, 2, 3 ]").ReturnType.ToString());
            Assert.Equal("Array<String>", ParseFunction(@"a ƒ() => [ ""a"", ""b"", ""c"" ]").ReturnType.ToString());
            Assert.Equal("Array<Object>", ParseFunction(@"a ƒ() => [ ""a"", 1 ]").ReturnType.ToString());

            // Assert.Equal("Array<Tuple<>>",  ParseFunction("a ƒ() => [ (1, 2), (2, 3), (4, 5) ]").ReturnType.ToString());

        }

        public static FunctionExpression ParseFunction(string text)
        {
           var compiler = new Compiler();

           return compiler.VisitFunctionDeclaration(
               syntax: (FunctionDeclarationSyntax)Parse(text).First()
           );
        }

        public static IEnumerable<ISyntaxNode> Parse(string source)
        {
            using var parser = new Parser(source);

            foreach (var node in parser.Enumerate())
            {
                yield return node;

            }
        }
    }
}