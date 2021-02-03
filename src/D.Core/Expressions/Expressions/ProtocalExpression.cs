using E.Symbols;

namespace E.Expressions
{
    public sealed class ProtocolExpression : IExpression, INamedObject
    {  
        public ProtocolExpression(Symbol name, FunctionExpression[] members)
        {
            Name = name;
            Members = members;
        }

        public Symbol Name { get; set; }

        public FunctionExpression[] Members { get; }

        // public IMessageDeclaration[] Channel { get; set; }

        ObjectType IObject.Kind => ObjectType.Protocol;

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