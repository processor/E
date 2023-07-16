namespace E.Expressions;

// let (a, b, c) = point

public sealed class DestructuringAssignment(AssignmentElement[] elements, IExpression expression) : IExpression
{
    public AssignmentElement[] Variables { get; } = elements;

    public IExpression Expression { get; } = expression;

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

public readonly struct AssignmentElement(string name, Type type)
{
    public string Name { get; } = name;

    public Type Type { get; } = type;
}