namespace E.Expressions
{
    // let (a, b, c) = point

    public sealed class DestructuringAssignment : IExpression
    {
        public DestructuringAssignment(AssignmentElement[] elements, IExpression expression)
        {
            Variables = elements;
            Expression = expression;
        }
        
        public AssignmentElement[] Variables { get; }

        public IExpression Expression { get; }

        ObjectType IObject.Kind => ObjectType.DestructuringAssignment;
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
            Name = name;
            Type = type;
        }

        public string Name { get; }

        public Type Type { get; }
    }
}