using Xunit;

namespace D.Operators.Tests
{
    using static OperatorType;

    public class OperatorTests
    {
        [Fact]
        public void BinarySearch()
        {
            var env = new Node();

            Assert.True(env.Operators.Maybe(Infix, '%', out _)); // %
            Assert.True(env.Operators.Maybe(Infix, '=', out _)); // =
            Assert.True(env.Operators.Maybe(Infix, '=', out _)); // ==
            Assert.True(env.Operators.Maybe(Infix, '=', out _)); // ===
            Assert.True(env.Operators.Maybe(Infix, '>', out _)); // >

            // >>>

            Assert.False(env.Operators.Maybe(Infix, 'm', out _));
        }

        [Fact]
        public void Registered()
        {
            var env = new Node();

            // Unary Operators
            Assert.Equal(Operator.UnaryPlus,          env.Operators[Prefix, "+"]);
            Assert.Equal(Operator.BitwiseNot,         env.Operators[Prefix, "~"]);
            Assert.Equal(Operator.Negation,           env.Operators[Prefix, "-"]);
            Assert.Equal(Operator.Not,                env.Operators[Prefix, "!"]);
                                                      
            // Binary Operators                       
            Assert.Equal(Operator.Multiply,           env.Operators[Infix, "*"]);
            Assert.Equal(Operator.Power,              env.Operators[Infix, "**"]);
            Assert.Equal(Operator.Divide,             env.Operators[Infix, "/"]);
            Assert.Equal(Operator.Add,                env.Operators[Infix, "+"]);
            Assert.Equal(Operator.Subtract,           env.Operators[Infix, "-"]);
            Assert.Equal(Operator.Remainder,          env.Operators[Infix, "%"]);
                                                      
            Assert.Equal(Operator.Is,                 env.Operators[Infix, "is"]);
            Assert.Equal(Operator.As,                 env.Operators[Infix, "as"]);
                                                      
            // Bitwise operators                      
            Assert.Equal(Operator.BitwiseAnd,         env.Operators[Infix, "&"]);
            Assert.Equal(Operator.LeftShift,          env.Operators[Infix, "<<"]);
            Assert.Equal(Operator.SignedRightShift,   env.Operators[Infix, ">>"]);
            Assert.Equal(Operator.UnsignedRightShift, env.Operators[Infix, ">>>"]);
        }

        public class Func : IFunction
        {
            public ObjectType Kind => ObjectType.Function;

            public string Name => "hi";

            public Parameter[] Parameters => null;

            public object Invoke(IArguments args) => "hello " + args[0].ToString() + " " + args[1].ToString();
        }

        [Fact]
        public void CustomOperator()
        {
            var env = new Node();

            var op = Operator.Infix(ObjectType.Function, "#", 0);

            env.Add("#", new Func());

            env.Operators.Add(op);

            var eval = new Evaluator(env);

            Assert.Equal("hello 5 2", eval.Evaluate("5 # 2"));
        }
    }
}
