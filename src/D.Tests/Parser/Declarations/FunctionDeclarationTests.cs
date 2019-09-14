using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class FunctionDeclarationTests : TestBase
    {
        private static readonly Compiler compiler = new Compiler();

        [Fact]
        public void IndexAssignment()
        {
            var func = Parse<FunctionDeclarationSyntax>(@"
inverse ƒ -> Matrix<T> {
    this[0] = t11 * detInv
    this[1] = (n24 * n33 * n41 - n23 * n34 * n41 - n24 * n31 * n43 + n21 * n34 * n43 + n23 * n31 * n44 - n21 * n33 * n44) * detInv
  }
");

            var body = (BlockSyntax)func.Body;

            var assignment = (BinaryExpressionSyntax)body.Statements[0];

            var left = (IndexAccessExpressionSyntax)assignment.Left;
            var right = (BinaryExpressionSyntax)assignment.Right;

            Assert.Equal("this", (Symbol)left.Left);
            Assert.Equal(0, (NumberLiteralSyntax)left.Arguments[0].Value);
        }

        [Fact]
        public void Elements()
        {
            var func = Parse<FunctionDeclarationSyntax>(@"
fromTranslation ƒ <T: Number>(x: T, y: T, z: T) => Matrix4<T>(
  elements: [
    1, 0, 0, x,
    0, 1, 0, y,
    0, 0, 1, z,
    0, 0, 0, 1
  ]
)");

            // var f = compiler.VisitFunction(func);

            Assert.Equal("fromTranslation", func.Name);
            Assert.Equal("T", func.GenericParameters[0].Name);
            Assert.Equal("Number", func.GenericParameters[0].Type);

            Assert.Equal(3, func.Parameters.Length);
            
            // Assert.Equal("Matrix4", func.ReturnType.Name);

            var elements = ((ObjectInitializerSyntax)((LambdaExpressionSyntax)func.Body).Expression).Arguments[0];
            var array    = (ArrayInitializerSyntax)elements.Value;

            Assert.Equal(16  , array.Elements.Length);
            Assert.Equal(1   , (NumberLiteralSyntax)array.Elements[0]);
            Assert.Equal("x" , (Symbol)array.Elements[3]);
        }

        [Fact]
        public void Constructor()
        {
            var func = Parse<FunctionDeclarationSyntax>("Point ƒ <T: Number>(x: T, y: T, z: T) => Point<T>(x, y, z)");

            Assert.Equal("Point", func.Name);

            Assert.Equal("T"      , func.GenericParameters[0].Name);
            Assert.Equal("Number" , func.GenericParameters[0].Type);

            Assert.True(func.IsInitializer);

            Assert.Equal("x", func.Parameters[0].Name);
            Assert.Equal("y", func.Parameters[1].Name);
            Assert.Equal("z", func.Parameters[2].Name);
                              
            Assert.Equal("T", func.Parameters[0].Type);
            Assert.Equal("T", func.Parameters[1].Type);
            Assert.Equal("T", func.Parameters[2].Type);

            var lambda    = (LambdaExpressionSyntax)func.Body;
            var newObject = (ObjectInitializerSyntax)lambda.Expression;

            Assert.Equal("Point", newObject.Type.Name);
            Assert.Equal("T",     newObject.Type.Arguments[0].Name);

            foreach (var member in newObject.Arguments)
            {
                Assert.True(member.Value != null);
            }
        }

        [Fact]
        public void Q()
        {
            var func = Parse<FunctionDeclarationSyntax>(@"
clamp ƒ <T> (p: Point<T>, min: Point<T>, max: Point<T>) => Point<T> {
  x: max(min.x, min(max.x, p.x)),
  y: max(min.y, min(max.y, p.y)),
  z: max(min.z, min(max.z, p.z))
}");
            Assert.Equal("clamp", func.Name);

            Assert.Equal(3,            func.Parameters.Length);
            Assert.Equal("p",          func.Parameters[0].Name);
            Assert.Equal("min",        func.Parameters[1].Name);
            Assert.Equal("max",        func.Parameters[2].Name);
            Assert.Equal("Point<T>",   func.Parameters[0].Type);
            Assert.Equal("Point<T>",   func.Parameters[1].Type);
            Assert.Equal("Point<T>",   func.Parameters[2].Type);

            // var binder = new Binder();

            // binder.Visit(func);

            // Assert.Equal("Point",   func.ReturnType.Name);
            // Assert.Equal("T",       func.ReturnType.Arguments[0].Name);
        }
        
        [Fact]
        public void ParameterConditionShorthand()
        {
            var func = Parse<FunctionDeclarationSyntax>(@"
abs ƒ (a: Integer > 0) -> Integer {
  return 1;
}");
            
            // abs ƒ(a: Integer) -> Integer
            // where a > 0 {

            // }
            Assert.Equal("abs", func.Name);
            Assert.Equal("Integer", func.ReturnType);

            Assert.Equal("a",       func.Parameters[0].Name);
            Assert.Equal("Integer", func.Parameters[0].Type);
            Assert.Equal("a > 0",   func.Parameters[0].Condition.ToString());

        }

        [Fact]
        public void ParameterCondition()
        {
            var func = Parse<FunctionDeclarationSyntax>(@"
f ƒ (
  x: Integer where x > 0 && x < 10 @description(""A positive integer"")
) -> Integer {
  return 1;
}");

            // f ƒ(a: Integer) -> Integer
            // where x > 0 && x < 10 {

            // }
            Assert.Equal("f", func.Name);
            Assert.Equal("Integer", func.ReturnType);

            Assert.Equal("description", func.Parameters[0].Annotations[0].Name);
            Assert.Equal("A positive integer", func.Parameters[0].Annotations[0].Arguments[0].Value.ToString());

            Assert.Equal("x", func.Parameters[0].Name);
            Assert.Equal("Integer", func.Parameters[0].Type);
            Assert.Equal("x > 0 && x < 10", func.Parameters[0].Condition.ToString());
        }

        [Fact]
        public void FuncToString()
        {
            var func = Parse<FunctionDeclarationSyntax>("toString ƒ () => $\"{x},{y},{z}\"");

            Assert.Equal("toString", func.Name);

            Assert.Empty(func.Parameters);        }

        [Fact]
        public void Generic2()
        {
            var func = Parse<FunctionDeclarationSyntax>(
"dot ƒ <T: Number> (lhs: Point<T>, rhs: Point<T>) => lhs.x * lhs.x + rhs.y * rhs.y + lsh.z * rhs.z");

            // 'T / Generic arg

            Assert.Equal("dot",     func.Name);
            Assert.Single(          func.GenericParameters);
            Assert.Equal("T",       func.GenericParameters[0].Name);
            Assert.Equal("Number",  func.GenericParameters[0].Type);
            Assert.Equal(2,         func.Parameters.Length);

            Assert.Equal("Point",    func.Parameters[0].Type.Name);
            Assert.Equal("Point<T>", func.Parameters[0].Type.ToString());

            Assert.Equal("Point",    func.Parameters[1].Type.Name);
            Assert.Equal("Point<T>", func.Parameters[1].Type.ToString());

            Assert.Single(          func.Parameters[0].Type.Arguments);
            Assert.Equal("T",       func.Parameters[0].Type.Arguments[0].Name);
        }

        [Fact]
        public void Generic()
        {
            var func = Parse<FunctionDeclarationSyntax>(@"
clamp ƒ(p: geometry::Point<T>, min: Point<T>, max: Point<T>) => Point {
  x: max(min.x, min(max.x, p.x)),
  y: max(min.y, min(max.y, p.y)),
  z: max(min.z, min(max.z, p.z))
}
");

            Assert.Equal("clamp", func.Name);
            Assert.Equal(3, func.Parameters.Length);

            Assert.Equal("p",        func.Parameters[0].Name);
            Assert.Equal("geometry", func.Parameters[0].Type.Module);

            Assert.Equal("Point",    func.Parameters[0].Type.Name);
            Assert.Equal("T",        func.Parameters[0].Type.Arguments[0].Name);
        }

        [Fact]
        public void Predicate()
        {
            var func = Parse<FunctionDeclarationSyntax>("a function(a: Integer > 0) => 1");

            Assert.Equal("a > 0", func.Parameters[0].Condition.ToString());
        }


      

        [Fact]
        public void Log2()
        {
            var f = Parse<FunctionDeclarationSyntax>(@"
log2 ƒ(x) {
 var n = 1, i = 0;

 while x > n {
   n <<= 1

   i += 1
 }

 return i
}");


        }
        [Fact]
        public void Readi8()
        {
            var f = Parse<FunctionDeclarationSyntax>(@"readi8 ƒ(start, data) => data[start] << 24 >> 24");
        }

        // (i8,i8) -> i8
        // (a)->(b)
        // (Integer,Integer) -> i8
        // ƒ(a)->c

        [Fact]
        public void TypedFunction()
        {
            var w = Parse<FunctionDeclarationSyntax>(@"
sum ƒ(a: Integer, b: Integer) {
  return a + b
}
");

            Assert.Equal(2,         w.Parameters.Length);
            Assert.Equal("a",       w.Parameters[0].Name);
            Assert.Equal("Integer", w.Parameters[0].Type.ToString());

            var body = (BlockSyntax)w.Body;

            var returnStatement = (ReturnStatementSyntax)body.Statements[0];

            Assert.Single(body.Statements);
        }

        [Fact]
        public void InferedFunction()
        {
            var w = Parse<FunctionDeclarationSyntax>(@"
sum ƒ(a, b) {
  return a + b
}");

            Assert.Equal(2, w.Parameters.Length);
            Assert.Equal("a", w.Parameters[0].Name);
            Assert.Equal("b", w.Parameters[1].Name);
        }

        [Fact]
        public void InferedFunctionLambda()
        {
            var w = Parse<FunctionDeclarationSyntax>("sum ƒ(a, b) => a + b");

            Assert.Equal(2, w.Parameters.Length);
            Assert.Equal("a", w.Parameters[0].Name);
            Assert.Equal("b", w.Parameters[1].Name);
        }

        [Fact]
        public void DefaultParameters()
        {
            var w = Parse<FunctionDeclarationSyntax>(@"
add100 ƒ(a, b: Integer = 100) => a + b
");

            Assert.Equal("add100",   w.Name.ToString());
            Assert.Equal(2,          w.Parameters.Length);
            Assert.Equal("100",      ((NumberLiteralSyntax)w.Parameters[1].DefaultValue).Text);

            Assert.True(w.Body is LambdaExpressionSyntax);
        }
    }
}
