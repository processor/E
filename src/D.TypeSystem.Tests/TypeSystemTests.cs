using System;
using Carbon.Json;
using Xunit;

namespace D.Inference
{
    using static Node;

    public class TypeSystemTests
    {
        
        [Fact]
        public void D()
        {
            var flow = new Flow();

            var listOfString = flow.GetListTypeOf(Kind.String);
            var listOfFloat  = flow.GetListTypeOf(Kind.Number);

            Assert.Equal("String",   flow.Infer(Apply(Var("head"),     new[] { Const(listOfString) })).Id);
            Assert.Equal("Number",   flow.Infer(Apply(Var("head"),     new[] { Const(listOfFloat) })).Id);
            Assert.Equal("Boolean",  flow.Infer(Apply(Var("contains"), new[] { Const(listOfFloat) })).Id);
        }        

        [Fact]
        public void E()
        {
            var system = new Flow();

            system.AddVariable("x", Kind.Number);
            system.AddVariable("y", Kind.Number);
            system.AddVariable("z", Kind.Int32);
            system.AddVariable("x1", Kind.Float32);

            system.AddVariable("name", Kind.String);

            /*
            system.AddFunction("+", new[] {
                new Parameter("lhs", Kind.Object),
                new Parameter("rhs", Kind.Object),
            }, Var("rhs"));
            */

            var generic1 = system.NewGeneric();

            system.Infer(Define(Var("+"), Abstract(new[] {
                Var("lhs", generic1),
                Var("rhs", generic1)
            }, generic1, Var("lhs"))));


            system.Infer(Define(Var("/"), Abstract(new[] {
                Var("lhs", system.GetType(Kind.Float32)),
                Var("rhs", system.GetType(Kind.Float32))
            }, system.GetType(Kind.Float32), Const(system.GetType(Kind.Float32)))));

            Assert.Equal("Number", system.Infer(Var("x")).Name.ToString());
            Assert.Equal("Object", system.Infer(Var("x")).Constructor.Name.ToString());

            Assert.Equal("Number", system.Infer(Apply(Var("+"), new[] {
                Var("x"),
                Var("y")
            })).Id);

            Assert.Equal("Int32", system.Infer(Apply(Var("+"), new[] {
                Var("z"),
                Var("z")
            })).Id);

       
            Assert.Equal("Float32", system.Infer(Apply(Var("/"), new[] {
                Var("x"),
                Var("y")
            })).Id);
        }

     
        [Fact]
        public void C()
        {
            var flow = new Flow();

            flow.AddVariable("a",    Kind.Int64);
            flow.AddVariable("b",    Kind.Number);
            flow.AddVariable("c",    Kind.Number);
            flow.AddVariable("name", Kind.String);

            var generic1 = flow.NewGeneric();

            flow.Infer(Define(Var("+"), Abstract(new[] {
                Var("lhs", generic1),
                Var("rhs", generic1)
            }, generic1, Var("lhs"))));

            flow.AddFunction("concat", new[] {
                new Parameter("lhs", Kind.String),
                new Parameter("rhs", Kind.String),
            }, Kind.String);

            var a = flow.NewGeneric();
            var b = flow.NewGeneric();

            flow.AddFunction("test2",
                args: new[] {
                    Var("lhs", a),
                    Var("rhs", b)
                }, 
                body : Apply(Var("+"), new[] { Var("lhs"), Var("rhs") }
             ));

        

            Assert.Equal("Int64",   flow.Infer(Var("a")).Id);
            Assert.Equal("String",  flow.Infer(Var("name")).Id);
            Assert.Equal("Int64",   flow.Infer(Apply(Var("+"),       new[] { Var("a"),    Var("a") })).Id);

            Assert.Equal("String", flow.Infer(Apply(Var("concat"), new[] { Var("name"), Var("name") })).Id);

            Assert.Equal("Int64",  flow.Infer(Apply(Var("test2"),  new[] { Var("a"), Var("a") })).Id);
            Assert.Equal("String", flow.Infer(Apply(Var("test2"), new[] { Var("name"), Var("name") })).Id);



            // Assert.Equal("String", flow.Infer(Apply(Var("test3"), new[] { Var("name"), Var("name") })).Id);

            // Assert.Throws<ArgumentException>()
        }

    }
}
