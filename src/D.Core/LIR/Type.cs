using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using E.Expressions;

namespace E;

public sealed class Type : INamedObject, IExpression, IEquatable<Type>
{
    private static long id = 10_000_000;

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

    public Type(string? moduleName, string name, Type[]? args = null)
    {
        Id         = Interlocked.Increment(ref id);
        ModuleName = moduleName;
        Name       = name;
        Arguments  = args ?? [];
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
        Arguments         = [];
        BaseType          = baseType;
        Properties        = properties;
        GenericParameters = genericParameters;
        Flags             = flags;
    }
        
    public Type? BaseType { get; } // aka constructor

    // Universal
    public long Id { get; set; }

    // e.g. physics
    public string? ModuleName { get; }

    // unique within a module / domain
    public string Name { get; }

    public Type[]? Arguments { get; }

    public Property[]? Properties { get; }

    public Parameter[]? GenericParameters { get; }

    // public Annotation[] Annotations { get; }

    public TypeFlags Flags { get; }

    public string FullName => ToString();
        
    ObjectType IObject.Kind => ObjectType.Type;

    // Implementations
    public List<ImplementationExpression> Implementations { get; } = new ();

    public Property? GetProperty(string name)
    {
        if (Properties is null) return null;

        foreach (Property property in Properties)
        {
            if (property.Name.Equals(name, StringComparison.Ordinal))
            {
                return property;
            }

        }

        return null;
    }

    private static readonly ConcurrentDictionary<ObjectType, Type> s_cache = new();

    public static Type Get(ObjectType kind)
    {
        if (!s_cache.TryGetValue(kind, out Type? type))
        {
            type = new Type(kind);

            s_cache[kind] = type;
        }

        return type;
    }

    #region ToString

    public override string ToString()
    {
        var sb = new ValueStringBuilder(128);

        WriteTo(ref sb);

        return sb.ToString();
    }

    internal void WriteTo(ref ValueStringBuilder sb)
    {
        if (ModuleName is not null)
        {
            sb.Append(ModuleName);
            sb.Append("::");
        }

        sb.Append(Name);

        if (Arguments is { Length: > 0 })
        {
            sb.Append('<');

            var i = 0;

            foreach (var arg in Arguments)
            {
                if (++i > 1) sb.Append(',');

                arg.WriteTo(ref sb);
            }

            sb.Append('>');
        }
    }

    #endregion

    public bool Equals(Type? other)
    {
        if (other is null) return this is null;
        
        return this.Id == other.Id;
    }

    public override int GetHashCode() => id.GetHashCode();
}