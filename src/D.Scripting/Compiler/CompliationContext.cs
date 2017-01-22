using System.Collections.Concurrent;

namespace D.Compilation
{
    public class CompliationContext
    {
        // TODO: Use a Trie to lookup Functions w/ different signatures

        private readonly ConcurrentDictionary<string, IObject> objects = new ConcurrentDictionary<string, IObject>();

        private readonly CompliationContext parent;

        public CompliationContext(CompliationContext parent = null)
        {
            this.parent = parent;

            foreach(var primitive in new Modules.Primitives())
            {
                objects.TryAdd(primitive.Key, primitive.Value);
            }
        }

        public CompliationContext Parent => parent;

        public bool Add(string name, IObject value)
        {
            return objects.TryAdd(name, value);
        }

        private bool _TryGet(Symbol symbol, out IObject value)
        {
            if (objects.TryGetValue(symbol.Name, out value))
            {
                return true;
            }
            else if (parent != null && parent.TryGet(symbol, out value))
            {
                return true;
            }

            return false;
        }

        public bool TryGet<T>(Symbol symbol, out T value)
        {
            IObject v;

            if (_TryGet(symbol, out v))
            {
                value = (T)v;

                return true;
            }

            value = default(T);

            return false;
        }

        public T Get<T>(Symbol symbol)
        {
            T value;

            if (!TryGet<T>(symbol, out value))
            {

                if (typeof(T) == typeof(Type))
                {
                    return (T)(object)new Env().GetType(symbol);
                }

                throw new System.Exception($"context does not contain {typeof(T).Name} '{symbol.Name}'");
            }

            return value;
        }

        public T Get<T>(Symbol symbol, Argument[] args)
        {
            return default(T);
        }

        public T[] GetAll<T>(Symbol symbol)
        {
            return null;
        }

        public CompliationContext Nested()
        {
            return new CompliationContext(this);
        }
    }
}
