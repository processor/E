// An implementation of Hindley Milner's Algorithm W
// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace D.Inference
{
    public static class TypeSystem
    {
        internal abstract class Scheme : IType
        {
            protected Scheme(string id)
            {
                if (this is Type && id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                Bind(Id = id);
            }

            protected Scheme(string id, IType[] args)
                : this(id)
            {
                Arguments = args ?? Array.Empty<IType>();
            }

            public override string ToString() => Id;

            public IType Bind(string name)
            {
                Name = Node.Var(name, this);

                return this;
            }
            public IType Constructor { get; protected set; }

            public virtual string Id { get; protected set; }

            public IType[] Arguments { get; private set; }

            public IType Self { get; internal set; }

            public VarNode Name { get; private set; }

            public IType Value => Self != null ? Self.Value : this;

            public bool IsConstructor => Constructor == this;
        }

        internal sealed class Generic : Scheme
        {
            private string alpha;

            private string Alpha()
            {
                int id = Uid;

                var sb = new StringBuilder();

                while (id-- > 0)
                {
                    var r = id % 26;
                    var c = (char)(r + 97);
                    sb.Insert(0, c);
                    id = (id - r) / 26;
                }

                return sb.ToString();
            }

            private string GetName()
            {
                return Self?.Id ?? string.Concat('`', alpha ?? (alpha = Alpha()));
            }

            internal Generic() 
                : base(null) { Uid = Interlocked.Increment(ref id); }

            internal readonly int Uid;

            public override string ToString() => Self != null ? Self.ToString() : base.ToString();
            public override string Id
            {
                get => GetName();

                protected set { }
            }
        }

        internal sealed class Type : Scheme
        {
            internal Type(IType constructor, string id, IType[] args)
                : base(id, args)
            {
                Constructor = constructor ?? this;
            }

            public override string ToString()
            {
                var id = Arguments.Length > 0 ? Id : base.ToString();

                if (Arguments.Length == 0) return id;

     

                return string.Format("{0}<{1}>", 
                    id, 
                    string.Concat(
                        string.Join(", ", Arguments.Take(Arguments.Length - 1)),
                        Arguments.Length > 1 ? ", " : string.Empty,
                        Arguments[Arguments.Length - 1].ToString())
                    );
            }
        }

        private static IType Prune(IType type)
        {
            return type is Generic var && var.Self != null 
                ? (var.Self = Prune(var.Self)) 
                : type;
        }

        private static bool OccursIn(IType t, IType s)
        {
            return ((s = Prune(s)) != t) ? (s is Type ? OccursIn(t, s.Arguments) : false) : true;
        }

        private static bool OccursIn(IType t, IReadOnlyList<IType> types)
        {
            foreach (var type in types)
            {
                if (OccursIn(t, type)) return true;
            }

            return false;
        }

        public static IType Fresh(IType t, IReadOnlyList<IType> types)
        {
            return Fresh(t, types, new Dictionary<int, IType>());
        }

        private static IType Fresh(IType t, IReadOnlyList<IType> types, Dictionary<int, IType> variables)
        {
            t = Prune(t);

            if (t is Generic var)
            {
                if (!OccursIn(t, types))
                {
                    if (!variables.ContainsKey(var.Uid))
                    {
                        variables[var.Uid] = NewGeneric();
                    }

                    return variables[var.Uid];
                }
                else
                {
                    return t;
                }
            }
            else if (t is Type type)
            {
                return NewType(type.Constructor, type.Id, type.Arguments.Select(arg => Fresh(arg, types, variables)).ToArray());
            }
            else
            {
                throw new InvalidOperationException($"unsupported {t.GetType()}");
            }
        }

        private static int id;

        public static readonly IType Function = new Type(null, "Func", null); 

        public static IType NewGeneric() => new Generic();

        public static IType NewType(string id, IType[] args) => new Type(null, id, args);

        public static IType NewType(IType constructor, IType[] args) => new Type(constructor, constructor.Id, args);

        public static IType NewType(IType constructor, string id, IType[] args) => new Type(constructor, id, args);

        public static void Unify(IType t, IType s)
        {
            t = Prune(t);
            s = Prune(s);

            if (t is Generic tGeneric)
            {
                if (t != s)
                {
                    if (OccursIn(t, s))
                    {
                        throw new InvalidOperationException($"recursive unification of {t} in {s}");
                    }

                    tGeneric.Self = s;
                }
            }
            else if (t is Type && s is Generic)
            {
                Unify(s, t);
            }
            else if (t is Type t_type && s is Type s_type)
            {
                if (t_type.Constructor.Id != s_type.Constructor.Id || t_type.Arguments.Length != s_type.Arguments.Length)
                {
                    throw new InvalidOperationException($"{t_type} incompatible with {s_type}");
                }

                for (var i = 0; i < t_type.Arguments.Length; i++)
                {
                    Unify(t_type.Arguments[i], s_type.Arguments[i]);
                }
            }
            else
            {
                throw new InvalidOperationException($"undecided unification for {t} and {s}");
            }
        }

        public static IType Infer(Environment env, Node node) => Infer(env, node, Array.Empty<IType>());

        public static IType Infer(Environment env, Node node, IReadOnlyList<IType> types)
        {
            return node.Infer(env, types);
        }
    }
}