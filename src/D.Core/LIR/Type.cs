using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using D.Expressions;

namespace D
{
    public sealed class Type : INamedObject, IExpression, IEquatable<Type>
    {
        private static readonly ConcurrentDictionary<ObjectType, Type> cache = new ConcurrentDictionary<ObjectType, Type>();

        private static long id = 10000000;

        public Type(string name)
        {
            Id = Interlocked.Increment(ref id);
            Name = name;
        }

        public Type(ObjectType kind)
        {
            Id = (long)kind;
            Name = kind.ToString();

            if (kind != ObjectType.Object)
            {
                BaseType = Get(ObjectType.Object);
            }
        }

        public Type(ObjectType kind, params Type[] args)
        {
            Id        = (long)kind;
            Name      = kind.ToString();
            Arguments = args;
        }

        public Type(string? @namespace, string name, Type[]? args = null)
        {
            Id         = Interlocked.Increment(ref id);
            Namespace  = @namespace;
            Name       = name;
            Arguments  = args ?? Array.Empty<Type>();
        }

        public Type(
            string name,
            Type? baseType,
            Property[]? properties, 
            Parameter[]? genericParameters,
            TypeFlags flags = default)
        {
            Id                = Interlocked.Increment(ref id);
            Name              = name;
            Arguments         = Array.Empty<Type>();
            BaseType          = baseType;
            Properties        = properties;
            GenericParameters = genericParameters;
            Flags             = flags;
        }
        
        public Type? BaseType { get; } // aka constructor

        // Universal
        public long Id { get; set; }

        // e.g. physics
        public string? Namespace { get; }

        // unique within domain
        public string Name { get; }

        public Type[]? Arguments { get; }

        public Property[]? Properties { get; }

        public Parameter[]? GenericParameters { get; }

        // public Annotation[] Annotations { get; }

        public TypeFlags Flags { get; }

        public string FullName => ToString();
        
        ObjectType IObject.Kind => ObjectType.Type;

        // Implementations
        public List<ImplementationExpression> Implementations { get; } = new List<ImplementationExpression>();

        public Property? GetProperty(string name)
        {
            if (Properties is null) return null;

            foreach (Property property in Properties)
            {
                if (property.Name == name) return property;

            }

            return null;
        }

        public static Type Get(ObjectType kind)
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
                sb.Append('<');

                var i = 0;

                foreach (var arg in Arguments)
                {
                    if (++i > 1) sb.Append(',');

                    sb.Append(arg.ToString());
                }

                sb.Append('>');
            }

            return sb.ToString();
        }

        #endregion

        public bool Equals(Type other) => this.Id == other.Id;
        
        public override int GetHashCode() => id.GetHashCode();
    }
}