namespace E.Language.Tests;

public class TypeTests
{
    [Fact]
    public void A()
    {
        Assert.Equal("Map<String,Int64>", new Type(ObjectType.Map, new Type(ObjectType.String), new Type(ObjectType.Int64)).ToString());
        Assert.Equal("Set<String>",       new Type(ObjectType.Set, new Type(ObjectType.String)).ToString());
        Assert.Equal("Array<String>",     new Type(ObjectType.Array, new Type(ObjectType.String)).ToString());
        Assert.Equal("String",            new Type(ObjectType.String).ToString());
    }
}