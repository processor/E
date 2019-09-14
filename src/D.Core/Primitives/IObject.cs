namespace D
{
    public interface IObject
    {
        ObjectType Kind { get; }
    }

    public interface INamedObject : IObject
    {
        string Name { get; }
    }
}