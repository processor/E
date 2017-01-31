using System.Collections.Generic;

namespace D
{
    // Root = global

    // Property defination.... immutable?

    // Stack?

    public class Scope
    {
        private readonly Dictionary<string, IObject> items = new Dictionary<string, IObject>();

        private readonly Scope parent;

        public Scope()
        {
        }

        public Scope(Scope parent)
        {
            this.parent = parent;
        }

        public IObject This { get; set; }           // Single arg passed to the function, or current arg in flow

        public void Set(string name, IObject value)
        {
            // Figure out if the property is mutable

            items[name] = value; 
        }

        public IObject Get(string name)
        {
            if (!items.TryGetValue(name, out IObject var) && parent != null)
            {
                var = parent.Get(name); // check parent
            }

            return var;

            // otherwise check parent
        }

        // Functions are first class variables
    }
}
