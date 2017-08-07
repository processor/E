using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace D
{
    public class Type : INamedObject, IEquatable<Type>
    {
        private static readonly ConcurrentDictionary<Kind, Type> cache = new ConcurrentDictionary<Kind, Type>();

        private static long id = 10000000;

        public Type(string name)
        {
            Id = Interlocked.Increment(ref id);
            Name = name;
        }

        public Type(Kind kind)
        {
            Id = (long)kind;
            Name = kind.ToString();

            if (kind != Kind.Object)
            {
                BaseType = Get(Kind.Object);
            }
        }

        public Type(Kind kind, params Type[] args)
        {
            Id        = (long)kind;
            Name      = kind.ToString();
            Arguments = args;
        }

        public Type(string @namespace, string name, Type[] args = null)
        {
            Id         = Interlocked.Increment(ref id);
            Namespace  = @namespace;
            Name       = name;
            Arguments  = args ?? Array.Empty<Type>();
        }

        public Type(string name, Type baseType, Property[] properties, Parameter[] genericParameters)
        {
            Id                = Interlocked.Increment(ref id);
            Name              = name;
            Arguments         = Array.Empty<Type>();
            BaseType          = baseType;
            Properties        = properties;
            GenericParameters = genericParameters;
        }
        
        public Type BaseType { get; } // aka constructor

        // Universal
        public long Id { get; set; }

        // e.g. physics
        public string Namespace { get; }

        // unique within domain
        public string Name { get; }

        public Type[] Arguments { get; }

        public Property[] Properties { get; }

        public Parameter[] GenericParameters { get; }

        // public Annotation[] Annotations { get; }

        public string FullName => ToString();

        Kind IObject.Kind => Kind.Type;

        // Implementations
        public List<ImplementationExpression> Implementations { get; } = new List<ImplementationExpression>();

        public static Type Get(Kind kind)
        {
            if (!cache.TryGetValue(kind, out Type type))
            {
                type = new Type(kind);

                cache[kind] = type;
            }

            return type;
        }

        #region ToString

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Namespace != null)
            {
                sb.Append(Namespace);
                sb.Append("::");
            }

            sb.Append(Name);

            if (Arguments != null && Arguments.Length > 0)
            {
                sb.Append("<");

                var i = 0;

                foreach (var arg in Arguments)
                {
                    if (++i > 1) sb.Append(",");

                    sb.Append(arg.ToString());
                }

                sb.Append(">");
            }

            return sb.ToString();
        }

        #endregion


        #region Equality

        public bool Equals(Type other)
        {
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        #endregion

    }
}