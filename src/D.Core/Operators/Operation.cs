namespace E;

public readonly struct Operation(string name) : IObject
{
    public string Name { get; } = name;

    readonly ObjectType IObject.Kind => ObjectType.Operation;
}

// provide
// own          | have (something) as one's own
// bill 
// read
// write

// ? May | Must | Will | Is

// AKA action