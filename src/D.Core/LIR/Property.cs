using System;

namespace D
{
    public class Property : IObject
    {
        public Property(string name, Type type, ObjectFlags flags = ObjectFlags.None)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Flags = flags;
        }

        public string Name { get; }

        // String
        // String | Number
        // A & B
        public Type Type { get; }

        // mutable
        public ObjectFlags Flags { get; }

        // IsComputed ?

        // Getter
        // Setter

        public bool IsMutable => Flags.HasFlag(ObjectFlags.Mutable);

        Kind IObject.Kind => Kind.Property;

        #region ITypeMember

        public Type DeclaringType { get; set; }

        #endregion
    }
}
