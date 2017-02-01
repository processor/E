using System;
using System.Text;

namespace D.Syntax
{
    public class CallExpressionSyntax : SyntaxNode
    {
        public CallExpressionSyntax(SyntaxNode callee, Symbol functionName, ArgumentSyntax[] arguments)
        {
            Callee = callee;
            FunctionName = functionName;
            Arguments = arguments;
        }

        // Nullable 
        public SyntaxNode Callee { get; }  // Piper

        public Symbol FunctionName { get; }

        public ArgumentSyntax[] Arguments { get; }

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
        public MemberAccessExpressionSyntax(SyntaxNode left, Symbol memberName)
        {
            Left = left;
            MemberName = memberName;
        }

        // Type: Array | Property
        public SyntaxNode Left { get; }

        // Property | Function
        public Symbol MemberName { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Left.ToString());

            sb.Append(".");
            sb.Append(MemberName);

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