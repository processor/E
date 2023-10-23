﻿using System;
using System.Collections.Generic;
using System.Text;

using E.Inference;
using E.Symbols;

namespace E.Syntax;

public sealed class FunctionDeclarationSyntax : IMemberSyntax, ISyntaxNode
{
    public FunctionDeclarationSyntax(
        ParameterSyntax[] parameters,
        ISyntaxNode body,
        ObjectFlags flags = default)
        : this(parameters, body, null, flags) { }

    public FunctionDeclarationSyntax(
        ParameterSyntax[] parameters,
        ISyntaxNode body,
        Symbol? returnType,
        ObjectFlags flags = default)
    {
        Parameters        = parameters;
        GenericParameters = [];
        Body              = body;
        ReturnType        = returnType;
        Flags             = flags;
    }

    public FunctionDeclarationSyntax(
        Symbol name, 
        ParameterSyntax[] genericParameters,
        ParameterSyntax[] parameters,
        Symbol? returnType,
        ISyntaxNode? body,
        ObjectFlags flags = default)
    {
        Name              = name;
        GenericParameters = genericParameters;
        Parameters        = parameters;
        ReturnType        = returnType;
        Body              = body;
        Flags             = flags;
    }

    public Symbol? Name { get; }

    public ParameterSyntax[] GenericParameters { get; }

    public ParameterSyntax[] Parameters { get; }

    public Symbol? ReturnType { get; }

    // Class Or Interface

    public ISyntaxNode? DeclaringType { get; internal set; }

    // Block or lambda
    public ISyntaxNode? Body { get; }

    public override string ToString()
    {
        var writer = new ValueStringBuilder(128);

        WriteTo(ref writer);

        return writer.ToString();
    }

    internal void WriteTo(ref ValueStringBuilder writer)
    {
        writer.Append("ƒ(");

        foreach (var parameter in Parameters)
        {
            parameter.Type?.WriteTo(ref writer);
        }

        writer.Append(')');

        if (Body is not null)
        {
            writer.Append(Body.ToString());
            writer.Append('\n');
        }
    }

    #region Flags

    public ObjectFlags Flags { get; }

    // Mutating?

    public bool IsStatic => IsOperator || !Flags.HasFlag(ObjectFlags.Instance);

    public bool IsLambda => Flags.HasFlag(ObjectFlags.Lambda);

    public bool IsAbstract    => Flags.HasFlag(ObjectFlags.Abstract);
    public bool IsOperator    => Flags.HasFlag(ObjectFlags.Operator);
    public bool IsAnonymous   => Flags.HasFlag(ObjectFlags.Anonymous);
    public bool IsInitializer => Flags.HasFlag(ObjectFlags.Initializer);
    public bool IsProperty    => Flags.HasFlag(ObjectFlags.Property);
    public bool IsIndexer     => Flags.HasFlag(ObjectFlags.Indexer);
    public bool IsConverter   => Flags.HasFlag(ObjectFlags.Converter);

    #endregion

    #region Helpers

    TypeSymbol IMemberSyntax.Type => new (KnownTypeNames.Function, GetParameterTypeSymbols(this.Parameters));

    private static Symbol[] GetParameterTypeSymbols(ReadOnlySpan<ParameterSyntax> parameters)
    {
        var typeSymbols = new Symbol[parameters.Length];

        for (var i = 0; i < parameters.Length; i++)
        {
            typeSymbols[i] = parameters[i].Type;
        }

        return typeSymbols;
    }


    #endregion

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.FunctionDeclaration;
}

// a + 5    =  a → a + 5
// a + 5^2  =  a → a^2

// →
// f: X → Y 
// f(x) = 1/x

// ƒ(x, y) = 5x4

// ƒ(x, y) = x·y