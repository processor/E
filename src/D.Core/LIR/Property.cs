using System;

namespace D
{
    public class Property : IObject
    {
        public Property(string name, IType type, bool isMutable = false)
        {
            #region Preconditions

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (type == null)
                throw new ArgumentNullException(nameof(type));

            #endregion

            Name = name;
            Type = type;
            IsMutable = isMutable;
        }

        public string Name { get; }

        public IType Type { get; }

        public bool IsMutable { get; }

        Kind IObject.Kind => Kind.Property;

        #region ITypeMember

        public IType DeclaringType { get; set; }

        #endregion
    }
}
