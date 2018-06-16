using Xunit;

namespace D.Inference
{
    using static Node;

    public class FlowTests
    {
        [Fact]
        public void Var()
        {
            var flow = new Flow();

            var number = flow.GetType(Kind.Number);
            var boolean = flow.GetType(Kind.Boolean);

            var a = new VariableNode("a", null);

            flow.Assign(a, Kind.Number);
            
            Assert.Equal("Number", flow.Infer(a).Name);

            flow.Assign(a, Kind.Boolean);

            Assert.Equal("Boolean", flow.Infer(a).Name);
        }

        [Fact]
        public void A()
        {
            var flow = new Flow();

            Assert.Equal("Boolean", flow.Infer(Apply(Variable("!"), new[] {
                Variable("Boolean")
            })).Name);
        }

        [Fact]
        public void B()
        {
            var flow = new Flow();

            var a = Apply(Variable("!"), new[] {
                Variable("Boolean")
            });

            var b = Apply(Variable("+"), new[] {
                a, a
            });

            var c = Apply(Variable("*"), new[] {
                b, a
            });

            Assert.Equal("Boolean", flow.Infer(c).Name);
        }

        [Fact]
        public void D()
        {
            var flow = new Flow();

            var listOfString = flow.GetListTypeOf(Kind.String);
            var listOfFloat = flow.GetListTypeOf(Kind.Number);

            Assert.Equal("String", flow.Infer(Apply(Variable("head"), new[] { Constant(listOfString) })).Name);
            Assert.Equal("Number", flow.Infer(Apply(Variable("head"), new[] { Constant(listOfFloat) })).Name);
            Assert.Equal("Boolean", flow.Infer(Apply(Variable("contains"), new[] { Constant(listOfFloat) })).Name);
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

            Assert.Equal("Number", system.Infer(Variable("x")).Name.ToString());
            Assert.Equal("Object", system.Infer(Variable("x")).Constructor.Name.ToString());

            Assert.Equal("Number", system.Infer(Apply(Variable("+"), new[] {
                Variable("x"),
                Variable("y")
            })).Name);

            Assert.Equal("Int32", system.Infer(Apply(Variable("+"), new[] {
                Variable("z"),
                Variable("z")
            })).Name);

            Assert.Equal("Number", system.Infer(Apply(Variable("/"), new[] {
                Variable("x"),
                Variable("y")
            })).Name);
        }

        [Fact]
        public void C()
        {
            var flow = new Flow();

            flow.AddVariable("a", Kind.Int64);
            flow.AddVariable("b", Kind.Number);
            flow.AddVariable("c", Kind.Number);
            flow.AddVariable("name", Kind.String);

            var any = flow.NewGeneric();

            flow.Infer(Define(Variable("+"), Abstract(new[] {
                Variable("lhs", any),
                Variable("rhs", any)
            }, any, Variable("lhs"))));

            flow.AddFunction("concat", new[] {
                new Parameter("lhs", Kind.String),
                new Parameter("rhs", Kind.String),
            }, Kind.String);

            var a = flow.NewGeneric();
            var b = flow.NewGeneric();

            flow.AddFunction("test2",
                args: new[] {
                    Variable("lhs", a),
                    Variable("rhs", b)
                },
                body: Apply(Variable("+"), new[] { Variable("lhs"), Variable("rhs") }
             ));

            Assert.Equal("Int64", flow.Infer(Variable("a")).Name);
            Assert.Equal("String", flow.Infer(Variable("name")).Name);
            Assert.Equal("Int64", flow.Infer(Apply(Variable("+"), new[] { Variable("a"), Variable("a") })).Name);

            Assert.Equal("String", flow.Infer(Apply(Variable("concat"), new[] { Variable("name"), Variable("name") })).Name);

            Assert.Equal("Int64", flow.Infer(Apply(Variable("test2"), new[] { Variable("a"), Variable("a") })).Name);
            Assert.Equal("String", flow.Infer(Apply(Variable("test2"), new[] { Variable("name"), Variable("name") })).Name);
        }

    }
}
