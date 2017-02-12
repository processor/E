using System;
using System.Text;

namespace D.Syntax
{
    // |>  pipe
    // [ ] indexAccess
    // .   memberAccess
    // ()  invoke

    public class CallExpressionSyntax : SyntaxNode
    {
        public CallExpressionSyntax(SyntaxNode callee, Symbol name, ArgumentSyntax[] arguments)
        {
            Callee = callee;
            Name = name;
            Arguments = arguments;
        }

        // Nullable 
        public SyntaxNode Callee { get; }  // Piper
        
        public Symbol Name { get; }

        public ArgumentSyntax[] Arguments { get; }

        public bool IsPiped { get; set; }

        Kind IObject.Kind => Kind.CallExpression;
    }

    public class ArgumentSyntax : SyntaxNode
    {
        public ArgumentSyntax(SyntaxNode value)
        {
            Value = value;
        }

        public ArgumentSyntax(string name, SyntaxNode value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public SyntaxNode Value { get; }

        public Kind Kind => Kind.Argument;
    }

    // .member
    public class MemberAccessExpressionSyntax : SyntaxNode
    {
        public MemberAccessExpressionSyntax(SyntaxNode left, Symbol name)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public SyntaxNode Left { get; }

        // Property | Function
        public Symbol Name { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Left.ToString());

            sb.Append(".");
            sb.Append(Name);

            return sb.ToString();
        }

        Kind IObject.Kind => Kind.MemberAccessExpression;
    }

    // [index]
    public class IndexAccessExpressionSyntax : SyntaxNode
    {
        public IndexAccessExpressionSyntax(SyntaxNode left, ArgumentSyntax[] arguments)
        {
            Left = left;
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public SyntaxNode Left { get; }

        // [1]
        // [1, 2]
        public ArgumentSyntax[] Arguments { get; }

        Kind IObject.Kind => Kind.IndexAccessExpression;
    }
}