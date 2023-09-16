﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using E.Expressions;
using E.Symbols;

namespace E;

public class FunctionExpression : INamedObject, IExpression
{
    public FunctionExpression(
        string name, 
        Type returnType, 
        params Parameter[] parameters)
    {
        Name              = name;
        Parameters        = parameters;
        GenericParameters = [];
        ReturnType        = returnType;
    }

    public FunctionExpression(
       Parameter[] parameters,
       IExpression body,
       ObjectFlags flags = ObjectFlags.None)
        : this(parameters, body, null, flags) { }

    public FunctionExpression(
       Parameter[] parameters,
       IExpression body,
       Type returnType,
       ObjectFlags flags = ObjectFlags.None)
    {
        Parameters = parameters;
        GenericParameters = [];
        Body = body;
        ReturnType = returnType;
        Flags = flags;
    }

    public FunctionExpression(
        Symbol? name,
        Parameter[] genericParameters,
        Parameter[] parameters,
        Type returnType,
        IExpression? body,
        ObjectFlags flags = ObjectFlags.None)
    {
        Name = name;
        GenericParameters = genericParameters;
        Parameters = parameters;
        ReturnType = returnType;
        Body = body;
        Flags = flags;
    }
    
    public string? Name { get; }

    public Parameter[] Parameters { get; }

    public Parameter[] GenericParameters { get; }

    public Type ReturnType { get; }
    
    public override string ToString()
    {
        var writer = new ValueStringBuilder();

        WriteTo(ref writer);

        return writer.ToString();
    }

    public virtual IObject? Invoke(IArguments arguments)
    {
        return null;
    }

    #region Implementation

    public Type? DeclaringType { get; set; }

    // Block or lambda
    public IExpression? Body { get; set; }

    public ObjectFlags Flags { get; set; }

    #endregion

    #region Flags

    public bool IsStatic => IsOperator || !Flags.HasFlag(ObjectFlags.Instance);

    public bool IsAbstract    => (Flags & ObjectFlags.Abstract) != 0;
    
    public bool IsOperator    => (Flags & ObjectFlags.Operator) != 0;
    
    public bool IsAnonymous   => (Flags & ObjectFlags.Anonymous) != 0;

    public bool IsInitializer => (Flags & ObjectFlags.Initializer) != 0;

    [MemberNotNullWhen(true, nameof(Name), nameof(ReturnType))]
    public bool IsProperty    => (Flags & ObjectFlags.Property) != 0;

    public bool IsIndexer     => (Flags & ObjectFlags.Indexer) != 0;
    
    public bool IsConverter   => (Flags & ObjectFlags.Converter) != 0;

    public Visibility Visibility
    {
        get
        {
            if ((Flags & ObjectFlags.Private) != 0)
            {
                return Visibility.Private;
            }

            return Visibility.Public;
        }
    }

    #endregion

    #region INode

    // INode Parent { get; }
    
    #endregion

    internal void WriteTo(ref ValueStringBuilder sb)
    {
        sb.Append("ƒ(");

        foreach (var parameter in Parameters)
        {
            parameter.Type.WriteTo(ref sb);
        }

        sb.Append(')');

        if (Body is not null)
        {
            sb.Append(Body.ToString());
        }
    }

    ObjectType IObject.Kind => ObjectType.Function;
}


// a + 5    =  a → a + 5
// a + 5^2  =  a → a^2

// →
// f: X → Y 
// f(x) = 1/x

// ƒ(x, y) = 5x4

// ƒ(x, y) = x·y
