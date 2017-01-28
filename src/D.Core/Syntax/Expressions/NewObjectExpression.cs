namespace D.Syntax
{
    public class NewObjectExpressionSyntax : SyntaxNode
    {
        public NewObjectExpressionSyntax(Symbol type, RecordMemberSyntax[] members)
        {
            Type = type;
            Members = members;
        }

        public Symbol Type { get; }

        public RecordMemberSyntax[] Members { get; }

        public RecordMemberSyntax this[int index] => Members[index];

        public int Count => Members.Length;

        Kind IObject.Kind => Kind.NewObjectExpression; 
    }

    // { a: 1, b: 2 }
    // { a, b, c }

    public struct RecordMemberSyntax
    {
        public RecordMemberSyntax(Symbol auto)
        {
            Name = auto;
            Value = auto;
            Implict = true;
        }

        public RecordMemberSyntax(Symbol name, SyntaxNode value)
        {
            Name = name;
            Value = value;
            Implict = false;
        }

        public Symbol Name { get; }

        public bool Implict { get; }

        public SyntaxNode Value { get; }
    }

    // Point { x: 1, y: 2 }
    // Rust Notes: There is exactly one way to create an instance of a user-defined type: name it, and initialize all its fields at once:
}