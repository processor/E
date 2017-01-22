using System;

namespace D
{
    using Expressions;

    public class Implementation : IObject
    {
        public Implementation(Protocal protocal, Type type, Variable[] variables, Function[] members)
        {
            #region Preconditions

            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (members == null)
                throw new ArgumentNullException(nameof(members));

            #endregion

            Protocal = protocal;
            Type = type;
            Variables = variables;
            Methods = members;
        }

        public Type Type { get; }

        public Protocal Protocal { get; }

        public Variable[] Variables { get; }

        public Function[] Methods { get; }
        
        Kind IObject.Kind => Kind.Implementation;
    }
}