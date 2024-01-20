namespace E;

public sealed class Property(
    string name,
    Type type,
    ObjectFlags modifiers = ObjectFlags.None) : Member(name, type, modifiers), IObject
{
    // IsComputed ?

    // Getter
    // Setter

    ObjectType IObject.Kind => ObjectType.Property;

    // ITypeMember

    public Type? DeclaringType { get; set; }
}