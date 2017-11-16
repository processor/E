using System;

namespace D.Syntax
{
    // let (a, b, c) = point

    public class DestructuringAssignmentSyntax : ISyntaxNode
    {
        public DestructuringAssignmentSyntax(AssignmentElementSyntax[] elements, ISyntaxNode instance)
        {
            Variables = elements;
            Instance = instance;
        }

        public AssignmentElementSyntax[] Variables { get; }

        public ISyntaxNode Instance { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.DestructuringAssignment;
    }

    public struct AssignmentElementSyntax
    {
        public AssignmentElementSyntax(Symbol name, Symbol type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
        }

        public Symbol Name { get; }

        public Symbol Type { get; }
    }
}