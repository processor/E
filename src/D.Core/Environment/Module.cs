using System.Collections;
using System.Collections.Generic;

namespace D
{
    public class Module : IEnumerable<(string, object)>
    {
        public readonly List<(string, object)> members = new List<(string, object)>();

        public Module(string name = null, Module parent = null)
        {
            Name = name;
            Parent = parent;
        }

        public Module Parent { get; }

        public string Name { get; } // This may have mutiple levels...

        public (string, object) this[int index] => members[index];

        public void Add(INamedObject value) => members.Add((value.Name, value));

        public void Add(string name, object value) => members.Add((name, value));

        IEnumerator IEnumerable.GetEnumerator() => members.GetEnumerator();

        IEnumerator<(string, object)> IEnumerable<(string, object)>.GetEnumerator() => members.GetEnumerator();
    }
}