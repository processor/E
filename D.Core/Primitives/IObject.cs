namespace D
{
    public interface IObject
    {
        Kind Kind { get; }
    }

    public interface INamedObject : IObject
    {
        string Name { get; }
    }
}