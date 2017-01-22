namespace D.Expressions
{
    // let (a, b, c) = point


    public class DestructuringAssignment : IExpression
    {
        public DestructuringAssignment(TypedValue[] elements, IExpression instance)
        {
            Variables = elements;
            Instance = instance;
        }

        public TypedValue[] Variables { get; }

        public IExpression Instance { get; }

        Kind IObject.Kind => Kind.DestructuringAssignment;
    }
}