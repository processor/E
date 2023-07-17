namespace E;

public readonly struct Annotation(string name, IObject value)
{
    public string Name { get; } = name;

    public IObject Value { get; } = value;
}