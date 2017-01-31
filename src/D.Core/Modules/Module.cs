using System.Collections;
using System.Collections.Generic;

namespace D
{
    // Constants
    // Functions
    // Operators

    public interface IModule : IEnumerable<(string, IObject)> { }

    public class Module : IModule
    {
        public readonly List<(string, IObject)> members = new List<(string, IObject)>();

        // TODO: Imports
        // exports?

        public List<Symbol> Imports { get; } = new List<Symbol>();

        public void Add(INamedObject value)
            => members.Add((value.Name, value));

        public void Add((string, IObject) tuple)
            => Add(tuple.Item1, tuple.Item2);

        public void Add(string name, IObject value)
            => members.Add((name, value));

        IEnumerator IEnumerable.GetEnumerator() 
            => members.GetEnumerator();

        IEnumerator<(string, IObject)> IEnumerable<(string, IObject)>.GetEnumerator()
            => members.GetEnumerator();
    }
}
