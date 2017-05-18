using System;

using Xunit;

namespace D.Inference
{
    public class TypeSystemTests
    {
        [Fact]
        public void D()
        {
            var flow = new Flow();

            var listOfString = flow.GetListTypeOf(Kind.String);
            var listOfFloat  = flow.GetListTypeOf(Kind.Number);

            Assert.Equal("String",  flow.Infer(new Call("head",     new[] { new Constant(listOfString) })).Name);
            Assert.Equal("Number",   flow.Infer(new Call("head",     new[] { new Constant(listOfFloat) })).Name);
            Assert.Equal("Boolean", flow.Infer(new Call("contains", new[] { new Constant(listOfFloat) })).Name);
        }

        [Fact]
        public void C()
        {
            var flow = new Flow();

            flow.AddVariable("a", Kind.Int64);
            flow.AddVariable("b", Kind.Number);
            flow.AddVariable("c", Kind.Number);
            flow.AddVariable("name", Kind.String);

            flow.AddFunction("+",
                new[] { new Parameter("lhs"), new Parameter("rhs"), }, new Variable("rhs"));

            flow.AddFunction("concat",
                new[] {
                    new Parameter("lhs", Kind.String),
                    new Parameter("rhs", Kind.String),
                }, Kind.String);

            flow.AddFunction("test",
                new[] {
                    new Parameter("lhs"),
                    new Parameter("rhs"),
             }, new Variable("rhs"));

            flow.AddFunction("test2",
                parameters: new[] { new Parameter("lhs"), new Parameter("rhs") }, 
                returns: new Call("+", new[] { new Variable("lhs"), new Variable("rhs") }
             ));

            flow.AddFunction("test3",
              new[] {
                new Parameter("a1"),
                new Parameter("b2"),
             }, new Call("concat", new[] { new Variable("a1"), new Variable("b2") }));


            Assert.Equal("Integer", flow.Infer(new Variable("a")).Name);
            Assert.Equal("String",  flow.Infer(new Variable("name")).Name);
            Assert.Equal("Integer", flow.Infer(new Call("+",       new[] { new Variable("a"),    new Variable("a") })).Name);
            Assert.Equal("String",  flow.Infer(new Call("concat",  new[] { new Variable("name"), new Variable("name") })).Name);
            Assert.Equal("String",  flow.Infer(new Call("test",    new[] { new Variable("name"), new Variable("name") })).Name);
            Assert.Equal("Float",   flow.Infer(new Call("test",    new[] { new Variable("name"), new Variable("b") })).Name);
            Assert.Equal("Float",   flow.Infer(new Call("test2",   new[] { new Variable("name"), new Variable("b") })).Name);

            Assert.Throws<InvalidOperationException>(() => {
                flow.Infer(new Call("test3", new[] { new Variable("name"), new Variable("b") }));
             });

            Assert.Equal("String", flow.Infer(new Call("test3", new[] { new Variable("name"), new Variable("name") })).Name);

            // Assert.Throws<ArgumentException>()
        }
    }
}
