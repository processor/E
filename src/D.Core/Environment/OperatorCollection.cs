namespace D
{
    using Collections;

    public class OperatorCollection
    {
        private readonly Trie<Operator> trie = new Trie<Operator>();

        public bool Contains(string symbol) => trie.ContainsKey(symbol);

        public bool TryGet(string symbol, out Operator op) => trie.TryGetValue(symbol, out op);

        public void Add(params Operator[] ops)
        {
            foreach (var op in ops)
            { 
                trie.Add(AsSymbol(op.Type) + op.Name, op);
            }
        }

        public Operator this[OperatorType type, string name]  => trie[AsSymbol(type) + name];

        public bool Maybe(OperatorType type, char ch, out Trie<Operator>.Node node)
        {
            trie.TryGetNode(AsSymbol(type), out node);

            return node.TryGetNode(ch, out node);
        }

        private static string AsSymbol(OperatorType type)
        {
            switch (type)
            {
                case OperatorType.Infix   : return "infix_";
                case OperatorType.Prefix  : return "prefix_";
                case OperatorType.Postfix : return "postfix_";
                default                   : return "";
            }
        }
    }
}