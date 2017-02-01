using System;

namespace D
{
    public class Property : IObject
    {
        public Property(string name, IType type, VariableFlags flags = VariableFlags.None)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Flags = flags;
        }

        public string Name { get; }

        // String
        // String | Number
        // A & B
        public IType Type { get; }

        public VariableFlags Flags { get; }

        public bool IsMutable => Flags.HasFlag(VariableFlags.Mutable);

        Kind IObject.Kind => Kind.Property;

        #region ITypeMember

        public IType DeclaringType { get; set; }

        #endregion
    }
}
