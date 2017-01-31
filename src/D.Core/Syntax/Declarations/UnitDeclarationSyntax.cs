using System;

namespace D.Syntax
{
    // K unit : Temperature @name("Kelvin") @SI = 1
    public class UnitDeclarationSyntax : SyntaxNode
    {
        public UnitDeclarationSyntax(Symbol name, Symbol baseType, AnnotationExpressionSyntax[] annotations, SyntaxNode expression)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BaseType = baseType;
            Annotations = annotations ?? throw new ArgumentNullException(nameof(annotations));
            Expression = expression;
        }

        public Symbol Name { get; }

        public Symbol BaseType { get; }

        public AnnotationExpressionSyntax[] Annotations { get; }

        public SyntaxNode Expression { get; }

        Kind IObject.Kind => Kind.UnitDeclaration;
    }
}