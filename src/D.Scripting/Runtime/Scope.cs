﻿using System.Collections.Generic;

namespace D
{
    public class Scope
    {
        private readonly Dictionary<string, object> items = new Dictionary<string, object>();

        private readonly Scope? parent;
        
        public Scope(Scope? parent = null)
        {
            this.parent = parent;
        }

        public object? This { get; set; } // Single arg passed to the function, or current arg in flow

        public void Set(string name, object value)
        {
            // Figure out if the property is mutable

            items[name] = value; 
        }

        public object Get(string name)
        {
            if (!items.TryGetValue(name, out object variable) && parent != null)
            {
                variable = parent.Get(name); // check parent
            }

            return variable!;
        }
    }
}
