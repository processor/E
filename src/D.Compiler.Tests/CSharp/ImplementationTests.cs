
using Xunit;

namespace D.Compilation.Tests
{
    using static Helper;

    public class ImplementationTests
    {
        [Fact]
        public void BezierType()
        {
            var r = Transpile(@"
Curve protocol {
  getPoint (position: Number) -> Vector2
}

Bezier struct { 
  c1: Vector3 // anchor point coordinates
  c2: Vector3 // first control point
  c3: Vector3 // second control point
  c4: Vector3 // second anchor point 
}

Curve impl for Bezier {
  getPoint (t: Number) => Vector2 (
    x: c1.x * b1(t) + c2.x * b2(t) + c3.x * b3(t) + c4.x * c4(t),
    y: c1.y * b1(t) + c2.y * b2(t) + c3.y * b3(t) + c4.y * b4(t)
  )

  private b1 (t: Number) => t * t * t
  private b2 (t: Number) => 3 * t * t * (1-t)
  private b3 (t: Number) => 3 * t * (1-t) * (1-t)
  private b4 (t: Number) => (1 - t) * (1 - t) * (1 - t)
}
");

            // throw new System.Exception(r);

            Assert.Equal(@"
public interface Curve
{
    Vector2 GetPoint(double position);
}

public struct Bezier : Curve
{
    public Bezier(Vector3 c1, Vector3 c2, Vector3 c3, Vector3 c4)
    {
        C1 = c1;
        C2 = c2;
        C3 = c3;
        C4 = c4;
    }

    public Vector3 C1 { get; }

    public Vector3 C2 { get; }

    public Vector3 C3 { get; }

    public Vector3 C4 { get; }

    Vector2 Curve.GetPoint(double t) => new Vector2(x: c1.X * b1(t) + c2.X * b2(t) + c3.X * b3(t) + c4.X * c4(t), y: c1.Y * b1(t) + c2.Y * b2(t) + c3.Y * b3(t) + c4.Y * b4(t));

    private object B1(double t) => t * t * t;

    private object B2(double t) => 3 * t * t * (1 - t);

    private object B3(double t) => 3 * t * (1 - t) * (1 - t);

    private object B4(double t) => (1 - t) * (1 - t) * (1 - t);
}
".Trim(), r);
        }

        [Fact]
        public void Var()
        {
            Assert.Equal(@"
public class Cuboid
{
    public Cuboid(List<Polygon> polygons)
    {
        Polygons = polygons;
    }

    public List<Polygon> Polygons { get; }
}
".Trim(),

            Helper.Transpile(@"
Cuboid class {
  polygons: [ Polygon ]
}

Cuboid impl
{
    let definition = [
        // faces       normals (aka direction)
        [ [ 0, 4, 6, 2 ], [ -1,  0,  0 ] ],       
        [ [ 1, 3, 7, 5 ], [ +1,  0,  0 ] ],
        [ [ 0, 1, 5, 4 ], [  0, -1,  0 ] ],
        [ [ 2, 6, 7, 3 ], [  0, +1,  0 ] ],
        [ [ 0, 2, 3, 1 ], [  0,  0, -1 ] ],
        [ [ 4, 5, 7, 6 ], [  0,  0, +1 ] ]
    ]
}"));
        }

        [Fact]
        public void Z()
        {
            Assert.Equal(@"
public static string A(Point<T> point)
{
    var x = point.X;
    var y = point.Y;
    var z = point.Z;
}".Trim(),

Transpile(@"
a ƒ(point: Point<T>) -> String {
  let (x, y, z) = point
}
"));
        }

        [Fact]
        public void X()
        {
            Assert.Equal(@"
public static object CubicIn(double x) => x * x * x;

public static object CubicOut(double x) => Math.Pow(x - 1, 3) + 1;

public static double Linear(double x) => x;

public static object SinIn(double x) => Math.Sin(x * Math.PI * 0.5);

public static object SinOut(double x) => -Math.Cos(Math.PI * x) / 2 + 0.5;
".Trim(),

            Transpile(@"
cubicIn  ƒ(x: Number) => x * x * x;
cubicOut ƒ(x: Number) => (x - 1) ** 3 + 1;
linear   ƒ(x: Number) => x;
sinIn    ƒ(x: Number) => sin(x * π * 0.5);
sinOut   ƒ(x: Number) => - cos(π * x) / 2 + 0.5;
"));
        }


        [Fact]
        public void Q()
        {
            Assert.Equal(@"
public static Point<T> Clamp<T>(Point<T> p, Point<T> min, Point<T> max) => new Point<T>(x: Math.Max(min.X, Math.Min(max.X, p.X)), y: Math.Max(min.Y, Math.Min(max.Y, p.Y)), z: Math.Max(min.Z, Math.Min(max.Z, p.Z)));

".Trim(),

            Transpile(@"
clamp ƒ <T> (p: Point<T>, min: Point<T>, max: Point<T>) => Point<T>(
  x: max(min.x, min(max.x, p.x)),
  y: max(min.y, min(max.y, p.y)),
  z: max(min.z, min(max.z, p.z))
)

"));
        }

        [Fact]
        public void SimpleProperties()
        {
            Assert.Equal(@"

namespace Namespaced
{
    public struct Vector3
    {
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }
    }

    public struct Sphere
    {
        public Sphere(Vector3 center)
        {
            Center = center;
        }

        public Vector3 Center { get; }
    }

    public class Class
    {
        public long A => 1;

        public string B => ""b"";

        public long C() => 1;

        public int D(int x) => x;

        public Vector3 E(Vector3 point) => point;

        public double F(Vector3 point) => point.X;

        public double G(Sphere sphere) => sphere.Center.X;
    }
}
".Trim(),

        Transpile(@"

Vector3 struct {
  x, y, z: Float64
}

Sphere struct {
  center: Vector3
}

Class class { 

}

Class impl { 
  a => 1
  b => ""b""
  c () => 1
  d (x: Int32) => x
  e (point: Vector3) => point
  f (point: Vector3) => point.x
  g (sphere: Sphere) => sphere.center.x;
}

", "Namespaced"));
        }


        [Fact]
        public void WriteModule()
        {
            Assert.Equal(@"

namespace Banking
{
    public class Bank
    {
        public Bank(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
".Trim(),

Transpile(@"
Bank class { 
  name: String
}", "Banking"));
        }

        [Fact]
        public void RewriteType()
        {
            // [Record]

            Assert.Equal(@"
public class Account
{
    public Account(decimal balance, Entity owner, DateTime created)
    {
        Balance = balance;
        Owner = owner;
        Created = created;
    }

    public decimal Balance { get; set; }

    public Entity Owner { get; }

    public DateTime Created { get; }
}

".Trim(),

Transpile(@"

Account record { 
  mutable balance : Decimal
  owner           : Entity
  created         : DateTime
}

Account impl { }

"));
        }

        [Fact]
        public void Z1()
        {
            Assert.Equal(@"
public struct Matrix4<T>
{
    public Matrix4(List<T> elements)
    {
        Elements = elements;
    }

    public List<T> Elements { get; }

    public object this[long index] => this.Elements[index];

    public object M11 => this[0];

    public object M12 => this[4];

    public object M13 => this[8];

    public object M14 => this[12];

    public object M21 => this[1];

    public object M22 => this[5];

    public object M23 => this[9];

    public object M24 => this[13];

    public object M31 => this[2];

    public object M32 => this[6];

    public object M33 => this[10];

    public object M34 => this[14];

    public object M41 => this[3];

    public object M42 => this[7];

    public object M43 => this[11];

    public object M44 => this[15];
}

".Trim(),
Transpile(@"
Matrix4 struct <T> {
  elements: [T] 
}

Matrix4 impl {
  [index: Int64] => this.elements[index]

  m11 => this[0]  // 1, 1
  m12 => this[4]  // 1, 2
  m13 => this[8]  // 1, 3
  m14 => this[12] // 1, 4
  m21 => this[1]  // 2, 1
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
}"));
        }

        [Fact]
        public void RewriteOperatorsMin()
        {
            Assert.Equal(@"
public struct Point
{
    public Point(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double X { get; }

    public double Y { get; }

    public double Z { get; }

    public static Point operator *(Point a, double b) => new Point(x: a.X * b, y: a.Y * b, z: a.Z * b);

    public static Point operator /(Point a, double b) => new Point(x: a.X / b, y: a.Y / b, z: a.Z / b);

    public static Point operator +(Point a, Point b) => new Point(x: a.X + b.X, y: a.Y + b.Y, z: a.Z + b.Z);

    public static Point operator -(Point a, Point b) => new Point(x: a.X - b.X, y: a.Y - b.Y, z: a.Z - b.Z);
}
".Trim(),

Transpile(@"
Point struct { x, y, z: Number }

Point impl {
  * (a: Point, b: Number) => Point(x: a.x * b,   y: a.y * b,   z: a.z * b);
  / (a: Point, b: Number) => Point(x: a.x / b,   y: a.y / b,   z: a.z / b);
  + (a: Point, b: Point)  => Point(x: a.x + b.x, y: a.y + b.y, z: a.z + b.z);
  - (a: Point, b: Point)  => Point(x: a.x - b.x, y: a.y - b.y, z: a.z - b.z);
}
"));
        }

        [Fact]
        public void RewriteOperators()
        {
            Assert.Equal(@"
public struct Point
{
    public static Point operator *(Point a, double b) => new Point(x: a.X * b, y: a.Y * b, z: a.Z * b);

    public static Point operator /(Point a, double b) => new Point(x: a.X / b, y: a.Y / b, z: a.Z / b);

    public static Point operator +(Point p1, Point p2) => new Point(x: p1.X + p2.X, y: p1.Y + p2.Y, z: p1.Z + p2.Z);

    public static Point operator -(Point p1, Point p2) => new Point(x: p1.X - p2.X, y: p1.Y - p2.Y, z: p1.Z - p2.Z);
}
".Trim(),

Transpile(@"
Point struct { }

Point impl {
  * (a: Point, b: Number)  => Point(x: a.x * b, y: a.y * b, z: a.z * b);
  / (a: Point, b: Number)  => Point(x: a.x / b, y: a.y / b, z: a.z / b);
  + (p1: Point, p2: Point) => Point(x: p1.x + p2.x, y: p1.y + p2.y, z: p1.z + p2.z);
  - (p1: Point, p2: Point) => Point(x: p1.x - p2.x, y: p1.y - p2.y, z: p1.z - p2.z);
}

"));
        }


        [Fact]
        public void GenericPoint()
        {
            Assert.Equal(@"
public struct Point<T>
{
    public Point(T x, T y, T z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public T X { get; }

    public T Y { get; }

    public T Z { get; }
}
".Trim(),

Transpile(@"

Point struct <T> { 
  x, y, z: T
}

"));
        }



        [Fact]
        public void WriteProtocol()
        {
            Assert.Equal(@"
public interface Curve
{
    Point GetPoint(double t);
}
".Trim(),

Transpile(@"
Curve protocol { 
  getPoint (t: Number) -> Point
}
"));
        }



        [Fact]
        public void WriteProtocol3()
        {
            Assert.Equal(@"
public interface Observer
{
    Function<A,B,C,D> Next { get; }
    Function<A,B> Next { get; }
    Function<A,Function<B,C>> Next { get; }
}
".Trim(),

Transpile(@"
Observer protocol { 
  next -> (A, B, C) -> D
  next -> A -> B
  next -> A -> B -> C
}
"));

        }

        [Fact]
        public void WriteProtocol2()
        {
            Assert.Equal(@"
public interface Node
{
    Kind Kind { get; }
    List<Node> Children { get; }
}
".Trim(),

Transpile(@"
Node protocol { 
  kind -> Kind
  children -> [ Node ]
}
"));
        }



        [Fact]
        public void Constructors()
        {
            Assert.Equal(@"
public interface Geometry
{
    Point Center { get; }
}

public struct Point : Geometry
{
    public Point(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
        Z = 0;
    }

    public Point(double v)
    {
        X = v;
        Y = v;
        Z = v;
    }

    public double X { get; }

    public double Y { get; }

    public double Z { get; }

    public object Length => x + y + z;

    Point Geometry.Center => this;
}
".Trim(),


Transpile(@"
Geometry protocol {
  center -> Point
}

Point struct { 
  x, y, z: Float
}

Point impl {
  from (x: Float, y: Float) => Point(x, y, z: 0)
  from (v: Float)           => Point(x: v, y: v, z: v)
 
  length => x + y + z;
}

Geometry impl for Point { 
  center => this
}

"));
        }

        [Fact]
        public void Unit()
        {
            var unit = Helper.CompileModule(@"
Point struct { 
  x, y, z: Number
}

Point impl {
  from (x, y) => Point(x, y, z: 0);
  from v      => Point(x: v, y: v, z: v);
}
");

            var pointType = unit.Statements[0] as Type;

            // Assert.Equal(1, unit.Members.Count);

            Assert.Equal("Point", pointType.Name);
            Assert.Equal(3, pointType.Properties.Length);

            // Assert.Equal(1, unit.Implementations[unit.Types[0]].Count);
            // Assert.Equal(2, unit.Implementations.First()[0].Members.Length);
        }


        [Fact]
        public void Y()
        {
            Assert.Equal(
@"public static Point<T> Negate<T>(Point<T> value) => new Point<T>(x: -value.X, y: -value.Y, z: -value.Z);",

Transpile(
@"negate ƒ <T> (value: Point<T>) => Point<T>(
  x: - value.x, 
  y: - value.y, 
  z: - value.z 
)"));
        }
    }
}
