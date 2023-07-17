namespace E;

public abstract class Member(string name, Type type, ObjectFlags modifiers)
{
    public string Name { get; } = name;

    // String
    // String | Number
    // A & B
    public Type Type { get; } = type;

    // mutable
    public ObjectFlags Modifiers { get; } = modifiers;

    #region Helpers

    public bool IsMutable => Modifiers.HasFlag(ObjectFlags.Mutable);

    #endregion
}