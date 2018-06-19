
using Xunit;

namespace D.Compilation.Tests
{
    public class ModuleTests
    {
        [Fact]
        public void A()
        {
            Assert.Equal(@"
namespace Fancy
{
    public static string A(Point<T> point)
    {
        var x = point.X;
    }
}".Trim(),

Helper.Transpile(@"
Fancy module {
  a ƒ(point: Point<T>) -> String {
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