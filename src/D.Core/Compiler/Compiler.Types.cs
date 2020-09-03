using System.Collections.Generic;

using D.Symbols;
using D.Syntax;

namespace D
{
    public partial class Compiler
    {
        public Type VisitTypeDeclaration(TypeDeclarationSyntax syntax)
        {
            var genericParameters = new Parameter[syntax.GenericParameters.Length];

            for (var i = 0; i < syntax.GenericParameters.Length; i++)
            {
                var member = syntax.GenericParameters[i];

                genericParameters[i] = new Parameter(member.Name, env.Get<Type>(member.Type ?? TypeSymbol.Object));

                // context.Add(member.Name, (Type)genericParameters[i].Type);
            }

            var properties = new List<Property>();

            foreach (var member in syntax.Members)
            {
                if (member is PropertyDeclarationSyntax property)
                {
                    properties.Add(new Property(property.Name, env.Get<Type>(property.Type), property.Flags));
                }
                else if (member is CompoundPropertyDeclaration compound)
                {
                    foreach (var p2 in compound.Members)
                    {
                        var memberType = p2.Type != null ? env.Get<Type>(p2.Type) : GetType(Visit(p2.Value));

                        properties.Add(new Property(p2.Name, memberType, p2.Flags));
                    }
                }
            }

            var baseType = syntax.BaseType != null
                ? env.Get<Type>(syntax.BaseType)
                : null;

            var type = new Type(syntax.Name, baseType, properties.ToArray(), genericParameters, syntax.Flags);

            env.Add(syntax.Name, type);

            flow.Add(type);

            return type;
        }
    }
}
