using Xunit;

namespace D.Compilation.Tests
{
    public class MasonaryTest
    {
        [Fact]
        public void A()
        {
            var a = Helper.Transpile(@"

Layout protocol {
  layout (nodes: [ Node ] ) -> Size
}

Size struct {
  width: Number
  height: Number
}

Box struct {
  var width  = 0.0, 
      height = 0.0,
      top    = 0.0, 
      left   = 0.0
}

Masonary class {
  gap     :   Number
  columns : [ Box ]

  init (columnCount: Int32, columnWidth: Float32, gap: Float64 = 10) {
    var left = 0
    
    gap     = gap
    columns = [ Box ];

    for i in 0..<columnCount {
      columns.add(Box(
        width  : columnWidth,
        height : 0,
        top    : 0,
        left   : left
      ))

      left += columnWidth + gap
    }
  }

  shortestColumn ƒ() => columns.orderByDescending(c => c.height) |> first;
}

Layout impl for Masonary {
  layout ƒ (elements: [ Node ]) { 
    for el in elements {
      let column = shortestColumn()
                
      // Add bottom gutter
      if column.height > 0 {
        column.height += columnGap
      }

      el.left = column.left
      el.top  = column.height

      // Add the item height to the column
      column.height += el.height
    }
    
    return Size(
      width  : 100,
      height : columns.max(c => c.height)
    )
  }
}
");
            // throw new System.Exception(a.ToString());
        }
    }

    public class ModuleTests
    {
        [Fact]
        public void A()
        {
            Assert.Equal(@"
namespace Fancy
{
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
    }

    public static string A(Point point)
    {
        var x = point.X;
    }
}
".Trim(),

Helper.Transpile(@"


Fancy module {
    Point struct {
        x: Float64
        y: Float64
        z: Float64
    }

    a ƒ(point: Point) -> String {
        let (x, _, _) = point
    }
}
"));
        }

        [Fact]
        public void B()
        {
            Assert.Equal(@"
namespace Fancy
{
    namespace Fancier
    {
        public static string A(Point<T> point)
        {
            var x = point.X;
            var y = point.Y;
            var z = point.Z;
        }
    }
}".Trim(),

    Helper.Transpile(@"
Fancy module {
  Fancier module {
    a ƒ(point: Point<T>) -> String {
      let (x, y, z) = point
    }
  }
}
"));
        }
    }
}