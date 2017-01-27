using System;

namespace D.Syntax
{
    // K unit : Temperature @name("Kelvin") @SI = 1
    public class UnitDeclarationSyntax : ISyntax
    {
        public UnitDeclarationSyntax(Symbol name, Symbol baseType, AnnotationExpressionSyntax[] annotations, ISyntax expression)
        {
            #region Preconditions

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));

            #endregion

            Name = name;
            BaseType = baseType;
            Annotations = annotations;
            Expression = expression;
        }

        public Symbol Name { get; }

        public Symbol BaseType { get; }

        public AnnotationExpressionSyntax[] Annotations { get; }

        public ISyntax Expression { get; }

        Kind IObject.Kind => Kind.UnitDeclaration;
    }
}