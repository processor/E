using System;
using System.Collections.Generic;
using System.Linq;

namespace D.Collections
{
    public class Trie<T>
    {
        private int count;

        public int Count => count;

        public Node Root { get; } = new Node(char.MinValue, null);

        public T this[string key]
        {
            get
            {
                if (!TryGetValue(key, out T value))
                {
                    throw new KeyNotFoundException($"Key '{key}' not found.");
                }

                return value;
            }

            set
            {
                if (TryGetNode(key, out Node node))
                {
                    node.Value = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public void Add(string key, T value)
        {
            #region Preconditions

            if (key == null) throw new ArgumentNullException(nameof(key));

            #endregion

            var node = Root;

            foreach (var character in key)
            {
                node = node.Add(character);
            }

            if (node.IsLeaf) throw new ArgumentException($"Key {key} already exists");

            node.Value = value;

            count++;
        }

        public bool ContainsKey(string key)
            => TryGetValue(key, out T value);

        public IEnumerable<KeyValuePair<string, T>> Scan(string prefix)
        {
            var node = Root;

            foreach (var item in prefix)
            {
                if (!node.TryGetNode(item, out node))
                {
                    return Enumerable.Empty<KeyValuePair<string, T>>();
                }
            }

            return node.Enumerator();
        }

        public bool Remove(string key)
        {
            #region Preconditions

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            #endregion

            if (!TryGetNode(key, out Node node))
            {
                return false;
            }

            if (!node.IsLeaf)
            {
                return false;
            }

            RemoveNode(node);

            return true;
        }

        public bool TryGetValue(string key, out T value)
        {
            #region Preconditions

            if (key == null) throw new ArgumentNullException(nameof(key));

            #endregion

            if (!TryGetNode(key, out Node node) || !node.IsLeaf)
            {
                value = default(T);

                return false;
            }

            value = node.Value;

            return true;
        }

        private void RemoveNode(Node node)
        {
            node.Remove();
            count--;
        }

        public bool TryGetNode(string key, out Node node)
        {
            node = Root;

            foreach (var c in key)
            {
                if (!node.TryGetNode(c, out node))
                {
                    return false;
                }
            }

            return true;
        }

        #region Node

        public class Node
        {
            private T value;
            private bool isLeaf = false;

            internal Node(char character, Node parent)
            {
                Character = character;
                Parent = parent;
            }

            public char Character { get; }

            // Depth?

            public string Key
            {
                get
                {
                    var stack = new Stack<char>();

                    stack.Push(Character);

                    Node node = this;

                    while ((node = node.Parent).Parent != null)
                    {
                        stack.Push(node.Character);
                    }

                    return new string(stack.ToArray());
                }
            }

            public T Value
            {
                get { return value; }
                set
                {
                    this.value = value;

                    isLeaf = true;
                }
            }

            private Node Parent { get; }

            public Dictionary<char, Node> Children = new Dictionary<char, Node>();

            internal Node Add(char key)
            {
                if (!Children.TryGetValue(key, out Node childNode))
                {
                    childNode = new Node(key, parent: this);

                    Children.Add(key, childNode);
                }

                return childNode;
            }

            public bool IsLeaf => isLeaf;

            internal void Clear()
            {
                Children.Clear();
            }

            internal IEnumerable<Node> ScanNodes()
            {
                foreach (var child in Children)
                {
                    if (child.Value.IsLeaf)
                    {
                        yield return child.Value;
                    }

                    foreach (var item in child.Value.ScanNodes())
                    {
                        if (item.IsLeaf)
                        {
                            yield return item;
                        }
                    }
                }
            }

            internal IEnumerable<KeyValuePair<string, T>> Enumerator()
            {
                if (IsLeaf)
                {
                    yield return new KeyValuePair<string, T>(Key, Value);
                }

                foreach (var item in Children)
                {
                    foreach (var element in item.Value.Enumerator())
                    {
                        yield return element;
                    }
                }
            }

            internal void Remove()
            {
                Value = default(T);

                isLeaf = false;

                if (Children.Count == 0 && Parent != null)
                {
                    Parent.Children.Remove(Character);

                    if (!Parent.IsLeaf)
                    {
                        Parent.Remove();
                    }
                }
            }

            public bool Contains(char key)
                => Children.ContainsKey(key);

            public bool TryGetNode(char key, out Node node)
                => Children.TryGetValue(key, out node);
        }

        #endregion
    }
}


// ref: https://en.wikipedia.org/wiki/Trie
