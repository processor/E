using Xunit;

namespace D.Parsing.Tests
{
    using static TokenKind;

    public class KeywordTests
    {
        [Theory]
        [InlineData("module",   Module)]

        [InlineData("protocol", Protocol)]
        [InlineData("from",     From)]
        [InlineData("for",      For)]
        [InlineData("using",    Using)]
        [InlineData("impl",     Implementation)]
        [InlineData("in",       In)]
        [InlineData("let",      Let)]
        [InlineData("var",      Var)]
        [InlineData("mutable",  Mutable)]
        [InlineData("return",   Return)]
        [InlineData("select",   Select)]
        [InlineData("type",     Type)]
        [InlineData("orderby",  Orderby)]
        [InlineData("with",     With)]
        [InlineData("when",     When)]
        [InlineData("where",    Where)]
        public void A(string name, TokenKind type)
        {
            Assert.Equal(name, new Tokenizer(name).Read(type));
        }

        [Theory]
        [InlineData("$"   , Dollar)]
        [InlineData("|"   , Bar)]
        [InlineData("."   , Dot)]
        [InlineData(".."  , DotDot)]
        [InlineData("..." , DotDotDot)]
        [InlineData("`"   , Backtick)]
        // [InlineData("#"   , Pound)]
        [InlineData('"'   , Quote)]
        [InlineData('\''  , Apostrophe)]
        public void Symbols(string name, TokenKind type)
        {
            Assert.Equal(name, new Tokenizer(name).Read(type));
        }
    }
}