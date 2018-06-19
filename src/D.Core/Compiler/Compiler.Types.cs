using System.Collections.Generic;

namespace D
{
    using Syntax;

    public partial class Compiler
    {
        public Type VisitTypeDeclaration(TypeDeclarationSyntax type)
        {
            var genericParameters = new Parameter[type.GenericParameters.Length];

            for (var i = 0; i < type.GenericParameters.Length; i++)
            {
                var member = type.GenericParameters[i];

                genericParameters[i] = new Parameter(member.Name, env.Get<Type>(member.Type ?? TypeSymbol.Object));

                // context.Add(member.Name, (Type)genericParameters[i].Type);
            }

            var properties = new List<Property>();

            foreach (var member in type.Members)
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

            var baseType = type.BaseType != null
                ? env.Get<Type>(type.BaseType)
                : null;

            var result = new Type(type.Name, baseType, properties.ToArray(), genericParameters);

            env.Add(type.Name, result);

            return result;
        }
    }
}
