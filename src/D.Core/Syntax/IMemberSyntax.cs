﻿using E.Symbols;

namespace E.Syntax;

public interface IMemberSyntax
{
    Symbol? Name { get; }

    TypeSymbol Type { get; }

    // Parent
}

// geometry.curve.get_point(Number) -> Point
// geography.earth.eccentricity     -> Number

// Value

// string Key { get; }
