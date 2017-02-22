using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class ModuleTests : TestBase
    {
     
        [Fact]
        public void DefaultModule()
        {
            var array = Parse<ModuleSyntax>(@"

Color module { 
  CMYK type  { c, m, y, k: i8 }
  RGBA type  { r, g, b, a: i8 }
  HSL  type  { h, s, l: f32   }
  YCbCr type { y, cB, cR: i8  }
}

");

            Assert.Equal(4, array.Statements.Length);
        }

        
    }
}