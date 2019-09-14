namespace D
{
    public readonly struct Operation : IObject
    {
        public Operation(string name)
        {
            Name = name;
        }

        public string Name { get; }

        ObjectType IObject.Kind => ObjectType.Operation;
    }

    // provide
    // own          | have (something) as one's own
    // bill 
    // read
    // write

    // ? May | Must | Will | Is
}

// AKA action
