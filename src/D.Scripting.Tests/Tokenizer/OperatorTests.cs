using Xunit;

namespace D.Parsing.Tests
{
    using static TokenKind;

    public class OperatorTests
    { 
        [Theory]
        [InlineData("*")]
        [InlineData("+")]
        [InlineData("-")]
        [InlineData('/')]
        [InlineData(">>")]
        [InlineData(">>>")]
        [InlineData("<<")]
        [InlineData("^")]       // xor, ⊕
        [InlineData("||")] // or
        [InlineData("&&")] // and
        public void Ops(string name)
        {
            using (var tokenizer = new Tokenizer(name))
            {
                Assert.Equal(name, tokenizer.Read(Op));
            }
        }
    }
}
