namespace D
{
    public sealed class Property : Member, IObject
    {
        public Property(string name, Type type, ObjectFlags modifiers = ObjectFlags.None)
            : base(name, type, modifiers) { }

        // IsComputed ?

        // Getter
        // Setter

        ObjectType IObject.Kind => ObjectType.Property;

        #region ITypeMember

        public Type? DeclaringType { get; set; }

        #endregion
    }
}