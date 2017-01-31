using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;

namespace D
{
    public class Type : INamedObject, IType
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
        }

        public Type(Kind kind, params Type[] args)
        {
            Id = (long)kind;
            Name = kind.ToString();
            Arguments = args;
        }

        public Type(string @namespace, string name, IType[] args = null)
        {
            Id         = Interlocked.Increment(ref id);
            Namespace  = @namespace;
            Name       = name;
            Arguments  = args ?? Array.Empty<IType>();
        }

        public Type(string name, Type baseType, Property[] properties, Parameter[] genericParameters)
        {
            Id = Interlocked.Increment(ref id);
            Name = name;
            Arguments = Array.Empty<IType>();
            Constructor = baseType;
            Properties = properties;
            GenericParameters = genericParameters;
        }

        public IType Constructor { get; }

        public IType Instance { get; }

        // Universal
        public long Id { get; set; }

        // e.g. physics
        public string Namespace { get; }

        // unique within domain
        public string Name { get; }

        public IType[] Arguments { get; }

        public Property[] Properties { get; }

        public Parameter[] GenericParameters { get; }

        // public Annotation[] Annotations { get; }

        public string FullName => ToString();

        Kind IObject.Kind => Kind.Type;

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
    }

    public interface IType
    {
        // long Id { get; }

        string Name { get; }

        IType Constructor { get; }  // Parent?

        IType[] Arguments { get; }  // args, parameters, or properties

        IType Instance { get; }     // Used for inference
    }
}