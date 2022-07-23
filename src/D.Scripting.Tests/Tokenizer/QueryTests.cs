namespace E.Parsing.Tests;

using static TokenKind;

public class TokenQueryTests
{
    [Fact]
    public void A()
    {
        var tokenizer = new Tokenizer(
            """
            from account in Accounts
            where account.balance > 0
            select sum account.balance
            orderby id descending
            skip 5
            take 10
            """);

        Assert.Equal("from",       tokenizer.Read(From));
        Assert.Equal("account",    tokenizer.Read(Identifier));
        Assert.Equal("in",         tokenizer.Read(In));
        Assert.Equal("Accounts",   tokenizer.Read(Identifier));
        Assert.Equal("where",      tokenizer.Read(Where));
        Assert.Equal("account",    tokenizer.Read(Identifier));
        Assert.Equal(".",          tokenizer.Read(Dot));
        Assert.Equal("balance",    tokenizer.Read(Identifier));
        Assert.Equal(">",          tokenizer.Read(Op));
        Assert.Equal("0",          tokenizer.Read(Number));
        Assert.Equal("select",     tokenizer.Read(Select));
        Assert.Equal("sum",        tokenizer.Read(Identifier));
        Assert.Equal("account",    tokenizer.Read(Identifier));
        Assert.Equal(".",          tokenizer.Read(Dot));
        Assert.Equal("balance",    tokenizer.Read(Identifier));
        Assert.Equal("orderby",    tokenizer.Read(Orderby));
        Assert.Equal("id",         tokenizer.Read(Identifier));
        Assert.Equal("descending", tokenizer.Read(Descending));
        Assert.Equal("skip",       tokenizer.Read(Identifier));
        Assert.Equal("5",          tokenizer.Read(Number));
        Assert.Equal("take",       tokenizer.Read(Identifier));
        Assert.Equal("10",         tokenizer.Read(Number));
    }
}