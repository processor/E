using Xunit;

namespace E.Inference
{
    using static Node;

    public class FlowTests
    {
        [Fact]
        public void Q()
        {
            var flow = new Flow();

            Assert.Equal(flow.GetType(ObjectType.Number).Name,  Type.Get(ObjectType.Number).Name);
            Assert.Equal(flow.GetType(ObjectType.Boolean).Name, Type.Get(ObjectType.Boolean).Name);

            Assert.Same(flow.GetType(ObjectType.Boolean), flow.GetType(ObjectType.Boolean));
            Assert.Same(flow.GetType(ObjectType.Int64),   flow.GetType(ObjectType.Int64));
        }

        [Fact]
        public void Var()
        {
            var flow = new Flow();

            var a = new VariableNode("a", null);

            flow.Assign(a, Type.Get(ObjectType.Number));
            
            Assert.Equal("Number", flow.Infer(a).Name);

            flow.Assign(a, Type.Get(ObjectType.Boolean));

            Assert.Equal("Boolean", flow.Infer(a).Name);
        }

        [Fact]
        public void A()
        {
            var flow = new Flow();

            var bv = flow.Define("bv", Type.Get(ObjectType.Boolean));

            var r = flow.Infer(Apply(Variable("!"), new[] {
                bv
            }));

            Assert.Equal("Boolean", r.Name);
        }

        static VariableNode Variable(string name, IType type = null)
        {
            return new VariableNode(name, type);
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

            var listOfString = flow.GetListTypeOf(ObjectType.String);
            var listOfFloat = flow.GetListTypeOf(ObjectType.Number);

            Assert.Equal("String",   flow.Infer(Apply(Variable("head"),     new[] { new ConstantNode(listOfString) })).Name);
            Assert.Equal("Number",  flow.Infer(Apply(Variable("head"),      new[] { new ConstantNode(listOfFloat) })).Name);
            Assert.Equal("Boolean", flow.Infer(Apply(Variable("contains"),  new[] { new ConstantNode(listOfFloat) })).Name);
        }

        [Fact]
        public void E()
        {
            var system = new Flow();

            system.Define("x",  Type.Get(ObjectType.Number));
            system.Define("y",  Type.Get(ObjectType.Number));
            system.Define("z",  Type.Get(ObjectType.Int32));
            system.Define("x1", Type.Get(ObjectType.Float32));

            system.Define("name", Type.Get(ObjectType.String));

            Assert.Equal("Number", system.Infer(Variable("x")).Name.ToString());
            Assert.Equal("Object", system.Infer(Variable("x")).BaseType.Name.ToString());

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

            flow.Define("a", Type.Get(ObjectType.Int64));
            flow.Define("b", Type.Get(ObjectType.Number));
            flow.Define("c", Type.Get(ObjectType.Number));
            flow.Define("name", Type.Get(ObjectType.String));

            var any = flow.NewGeneric();

            flow.Infer(new DefineNode(Variable("+"), Abstract(new[] {
                Variable("lhs", any),
                Variable("rhs", any)
            }, Variable("lhs"), any)));

            flow.AddFunction("concat", new[] {
                new Parameter("lhs", ObjectType.String),
                new Parameter("rhs", ObjectType.String),
            }, ObjectType.String);

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
