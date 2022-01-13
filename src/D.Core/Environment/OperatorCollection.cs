using System;
using System.Diagnostics.CodeAnalysis;

using E.Collections;

namespace E;

public class OperatorCollection
{
    private readonly Trie<Operator> trie = new ();

    public bool Contains(ReadOnlySpan<char> symbol) => trie.ContainsKey(symbol);

    public bool TryGet(ReadOnlySpan<char> symbol, [NotNullWhen(true)] out Operator? op) => trie.TryGetValue(symbol, out op);

    public void Add(params Operator[] ops)
    {
        foreach (var op in ops)
        {
            trie.Add(AsSymbol(op.Type) + op.Name, op);
        }
    }

    public Operator this[OperatorType type, string name] => trie[AsSymbol(type) + name];

    public bool Maybe(OperatorType type, char ch, [NotNullWhen(true)] out Trie<Operator>.Node? node)
    {
        return trie.TryGetNode(AsSymbol(type), out node) && node.TryGetNode(ch, out node);
    }

    private static string AsSymbol(OperatorType type) => type switch { 
        OperatorType.Infix   => "infix_",
        OperatorType.Prefix  => "prefix_",
        OperatorType.Postfix => "postfix_",
        _                    => string.Empty
    };       
}