namespace D
{
    using System.Collections.Generic;
    using Syntax;

    public partial class Compiler
    {
        public Type VisitTypeDeclaration(TypeDeclarationSyntax type)
        {
            var genericParameters = new Parameter[type.GenericParameters.Length];

            for (var i = 0; i < type.GenericParameters.Length; i++)
            {
                var member = type.GenericParameters[i];

                genericParameters[i] = new Parameter(member.Name, scope.Get<Type>(member.Type ?? TypeSymbol.Any));

                // context.Add(member.Name, (Type)genericParameters[i].Type);
            }

            var properties = new List<Property>();

            foreach(var member in type.Members)
            {
                if (member is PropertyDeclarationSyntax property)
                {
                    properties.Add(new Property(property.Name, scope.Get<Type>(property.Type), property.Flags));
                }
                else if (member is CompoundPropertyDeclaration compound) 
                {
                    foreach (var p2 in compound.Members)
                    {
                        properties.Add(new Property(p2.Name, scope.Get<Type>(p2.Type), p2.Flags));
                    }
                }
            }

            var baseType = type.BaseType != null
                ? scope.Get<Type>(type.BaseType)
                : null;

            return new Type(type.Name, baseType, properties.ToArray(), genericParameters);
        }
    }
}
