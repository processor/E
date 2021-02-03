using Xunit;

namespace E.Parsing.Tests
{
    using static TokenKind;

    public class SuperscriptTests
    {
        [Fact]
        public void ReadSuperscript()
        {
            var tokenizer = new Tokenizer("5 m³");

            Assert.Equal("5", tokenizer.Read(Number));
            Assert.Equal("m", tokenizer.Read(Identifier));
            Assert.Equal("³", tokenizer.Read(Superscript));
        }
    }
}