using D.Collections;
using Xunit;

namespace D.Operators.Tests
{
    using Expressions;

    using static OperatorType;

    public class OperatorTests
    {
        [Fact]
        public void BinarySearch()
        {
            var env = new Node();

            Trie<Operator>.Node node;

            Assert.True(env.Operators.Maybe(Infix, '%', out node)); // %
            Assert.True(env.Operators.Maybe(Infix, '=', out node)); // =
            Assert.True(env.Operators.Maybe(Infix, '=', out node)); // ==
            Assert.True(env.Operators.Maybe(Infix, '=', out node)); // ===
            Assert.True(env.Operators.Maybe(Infix, '>', out node)); // >

            // >>>

            Assert.False(env.Operators.Maybe(Infix, 'm', out node));
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
            Assert.Equal(Operator.Multiplication,     env.Operators[Infix, "*"]);
            Assert.Equal(Operator.Power,              env.Operators[Infix, "**"]);
            Assert.Equal(Operator.Division,           env.Operators[Infix, "/"]);
            Assert.Equal(Operator.Addition,           env.Operators[Infix, "+"]);
            Assert.Equal(Operator.Subtraction,        env.Operators[Infix, "-"]);
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
            public Kind Kind => Kind.FunctionExpression;

            public string Name => "hi";

            public Parameter[] Parameters => null;

            public IObject Invoke(IArguments args)
                => new StringLiteral("hello");
        }

        [Fact]
        public void CustomOperator()
        {
            var env = new Node();

            var op = Operator.Infix(Kind.FunctionExpression, "#", 0);

            env.Add("#", new Func());

            env.Operators.Add(op);

            var eval = new Evaulator(env);

            Assert.Equal("hello", eval.Evaluate("5 # 2").ToString());
        }
    }
}
