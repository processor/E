using System;
using System.Collections.Concurrent;

namespace D
{
    public class Node : INode
    {
        private readonly ConcurrentDictionary<string, IObject> nodes = new ConcurrentDictionary<string, IObject>();

        // We need to consider arguments in the names...

        private readonly OperatorCollection operators = new OperatorCollection();

        private readonly Node parent;
        private readonly string name;
        
        public Node(string name = null, Node parent = null)
        {
            this.name   = name;
            this.parent = parent;

            operators.Add(Operator.DefaultList);

            AddModule(new Modules.Primitives());
        }

        public Node(params IModule[] modules)
            : this()
        {
            foreach (var module in modules)
            {
                AddModule(module);
            }
        }

        public void AddModule(IModule module)
        {
            foreach (var (key, value) in module)
            {
                nodes.TryAdd(key, value);

                if (value is Operator op)
                {
                    operators.Add(op);
                }
            }
        }

        public string Name => name;
        
        public OperatorCollection Operators => operators;

        public void Add(string name, IObject value)
            => nodes.TryAdd(name, value);

        public bool TryGet<T>(string name, out T value)
        {
            if (!TryGet(name, out IObject r))
            {
                value = default;

                return false;
            }

            value = (T)r;

            return true;
        }

        public bool TryGet(string name, out IObject kind)
        {
            if (nodes.TryGetValue(name, out kind))
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


        public T Get<T>(Symbol symbol)
        {
            if (!TryGet<T>(symbol, out T value))
            {
                if (typeof(T) == typeof(Type))
                {
                    return (T)(object)new Node().GetType(symbol);
                }

                throw new Exception($"context does not contain {typeof(T).Name} '{symbol.Name}'");
            }

            return value;
        }

        public T Get<T>(Symbol symbol, Argument[] args)
        {
            // Find by match...

            throw new NotImplementedException();
        }
        
        public Node Parent => parent;
        
        public Node Nested(string name)
        {
            return new Node(name, this);            
        }
    }
}
