﻿namespace E;

public sealed class Null : IObject
{
    public static readonly Null Instance = new();

    private Null() { }

    ObjectType IObject.Kind => ObjectType.Null;

    public override string ToString() => "[none]";
}