namespace D
{
    // AKA action

    public struct Verb : IObject
    {
        public Verb(string name)
        {
            Name = name;
        }

        public string Name { get; }

        Kind IObject.Kind => Kind.Verb;
    }

    // provide
    // own          | have (something) as one's own
    // bill 
    // read
    // write

    // ? May | Must | Will | Is
}
