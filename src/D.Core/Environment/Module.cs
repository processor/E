using System.Collections;
using System.Collections.Generic;

namespace D
{
    public class Module : IEnumerable<(string, object)>
    {
        public readonly List<(string, object)> members = new List<(string, object)>();
      
        public Module(string name = null)
        {
            Name = name;
        }

        public string Name { get; set; }
 

        public (string, object) this[int index] => members[index];

        public void Add(INamedObject value) => members.Add((value.Name, value));

        public void Add((string, object) tuple) => members.Add((tuple.Item1, tuple.Item2));

        public void Add(string name, object value) =>  members.Add((name, value));

        IEnumerator IEnumerable.GetEnumerator() => members.GetEnumerator();

        IEnumerator<(string, object)> IEnumerable<(string, object)>.GetEnumerator() => members.GetEnumerator();
    }
}