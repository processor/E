// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;
using System.Collections.Generic;

namespace D.Inference
{
    public class Environment : Dictionary<string, IType>
    {
        private readonly Environment parent;

        public Environment(Environment parent = null)
        {
            this.parent = parent;
        }

        private IType Get(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            if (TryGetValue(id, out IType value))
            {
                return value;
            }
            else if (parent != null && parent.TryGetValue(id, out value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException(id + " was not found");
            }
        }

        private void Set(string id, IType type)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            base[id] = type;
        }

        public Environment Nested() => new Environment(this);

        public new IType this[string id]
        {
            get => Get(id);
            set => Set(id, value);
        }
    }
}