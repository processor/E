using System.Linq;

using D.Expressions;
using D.Imaging;
using D.Mathematics;
using D.Parsing;
using D.Syntax;
using D.Units;

using Xunit;

using static D.Units.UnitValue;

namespace D.Tests
{
    public class EvaulatorTests
    {
        private static readonly Node env = new Node(
            new ArithmeticModule(), 
            new TrigonometryModule(),
            new ColorModule()
        );

        [Fact]
        public void ConstructColor()
        {
            var rgb = (Rgb)Script.Evaluate("rgb(20%, 12%, 19%)", env);

            Assert.Equal(0.20, rgb.R);
            Assert.Equal(0.12, rgb.G);
            Assert.Equal(0.19, rgb.B);
        }

        [Fact]
        public void MultiAssignment()
        {
            var evaulator = new Evaluator();

            var parser = new Parser(
@"a = 1
  b = 2

  b");

            foreach (var statement in parser.Enumerate())
            {
                evaulator.Evaluate(statement);
            }

            Assert.Equal("1", evaulator.Scope.Get("a").ToString());
            Assert.Equal("2", evaulator.Scope.Get("b").ToString());

            Assert.Equal("2", evaulator.Scope.This.ToString());
        }

        [Fact]
        public void ManualProduct()
        {
            var evaulator = new Evaluator(env);

            var node = new BinaryExpression(Operator.Multiply, Px(10), Percent(50));

            var result = (UnitValue<double>)evaulator.Evaluate(node);

            Assert.Equal((5, CssUnits.Px), (result.Value, result.Unit));
        }

        [Fact]
        public void ManualProductWithCustomDimensionlessUnit()
        {
            var forth = new UnitInfo("forth", Dimension.None, 1, new Number(0.25));

            var evaulator = new Evaluator(env);

            var node = Expression.Multiply(Px(10), UnitValue.Create(1, forth));

            var result = (UnitValue<double>)evaulator.Evaluate(node);

            Assert.Equal((2.5, CssUnits.Px), (result.Value, result.Unit));
        }
        

        [Fact]
        public void ManualSum()
        {
            var evaulator = new Evaluator(env);

            var node = new BinaryExpression(Operator.Add, Px(10), new BinaryExpression(Operator.Add, Px(1), Px(1)));

            var result = (UnitValue<double>)evaulator.Evaluate(node);

            Assert.Equal(12,             result.Value);
            Assert.Equal(CssUnits.Px, result.Unit);
        }

        [Fact]
        public void Add()
        {
            Assert.Equal("1", Script.Evaluate("0 + 1", env).ToString());
            Assert.Equal("2", Script.Evaluate("1 + 1", env).ToString());
        }

        [Fact]
        public void CssUnitValues()
        {
            Assert.Equal("40px",     Script.Evaluate("20px + 20px",                  env).ToString());
            Assert.Equal("40px",     Script.Evaluate("20px * 2",                     env).ToString());
            Assert.Equal("40px",     Script.Evaluate("80px / 2",                     env).ToString());
            Assert.Equal("40000px²", Script.Evaluate("((80px / 2) ** 2) * 50 * 50%", env).ToString());
            Assert.Equal("10px",     Script.Evaluate("20px * 50%",                   env).ToString());
            Assert.Equal("20px",     Script.Evaluate("20px * 50% * 2",               env).ToString());
        }

        [Fact]
        public void Subtract()
        {
            Assert.Equal("-1",  Script.Evaluate("0 - 1", env).ToString());
            Assert.Equal("0",   Script.Evaluate("1 - 1", env).ToString());
            Assert.Equal("-1",  Script.Evaluate("1 - 2", env).ToString());
        }
        
        [Fact]
        public void Pipe()
        {
            var evaulator = new Evaluator();

            var parser = new Parser(
@"a = 11
  a |> add 50
  |> multiply 10

");

            evaulator.Evaluate(parser.Next());

            Assert.Equal("11", evaulator.Scope.Get("a").ToString());

            var pipe = (CallExpressionSyntax)parser.Next(); // left: (a |> add 50) |> multiply 10
            var left = (CallExpressionSyntax)pipe.Callee;   // a |> add 50

            Assert.Equal("a", left.Callee.ToString());

            Assert.Equal(SyntaxKind.Symbol, left.Callee.Kind);         
        }

        [Fact]
        public void Assignment()
        {
            var evaulator = new Evaluator();

            var parser = new Parser(@"a = 1");

            foreach (var statement in parser.Enumerate())
            {
                evaulator.Evaluate(statement);
            }

            Assert.Equal("1", evaulator.Scope.Get("a").ToString());
        }

        public object Eval(IExpression statement)
            => new Evaluator().Evaluate(statement);

      
        /*
        [Fact]
        public void Eval2()
        {
            var result = new Evaulator(env).Evaluate("1kg * 1lb");

            Assert.Equal("0.453592kg²", result.ToString());
        }
        */

        [Theory]
        [InlineData("2kg * 3",                 "6kg")]
        [InlineData("100mg * 3",               "300mg")]
        [InlineData("100mg / 10",              "10mg")]
        [InlineData("1kg + 1000g",             "2kg")]
        [InlineData("1kg + 100g",              "1.1kg")]
        [InlineData("1kg + 1.3g",              "1.0013kg")]
        [InlineData("1kg * 1kg",               "1kg²")]
        [InlineData("1g * 1g * 2g",            "2g³")]
        [InlineData("1kg * 1kg * 3kg * 4kg",   "12kg⁴")]
        [InlineData("30s * 30s",               "900s²")]
        [InlineData("(2kg * 3)",               "6kg")]
        [InlineData("30s ** 3",                "27000s³")]
        [InlineData("30s³ ** 2",               "900s⁴")]
        [InlineData("2 ** 32",                  "4294967296")]
        [InlineData("5s + 10s",                 "15s")]
        [InlineData("(1 * 5)",                  "5")]
        [InlineData("(1 * 5) + 3",              "8")]
        [InlineData("(1 + 5) * 3",              "18")]
        [InlineData("3 * (5 + 5)",              "30")]
        [InlineData("3 * (2 ** 3)",             "24")]
        [InlineData("2 + 3     + (10)",         "15")]
        [InlineData("(2 + 3)   + (10)",         "15")]
        [InlineData("(50 - 45) + (10 * 1 * 2)", "25")]
        [InlineData("(2h / 2)", "1h")]
        [InlineData("10s + 1min", "70s")]
        [InlineData("10s + 2min", "130s")]
        public void EvalScripts(string text, string r)
        {
            Assert.Equal(r, Script.Evaluate(text, env).ToString());
        }

        [Fact]
        public void C()
        {
            Assert.Equal("6kg", Script.Evaluate("2kg * 3", env).ToString());
        }
        // (a/b/(c* a))*(c* d/a)/d)

        [Theory]
        [InlineData("5 * b", new[] { "b" })]
        [InlineData("5 * q", new[] { "q" })]
        // [InlineData("5 * x * y", new[] { "x", "y" })]
        // [InlineData("5 * x * sin(y) * 5", new[] { "x", "y" })]
        // [InlineData("5 * x * y * sin(z)", new[] { "x", "y", "z" })]
        public void Functions(string text, string[] argNames)
        {
            var ƒ = (FunctionExpression)Script.Evaluate(text, env);

            Assert.Equal(argNames, ƒ.Parameters.Select(p => p.Name).ToArray());
        }

        /*
        [Fact]
        public void Equations()
        {
            var equation = (Equation)Script.Evaluate("y = 5 * x", env);

            Assert.Equal(2, equation.Symbols.Count);

            Assert.Equal("y", equation.Symbols[0]);
            Assert.Equal("x", equation.Symbols[1]);

            Assert.Equal("y", equation.Left.ToString());
            Assert.Equal("5 * x", equation.Right.ToString()); // ∙
        }
        */
        
        [Fact]
        public void FunctionCall()
        {
            Assert.Equal(-0.54402111088937d, (Number)Script.Evaluate("sin(10)", env), precision: 10);

            Assert.Equal("2", Script.Evaluate("sqrt(4)", env).ToString());
        }

        [Theory]
        [InlineData("5 * x", "ƒ(x,y) = 5 * x")]
        [InlineData("5 * x * sin(y) / z", "ƒ(x,y,z) = 5 * x * sin(y) / z")]

        public void Formatting(string a, string b)
        {
            // 5 * x * y

            var statement = new Parser(a).Next();
            
            // var ƒ = Eval(statement);

            // Assert.Equal(b, b.ToString());
        }

        [Fact]
        public void Eval3()
        {
            var parser = new Parser(@"1kg * 1lb * 4kg", env);

            var statement = (BinaryExpressionSyntax)parser.Next();

            var l = (UnitValueSyntax)statement.Left;
            var r = (BinaryExpressionSyntax)statement.Right;

            Assert.Equal("1 kg", l.ToString());
            Assert.Equal("1 lb", r.Left.ToString());
            Assert.Equal("4 kg", r.Right.ToString());

            /*
            var result = (Literal)new Evaulator().Evaluate(statement);

            Assert.Equal("1.814368kg", result.Value.ToString());
            */
        }
    }
}
