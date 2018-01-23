using System;

namespace D.Expressions
{
    // let (a, b, c) = point

    public class DestructuringAssignment : IExpression
    {
        public DestructuringAssignment(AssignmentElement[] elements, IExpression expression)
        {
            Variables = elements;
            Expression = expression;
        }
        
        public AssignmentElement[] Variables { get; }

        public IExpression Expression { get; }

        Kind IObject.Kind => Kind.DestructuringAssignment;
    }

    /*
    // (a, b, c)
    public class ObjectAssignmentPattern
    {
    }

    // [ a, b, c ]
    public class ArrayAssignmentPattern
    {
    }
    */

    public readonly struct AssignmentElement
    {
        public AssignmentElement(string name, Type type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
        }

        public string Name { get; }

        public Type Type { get; }
    }
}