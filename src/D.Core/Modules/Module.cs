using System.Collections;
using System.Collections.Generic;

namespace D
{
    // A module may contain:
    // - types
    // - functions
    // - protocals
    // - constants

    public class Module : IModule
    {
        public readonly List<(string, IObject)> members = new List<(string, IObject)>();
        
        // TODO: Maintain seperate dictionary mapping names...

        // TODO: Imports
        // exports?

        public List<Symbol> Imports { get; } = new List<Symbol>();

        public Module() { }

        public Module(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void Add(INamedObject value) => 
            members.Add((value.Name, value));

        public void Add((string, IObject) tuple) =>
            Add(tuple.Item1, tuple.Item2);

        public void Add(string name, IObject value) => 
            members.Add((name, value));

        public List<(string, IObject)> Members => members;

        IEnumerator IEnumerable.GetEnumerator() => 
            members.GetEnumerator();

        IEnumerator<(string, IObject)> IEnumerable<(string, IObject)>.GetEnumerator() =>
            members.GetEnumerator();
    }

    
}
