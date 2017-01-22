using System.Collections;
using System.Collections.Generic;

namespace D
{
    // Constants
    // Functions
    // Operators

    // TODO: Change to tuple w/ C# 7
    public interface IModule : IEnumerable<KeyValuePair<string, IObject>> { }

    public class Module : IModule
    {
        public readonly List<KeyValuePair<string, IObject>> members = new List<KeyValuePair<string, IObject>>();

        public void Add(INamedObject value)
        {
            members.Add(new KeyValuePair<string, IObject>(value.Name, value));
        }

        public void Add(string name, IObject value)
        {
            members.Add(new KeyValuePair<string, IObject>(name, value));
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => members.GetEnumerator();

        IEnumerator<KeyValuePair<string, IObject>> IEnumerable<KeyValuePair<string, IObject>>.GetEnumerator()
            => members.GetEnumerator();
    }
}
