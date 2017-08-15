using System.Linq;

using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class ImplementationTests : TestBase
    {
        [Fact]
        public void Type2_CompoundProperties()
        {
            var type = Parse<ImplementationDeclarationSyntax>(@"
Point impl {
  var x = 0, y = 0, z = 0
}
");

            // properties @ top level...

            var a = ((VariableDeclarationSyntax)type[0]);
        }

        [Fact]
        public void Type1()
        {
            var type = Parse<ImplementationDeclarationSyntax>(@"
Point impl {
  var public   x: Number = 0
  var private  y: Number = 0
  var internal z: Number = 0

  to String    => $""{x},{y},{z}""
  to [ T ]     => [ x, y, z ]
  to (T, T, T) => (x, y, z) 
}
");
            Assert.Equal("x",         ((VariableDeclarationSyntax)type[0]).Name);
            Assert.Equal("Number",      ((VariableDeclarationSyntax)type[0]).Type);

            Assert.Equal("String",      ((FunctionDeclarationSyntax)type[3]).ReturnType);
            Assert.Equal("List<T>",      ((FunctionDeclarationSyntax)type[4]).ReturnType);
            Assert.Equal("Tuple<T,T,T>", ((FunctionDeclarationSyntax)type[5]).ReturnType);

            foreach (var member in type.Members.OfType<FunctionDeclarationSyntax>())
            {
                Assert.True(member.IsConverter);
                Assert.True(member.Body is LambdaExpressionSyntax);
            }
        }

        [Fact]
        public void Converter()
        {
            var impl = Parse<ImplementationDeclarationSyntax>(@"
Point impl { 
  to String    => $""{x},{y},{z}""
  to [ T ]     => [ x, y, z ]
  to (T, T, T) => (x, y, z) 
}
");

            Assert.Equal("String",       ((FunctionDeclarationSyntax)impl[0]).ReturnType);
            Assert.Equal("List<T>",      ((FunctionDeclarationSyntax)impl[1]).ReturnType);
            Assert.Equal("Tuple<T,T,T>", ((FunctionDeclarationSyntax)impl[2]).ReturnType);

            foreach (var member in impl.Members.OfType<FunctionDeclarationSyntax>())
            {
                Assert.True(member.IsConverter);
                Assert.True(member.Body is LambdaExpressionSyntax);
            }
        }

        [Fact]
        public void Complicated()
        {

            var text = @"
Point type {
  x, y, z: T
}

Point<T> impl { 
  from (x, y, z: T) => Point<T>(x, y, z)
  from (x, y: T)    => Point<T>(x, y, z: 0)
  from (T)          => Point<T>(x: $0, y: $0, z: $0)
    
  to String => $""{x},{y},{z}"";

  negate () => Point<T>(
    x: -x,
    y: -y,
    z: -z
  )

  floor () => Point<T>(
    x: floor(x),
    y: floor(y),
    z: floor(z)
  )

  ceiling () => Point<T>(
    x: ceiling(x),
    y: ceiling(y),
    z: ceiling(z)
  )

  round () => Point(
    x: round(x),
    y: round(y),
    z: round(z)
  )
}
";

            using (var parser = new Parser(text))
            {
                var type = parser.Next();
                var implementation = parser.Next();
            }
        }

        [Fact]
        public void D()
        {
            var bank = Parse<ImplementationDeclarationSyntax>(@"
Point impl { 
  from (x: Number, y: Number, z: Number) {
    let point = Point(x, y, z)
    
    on point Changed change {
      log ""changed""
    }

    observe point Changed change { 
      log ""changed""
    } until point Destroyed
    
    on point Destroyed => log ""destroyed""

    return point
  }
}");
            var f  = (FunctionDeclarationSyntax)  bank.Members[0];
            var b  = (BlockExpressionSyntax)       f.Body;
            var o  = (ObserveStatementSyntax)     b.Statements[1];
            var o2 = (ObserveStatementSyntax)     b.Statements[2];
            var o3 = (ObserveStatementSyntax)     b.Statements[3];

            Assert.Equal("point", (Symbol)o.Observable);
            Assert.Equal("Changed", o.EventType.Name);
            Assert.Equal("change", o.ParameterName);
            Assert.Null(o.UntilExpression);

            Assert.Equal("point", o2.UntilExpression.Observable.ToString());
            Assert.Equal("Destroyed", o2.UntilExpression.Event.ToString());
        }

        [Fact]
        public void A()
        {
            var bank = Parse<ImplementationDeclarationSyntax>(@"
Bank impl { 
  openAccount (account: Account) {
    log ""neat""

    emit Account`Opened(name: ""fancy"")
  }

  close`Account (account: Account) {

  }
}");

            Assert.Equal(2, bank.Members.Length);

            var f = (FunctionDeclarationSyntax)bank.Members[0];

            Assert.Equal("openAccount", f.Name.ToString());
        }

        [Fact]
        public void OperatorTests()
        {
            var a = Parse<ImplementationDeclarationSyntax>(@"
Point impl { 
  * (p: Point, v: Number)  => Point(x: p.x * v, y: p.y * v, z: p.z * v);
  / (p: Point, v: Number)  => Point(x: p.x / v, y: p.y / v, z: p.z / v);
  + (p1: Point, p2: Point) => Point(x: p1.x + p2.x, y: p1.y + p2.y, z: p1.z + p2.z);
  - (p1: Point, p2: Point) => Point(x: p1.x - p2.x, y: p1.y - p2.y, z: p1.z - p2.z);
}");

            Assert.Equal(4, a.Members.Length);

            var l = (LambdaExpressionSyntax)((FunctionDeclarationSyntax)a.Members[0]).Body;

            Assert.Equal(3, ((ObjectInitializerSyntax)l.Expression).Arguments.Length);

            foreach (var member in a.Members.OfType<FunctionDeclarationSyntax>())
            {
                Assert.True(member.IsStatic);
                Assert.True(member.IsOperator);
            }
        }
        
        [Fact]
        public void B()
        {
            var a = Parse<ImplementationDeclarationSyntax>(@"
A impl { 
  zero   => Point(x: 1, y: 1, z: 1)
  string => ""neat""
  tuple  => (1, 1, 1)
}");

            Assert.Equal(3, a.Members.Length);

            var f = (FunctionDeclarationSyntax)a.Members[0];
            var l = (LambdaExpressionSyntax)f.Body;
            var p = (ObjectInitializerSyntax)l.Expression;

            var t = (TupleExpressionSyntax)((LambdaExpressionSyntax)((FunctionDeclarationSyntax)a.Members[2]).Body).Expression;

            Assert.Equal("zero", f.Name);
            Assert.Equal("Point", p.Type);

            Assert.Equal(3, t.Size);
        }

        [Fact]
        public void Expression()
        {
            var i = Parse<ImplementationDeclarationSyntax>(@"
A implementation { 
   a => Point(x: p.x * v, y: p.y * v, z: p.z * v)
}
");

            var f = (FunctionDeclarationSyntax)i.Members[0];
            var l = (LambdaExpressionSyntax)f.Body;
            var t = (ObjectInitializerSyntax)l.Expression;

            Assert.Equal("Point", t.Type.Name);
            Assert.True(f.IsProperty);
            Assert.Equal(3, t.Arguments.Length);
        }

        [Fact]
        public void Members()
        {
            var declaration = Parse<ImplementationDeclarationSyntax>(@"
Matrix4<T> implementation {
  [index: Integer] => this.elements[index]

  m11 => this[0]  // 1, 1
  m12 => this[4]  // 1, 2
  m13 => this[8]  // 1, 3
  m14 => this[12] // 1, 4
  m21 => this[1]  // 2, 1SELF.
  m22 => this[5]  // 2, 2
  m23 => this[9]  // 2, 3
  m24 => this[13] // 2, 4
  m31 => this[2]  // 3, 1
  m32 => this[6]  // 3, 2
  m33 => this[10] // 3, 3
  m34 => this[14] // 3, 4
  m41 => this[3]  // 4, 1
  m42 => this[7]  // 4, 2
  m43 => this[11] // 4, 3
  m44 => this[15] // 4, 4
}");

            Assert.Equal("Matrix4<T>", declaration.Type);
            Assert.Equal(null, declaration.Protocol);
            Assert.Equal(17, declaration.Members.Length);

            var indexer = (FunctionDeclarationSyntax)declaration[0];
            
            Assert.Equal("index", indexer.Parameters[0].Name);
            Assert.Equal("Integer", indexer.Parameters[0].Type);
            Assert.True(indexer.IsIndexer);

            var n11 = (FunctionDeclarationSyntax)declaration[1];
            var n12 = (FunctionDeclarationSyntax)declaration[2];

            Assert.Equal("m11", n11.Name);
            Assert.True(n11.IsProperty);
            Assert.True(((LambdaExpressionSyntax)n11.Body).Expression is IndexAccessExpressionSyntax);

            Assert.Equal("m12", n12.Name);
            Assert.True(n12.IsProperty);
        }

        [Fact]
        public void ModulePrefixedProtocolImplementation()
        {
            var declaration = Parse<ImplementationDeclarationSyntax>(@"
HTML::Element impl for CustomElement {

}");

            Assert.Equal("HTML", declaration.Protocol.Module);
            Assert.Equal("Element", declaration.Protocol.Name);
            Assert.Equal("CustomElement", declaration.Type.Name);
        }

        [Fact]
        public void Z()
        {
            var declaration = Parse<ImplementationDeclarationSyntax>(@"
Curve <T> implementation for Arc<T> {
  getPoint(this, t: Number) {
    var deltaAngle = endAngle - startAngle
    let samePoints = abs(deltaAngle) < ε

    // ensures that deltaAngle is 0 .. 2 π
    while deltaAngle < 0     { deltaAngle += π * 2 }
    while deltaAngle > π * 2 { deltaAngle -= π * 2 }
    
    if deltaAngle < ε {
      deltaAngle = samePoints ? 0 : π * 2
    }

    if direction == Clockwise && !samePoints {
      deltaAngle = (deltaAngle == (π * 2)) ? - (π * 2) : deltaAngle - (π * 2)
    }

    let angle = startAngle + t * deltaAngle

    return Point<T>(
      x: x + xRadius * cos(angle),
      y: y + yRadius * sin(angle),
      z: 0
    )
  }
}");

            Assert.Equal("Curve<T>", declaration.Protocol);
            Assert.Equal("Arc<T>",   declaration.Type);

            var f = (FunctionDeclarationSyntax)declaration.Members[0];

            Assert.Equal("getPoint", f.Name);

            var body     = (BlockExpressionSyntax)f.Body;

            var returnStatement = (ReturnStatementSyntax)body[body.Statements.Length - 1];
            var initializer     = (ObjectInitializerSyntax)returnStatement.Expression;

            Assert.Equal("Point<T>", initializer.Type);
        }
    }
}

