/*
    Hindley-Milner type inference ( https://en.wikipedia.org/wiki/Hindley%E2%80%93Milner_type_system )

    Cyril Jandia (08/2016)

    Public Domain.

    NO WARRANTY EXPRESSED OR IMPLIED.  USE AT YOUR OWN RISK.
 */

// REF: https://brianmckenna.org/files/js-type-inference/docs/typeinference.html


// NOTES....
// Hindley–Milner type system

// Types are classified as either monotypes or polytypes.
// Monotypes are further classified as either type constants (like int or string) or type variables (like α and β).
// Type constants can either be concrete types (like int and string) or type constructors (like Map and Set).
// Type variables (like α and β) behave as placeholders for concrete types (like int and string).

// Types themselves have types. Formally types of types are called kinds (i.e. there are different kinds of types).
// Concrete types (like int and string) and type variables (like α and β) are of kind *.
// Type constructors (like Map and Set) are lambda abstractions of types (e.g. Set is of kind * -> * and Map is of kind * -> * -> *).

// A monotype always designates a particular type, in the sense that it is equal only to itself and different from all others.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D.Inference
{
    public abstract class Node
    {
        public override string ToString() => Id;

        public abstract IType Infer(ITypeSystem system, IDictionary<string, IType> env, IList<IType> types);

        public Node(string id = null, Node[] args = null, Node body = null, object type = null)
        {
            Id = id;
            Args = args;
            Body = body;
            Type = type;
        }

        public string Id { get; }

        public Node[] Args { get; }

        public Node Body { get; }

        public object Type { get; }
    }

    public class Constant : Node
    {
        public Constant(string type)
            : base(type: type) { }

        public Constant(IType type)
            : base(type: type) { }

        public override IType Infer(ITypeSystem system, IDictionary<string, IType> env, IList<IType> types)
        {
            if (Type is IType)
            {
                return (IType)Type;
            }

            return system.Constant(env, (string)Type);
        }
    }

    public class Variable : Node
    {
        public Variable(string name)
            : base(id: name) { }

        public Variable(string name, object type)
            : base(id: name, type: type) { }

        public override IType Infer(ITypeSystem system, IDictionary<string, IType> env, IList<IType> types)
        {
            if (!env.ContainsKey(Id))
            {
                throw new InvalidOperationException($"undefined {Id}");
            }

            return system.Fresh(env[Id], types.ToArray());
        }
    }

    public class Call : Node
    {
        public Call(string id, Node[] args)
            : base(id: id, args: args) { }

        public Call(string id, Node[] args, object ctor)
            : base(id: id, args: args, type: ctor) { }
        
        public override IType Infer(ITypeSystem system, IDictionary<string, IType> env, IList<IType> types)
        {
            var args = Args.Select(arg => system.Infer(env, arg, types)).ToList();
            var name = Id;
            var type = system.Infer(env, new Variable(name), types);
            IType ctor = null;
            IType @out = null;

            if (Type != null)
            {
                ctor = !(Type is IType) ? system.Constant(env, (string)Type) : (IType)Type;
            }
            else
            {
                @out = system.NewGeneric();
                args.Add(@out);
            }
            system.Unify(system.NewType(ctor == null ? TypeSystem.Function : ctor, ctor == null ? TypeSystem.Function.Name : ctor.Name, args.ToArray()), type);
            return ctor == null ? @out : type;
        }
    }

    public class FuncNode : Node
    {
        public FuncNode(Node[] parameters, Node body)
         : base(args: parameters, body: body) { }

        public FuncNode(Node[] parameters, object type, Node body)
            : base(args: parameters, body: body, type: type) { }

        public override IType Infer(ITypeSystem system, IDictionary<string, IType> env, IList<IType> types)
        {
            var known = new List<IType>(types);
            var args = new List<IType>();

            foreach (var arg in Args)
            {
                IType var;
                if (arg.Type == null)
                {
                    var = system.NewGeneric();
                    known.Add(var);
                }
                else
                {
                    var = !(arg.Type is IType) ? system.Constant(env, (string)arg.Type) : (IType)arg.Type;
                }
                env[arg.Id] = var;
                args.Add(var);
            }

            args.Add(system.Infer(env, Body, known));

            if (Type != null)
            {
                system.Unify(args[args.Count - 1], !(Type is IType) ? system.Constant(env, (string)Type) : (IType)Type);
            }

            return system.NewType(TypeSystem.Function, TypeSystem.Function.Name, args.ToArray());
        }
    }

    public class Let : Node
    {
        public Let(string name, Node body)
            : base(id: name, body: body) { }

        public override IType Infer(ITypeSystem system, IDictionary<string, IType> env, IList<IType> types)
        {
            var scope = new Dictionary<string, IType>(env);

            var known = new List<IType>(types);

            var var = system.NewGeneric();

            scope[Id] = var;

            known.Add(var);

            system.Unify(var, system.Infer(scope, Body, known));

            return env[Id] = scope[Id];
        }
    }    
  
    public interface ITypeSystem
    {
        IType Fresh(IType t, IType[] types);

        IType Constant(IDictionary<string, IType> env, string ctor);

        IType NewGeneric();

        IType NewType(string id);

        IType NewType(string id, IType[] args);

        IType NewType(IType constructor);

        IType NewType(IType constructor, IType[] args);

        IType NewType(IType constructor, string id);

        IType NewType(IType constructor, string id, IType[] args);

        void Unify(IType t, IType s);

        IType Infer(IDictionary<string, IType> env, Node node);

        IType Infer(IDictionary<string, IType> env, Node node, IList<IType> types);

        IType[] Infer(IDictionary<string, IType> env, Node[] nodes);
    }

    public class TypeSystem : ITypeSystem
    {
        internal abstract class Scheme : IType
        {
            protected Scheme(string id) : this(id, null) { }

            protected Scheme(IType[] args) : this(null, args) { }

            protected Scheme(string id, IType[] args)
            {
                Name = id;
                Arguments = args ?? Array.Empty<IType>();
            }

            public long Id => 0;

            public override string ToString()
                => Name.ToString();

            public IType Constructor { get; protected set; }

            public virtual string Name { get; protected set; }

            public IType[] Arguments { get; private set; }

            public IType Instance { get; internal set; }
        }

        internal class Generic : Scheme
        {
            private string Alpha()
            {
                var id = Uid;
                var sb = new StringBuilder();

                while (id-- > 0)
                {
                    var r = id % 26;
                    var c = (char)(r + 97);
                    sb.Insert(0, c); id = (id - r) / 26;
                }

                return sb.ToString();
            }

            private string GetName() 
                => Instance != null ? Instance.Name : string.Concat('`', Alpha());

            internal Generic(TypeSystem system) 
                : base(null as string) {
                Uid = system.NewId();
            }

            internal readonly int Uid;

            public override string ToString() 
                => Instance != null ? Instance.ToString() : base.ToString();

            public override string Name
            {
                get { return GetName(); }

                protected set { }
            }
        }

        internal class Type : Scheme
        {
            internal Type(IType constructor, string id, IType[] args) : base(id, args)
            {
                Constructor = constructor ?? this;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                sb.Append(Name);

                if (Arguments.Length > 0)
                {
                    sb.Append("<");

                    sb.Append(
                        string.Concat(string.Join(", ", Arguments.Take(Arguments.Length - 1).Select(arg => arg.ToString())),
                    (Arguments.Length > 1 ? ", " : string.Empty),
                    Arguments[Arguments.Length - 1].ToString()));

                    sb.Append(">");
                }

                return sb.ToString();
            }
        }

        private static IType Create(string id)
            => Create(null, id);

        private static IType Create(IType constructor, string id) 
            => Create(constructor, id, null);

        private static IType Create(IType constructor, string id, IType[] args) 
            => new Type(constructor, id, args);

        private static IType Prune(IType type)
        {
            var var = type as Generic;

            if (var != null && var.Instance != null)
            {
                var.Instance = Prune(var.Instance);

                return var.Instance;
            }


            return type;
        }

        private static bool OccursIn(IType t, IType s)
            => ((s = Prune(s)) != t) ? (s is Type ? OccursIn(t, s.Arguments) : false) : true;

        private static bool OccursIn(IType t, IType[] types) 
            => types.Any(type => OccursIn(t, type));

        private IType Fresh(IType t, IType[] types, IDictionary<int, IType> vars)
        {
            vars = vars ?? new Dictionary<int, IType>();
            t = Prune(t);
            var var = t as Generic;
            var type = t as Type;
            if (var != null)
            {
                if (!OccursIn(t, types))
                {
                    if (!vars.ContainsKey(var.Uid))
                    {
                        vars[var.Uid] = NewGeneric();
                    }
                    return vars[var.Uid];
                }
                else
                {
                    return t;
                }
            }
            else if (type != null)
            {
                return NewType(type.Constructor, type.Name, type.Arguments.Select(arg => Fresh(arg, types, vars)).ToArray());
            }
            else
            {
                throw new InvalidOperationException(string.Concat("unsupported: ", t.GetType()));
            }
        }

        private int id;

        internal int NewId() => ++id;

        public static readonly IType Function = Create("Function");

        public static readonly ITypeSystem Default = Create();

        public static ITypeSystem Create() => new TypeSystem();

        public IType Fresh(IType t, IType[] types) => Fresh(t, types, null);

        public IType Constant(IDictionary<string, IType> env, string ctor)
        {
            if (!env.ContainsKey(ctor))
            {
                throw new InvalidOperationException("unknown: " + ctor);
            }

            return env[ctor];
        }

        public IType NewGeneric()
            => new Generic(this);

        public IType NewType(string id) => NewType(id, null);

        public IType NewType(string id, IType[] args) 
            => NewType(null, id, args);

        public IType NewType(IType constructor) 
            => NewType(constructor, null as IType[]);

        public IType NewType(IType constructor, IType[] args) 
            => NewType(constructor, constructor.Name, args);

        public IType NewType(IType constructor, string id) 
            => NewType(constructor, id, null);

        public IType NewType(IType constructor, string id, IType[] args)
            => Create(constructor, id, args);

        public void Unify(IType t, IType s)
        {
            t = Prune(t);
            s = Prune(s);
            if (t is Generic)
            {
                if (t != s)
                {
                    if (OccursIn(t, s))
                    {
                        throw new InvalidOperationException("recursive unification");
                    }

                    ((Generic)t).Instance = s;
                }
            }
            else if ((t is Type) && (s is Generic))
            {
                Unify(s, t);
            }
            else if ((t is Type) && (s is Type))
            {
                var t_type = (Type)t;
                var s_type = (Type)s;
                if ((t_type.Constructor.Name != s_type.Constructor.Name) || (t_type.Arguments.Length != s_type.Arguments.Length))
                {
                    throw new InvalidOperationException(string.Concat(t_type, " incompatible with ", s_type));
                }
                for (var i = 0; i < t_type.Arguments.Length; i++)
                {
                    Unify(t_type.Arguments[i], s_type.Arguments[i]);
                }
            }
            else
            {
                throw new InvalidOperationException("undecided unification");
            }
        }

        public IType Infer(IDictionary<string, IType> env, Node node) 
            => Infer(env, node, null);

        public IType Infer(IDictionary<string, IType> env, Node node, IList<IType> types) 
            => node.Infer(this, env, types ?? new List<IType>());

        public IType[] Infer(IDictionary<string, IType> env, Node[] nodes)
            => nodes.Select(node => Infer(env, node)).ToArray();
    }
}