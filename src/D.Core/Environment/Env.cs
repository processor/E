using System;
using System.Collections.Concurrent;

namespace D
{
    public class Env
    {
        private readonly ConcurrentDictionary<string, IObject> objects = new ConcurrentDictionary<string, IObject>();

        private readonly OperatorCollection operators = new OperatorCollection();

        private readonly Env parent;

        public Env(Env parent = null)
        {
            this.parent = parent;

            operators.Add(Operator.DefaultList);

            AddModule(new Modules.Primitives());
        }

        public Env(params IModule[] modules)
            : this()
        {
            foreach (var module in modules)
            {
                AddModule(module);
            }
        }

        public void AddModule(IModule module)
        {
            foreach (var member in module)
            {
                objects.TryAdd(member.Key, member.Value);

                if (member.Value is Operator)
                {
                    operators.Add((Operator)member.Value);
                }
            }
        }

        public OperatorCollection Operators => operators;

        public void Add(string name, IObject value)
            => objects.TryAdd(name, value);

        public bool TryGet<T>(string name, out T value)
        {
            if (!TryGet(name, out IObject r))
            {
                value = default(T);

                return false;
            }

            value = (T)r;

            return true;
        }

        public bool TryGet(string name, out IObject kind)
        {
            if (objects.TryGetValue(name, out kind))
            {
                return true;
            }
            else if (parent != null && parent.TryGet(name, out kind))
            {
                return true;
            }

            return false;
        }

        public bool TryGetType(Symbol symbol, out Type type)
        {
            if (TryGet(symbol.Name, out IObject t))
            {
                type = (Type)t;

                return true;
            }

            type = null;

            return false;
        }

        public Type GetType(Symbol symbol)
        {
            #region Preconditions

            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol));

            #endregion

            if (TryGetType(symbol, out Type type))
            {
                return type;
            }

            if (symbol.Arguments.Length > 0)
            {
                var args = new Type[symbol.Arguments.Length];

                for (var i = 0; i < args.Length; i++)
                {
                    args[i] = GetType(symbol.Arguments[i]);
                }

                return new Type(symbol.Module, symbol.Name, args);
            }
            else
            {
                return new Type(symbol.Module, symbol.Name);   
            }
        }

        public Env Parent => parent;

        public Env Nested()
        {
            return new Env(this);            
        }
    }
}
