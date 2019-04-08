using System;

namespace D.Expressions
{
    public class ProtocolExpression : IExpression, INamedObject
    {  
        public ProtocolExpression(Symbol name, FunctionExpression[] members)
        {
            Name    = name    ?? throw new ArgumentNullException(nameof(name));
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }

        public Symbol Name { get; set; }

        public FunctionExpression[] Members { get; }

        // public IMessageDeclaration[] Channel { get; set; }

        Kind IObject.Kind => Kind.Protocol;

        string INamedObject.Name => Name;

        #region Helpers

        public bool Contains(string name)
        {
            foreach (var member in Members)
            {
                if (member.Name == name) return true;
            }

            return false;
        }

        #endregion
    }
}