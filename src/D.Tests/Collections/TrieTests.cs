namespace E.Collections.Tests;

public class TrieTests
{
    [Fact]
    public void Test()
    {
        var trie = new Trie<int>();

        trie.Add("a", 1);
        trie.Add("aa", 2);
        trie.Add("aaa", 3);
        trie.Add("aaaaa", 5);

        Assert.Equal(4, trie.Count);

        Assert.Equal(1, trie["a"]);
        Assert.Equal(2, trie["aa"]);
        Assert.Equal(3, trie["aaa"]);
        Assert.Equal(5, trie["aaaaa"]);

        Assert.False(trie.ContainsKey("aaaa"));
        Assert.False(trie.ContainsKey("oranges"));
    }
}