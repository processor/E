namespace D
{
    public class Property : Member, IObject
    {
        public Property(string name, Type type, ObjectFlags modifiers = ObjectFlags.None)
            : base(name, type, modifiers) { }

        // IsComputed ?

        // Getter
        // Setter

        Kind IObject.Kind => Kind.Property;

        #region ITypeMember

        public Type DeclaringType { get; set; }

        #endregion
    }
}