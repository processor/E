﻿using E.Symbols;

namespace E.Expressions;

public sealed class CompoundTypeDeclaration : TypeDeclarationBase
{
    public CompoundTypeDeclaration(Symbol[] names, TypeFlags flags, Symbol baseType, Property[] properties)
         : base(baseType, properties, flags)
    {
        Names = names;
    }

    public Symbol[] Names { get; }
}    