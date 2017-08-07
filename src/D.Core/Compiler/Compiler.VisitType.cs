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

                genericParameters[i] = new Parameter(member.Name, scope.Get<Type>(member.Type ?? TypeSymbol.Any));

                // context.Add(member.Name, (Type)genericParameters[i].Type);
            }

            var properties = new Property[type.Members.Length];

            for (var i = 0; i < type.Members.Length; i++)
            {
                var member = type.Members[i];

                properties[i] = new Property(member.Name, scope.Get<Type>(member.Type), member.Flags);
            }

            var baseType = type.BaseType != null
                ? scope.Get<Type>(type.BaseType)
                : null;

            return new Type(type.Name, baseType, properties, genericParameters);
        }
    }
}
