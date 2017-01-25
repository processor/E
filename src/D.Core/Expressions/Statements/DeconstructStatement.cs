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

    public struct AssignmentElement
    {
        public AssignmentElement(string name, IType type)
        {
            #region Preconditions

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            #endregion

            Name = name;
            Type = type;
        }

        public string Name { get; }

        public IType Type { get; }
    }
}