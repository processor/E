using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace D
{
    public class Node
    {
        private readonly ConcurrentDictionary<string, object> children = new ConcurrentDictionary<string, object>();

        private readonly OperatorCollection operators = new OperatorCollection();

        private readonly int depth = 0;

        public Node(string name = null, Node parent = null)
        {
            Name = name;
            Parent = parent;

            if (parent != null)
            {
                this.depth = parent.depth + 1;
            }

            operators.Add(Operator.DefaultList);

            AddModule(new Modules.Primitives());
        }

        public Node(params Module[] modules)
            : this()
        {
            foreach (var module in modules)
            {
                AddModule(module);
            }
        }

        public void AddModule(Module module)
        {
            foreach (var pair in module.Exports)
            {
                children.TryAdd(pair.Key, pair.Value);

                if (pair.Value is Operator op)
                {
                    operators.Add(op);
                }
            }
        }

        public string Name { get; }
        
        public Node Parent { get; }

        public OperatorCollection Operators => operators;

        public void Add(string name, object value)

        {
            if (!children.TryAdd(name, value))
            {
                // throw new Exception(name + " already added");
            }
        }

        public void Set<T>(string name, T value)
        {
            children[name] = value;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            if (!TryGet(key, out object r))
            {
                value = default;

                return false;
            }

            value = (T)r;

            return true;
        }

        public bool TryGet(string name, out object kind)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (children.TryGetValue(name, out kind))
            {
                return true;
            }
            else if (Parent != null && Parent.TryGet(name, out kind))
            {
                return true;
            }

            return false;
        }

        private bool TryGetType(Symbol symbol, out Type type)
        {
            if (TryGet(symbol.Name, out object t))
            {
                type = (Type)t;

                return true;
            }

            type = null;

            return false;
        }

        public Type GetType(Symbol symbol)
        {
            if (symbol == null) throw new ArgumentNullException(nameof(symbol));

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

        public T Get<T>(string name)
        {
            if (!TryGetValue<T>(name, out T value))
            {
                throw new KeyNotFoundException($"Node does not contain {typeof(T).Name}");
            }

            return value;
        }

        public T Get<T>(Symbol symbol)
        {
            if (!TryGetValue<T>(symbol, out T value))
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
       
        public Node Nested(string name = null)
        {
            return new Node(name, this);
        }
    }
}