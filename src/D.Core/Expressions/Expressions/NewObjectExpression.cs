using System;

namespace D.Expressions
{
    public class NewObjectExpression : IExpression
    {
        public NewObjectExpression(Symbol type, ObjectMember[] members)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }

        public Symbol Type { get; }

        public ObjectMember[] Members { get; }

        public ObjectMember this[int index] => Members[index];

        public int Count => Members.Length;

        Kind IObject.Kind => Kind.NewObjectExpression; 
    }

    // { a: 1, b: 2 }
    // { a, b, c }

    public struct ObjectMember
    {
        public ObjectMember(Symbol auto)
        {
            Name = auto;
            Value = auto;
            Implict = true;
        }

        public ObjectMember(Symbol name, IExpression value)
        {
            Name = name;
            Value = value;
            Implict = false;
        }

        public Symbol Name { get; }

        public bool Implict { get; }

        public IExpression Value { get; }
    }

    // // Point { x: 1, y: 2 }
    // Rust Notes: There is exactly one way to create an instance of a user-defined type: name it, and initialize all its fields at once:
}