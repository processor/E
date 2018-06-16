using Xunit;

namespace D.Inference.Tests
{

    public class NodeTests
    {
        [Fact]
        public void LetTest()
        {
            var flow = new Flow();

            var boolean = flow.GetType(Kind.Boolean);
            var i32 = flow.GetType(Kind.Int32);
            var i64 = flow.GetType(Kind.Int64);

            // var b = Node.Var("b");

            // var b = flow.AddVariable("b", Kind.Int32);

            var b = flow.AddVariable("b", Kind.Int64);

            var letNode = Node.Let(new[] {
                Node.Define(Node.Var("a", "Boolean"), Node.Const(boolean)),
                Node.Define(b, Node.Const(i64))

            }, b);

            Assert.Equal("Int64", flow.Infer(letNode).ToString());
            
            Assert.Equal("(a = { Boolean }; b = { Int64 }) b", letNode.ToString());
        }

        [Fact]
        public void AbstractTest()
        {
            var flow = new Flow();

            var binary = TypeSystem.NewGeneric();
            var boolean = flow.GetType(Kind.Boolean);

            var a = Node.Define(Node.Var("gt"), Node.Abstract(new[] {
                Node.Var("lhs", binary),
                Node.Var("rhs", binary)
            }, boolean, Node.Const(boolean)));


            Assert.Equal("gt = (lhs, rhs) { Boolean } -> Boolean", a.ToString());
        }

        [Fact]
        public void AddTest()
        {
            var flow = new Flow();

            var binary = TypeSystem.NewGeneric();
            var i32 = flow.GetType(Kind.Int32);
            var i64 = flow.GetType(Kind.Int64);

            var apply = Node.Apply(Node.Var("+"), new[] { Node.Const(i32), Node.Const(i64) });
       
            Assert.Equal("+ ({ Int32 }, { Int64 })", apply.ToString());

            Assert.Equal("Int32", flow.Infer(apply).ToString());
        }
    }
}
