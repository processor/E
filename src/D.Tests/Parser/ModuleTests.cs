using E.Syntax;

namespace E.Parsing.Tests;

public class ModuleTests : TestBase
{
    [Fact]
    public void DefaultModule()
    {
        var array = Parse<ModuleSyntax>(
            """
            Color module { 
              CMYK  struct { c, m, y, k: i8 }
              RGBA  struct { r, g, b, a: i8 }
              HSL   struct { h, s, l: f32   }
              YCbCr struct { y, cB, cR: i8  }
            }
            """);

        Assert.Equal(4, array.Statements.Count);
    }
}