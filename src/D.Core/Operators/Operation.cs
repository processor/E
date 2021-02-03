namespace E
{
    public readonly struct Operation : IObject
    {
        public Operation(string name)
        {
            Name = name;
        }

        public string Name { get; }

        readonly ObjectType IObject.Kind => ObjectType.Operation;
    }

    // provide
    // own          | have (something) as one's own
    // bill 
    // read
    // write

    // ? May | Must | Will | Is
}

// AKA action
