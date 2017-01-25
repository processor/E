using System;
using System.Text;

namespace D.Syntax
{
    public class CallExpressionSyntax : ISyntax
    {
        public CallExpressionSyntax(ISyntax callee, Symbol functionName, ArgumentSyntax[] arguments)
        {
            Callee = callee;
            FunctionName = functionName;
            Arguments = arguments;
        }

        // Nullable 
        public ISyntax Callee { get; }  // Piper

        public Symbol FunctionName { get; }

        public ArgumentSyntax[] Arguments { get; }

        Kind IObject.Kind => Kind.CallExpression;
    }


    public class ArgumentSyntax : ISyntax
    {
        public ArgumentSyntax(ISyntax value)
        {
            Value = value;
        }

        public ArgumentSyntax(string name, ISyntax value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public ISyntax Value { get; }

        public Kind Kind => Kind.Argument;

    }

    // .member
    public class MemberAccessExpressionSyntax : ISyntax
    {
        public MemberAccessExpressionSyntax(ISyntax left, Symbol memberName)
        {
            Left = left;
            MemberName = memberName;
        }

        // Type: Array | Property
        public ISyntax Left { get; }

        // The member
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
    public class IndexAccessExpressionSyntax : ISyntax
    {
        public IndexAccessExpressionSyntax(ISyntax left, ArgumentSyntax[] arguments)
        {
            #region Preconditions

            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            #endregion

            Left = left;
            Arguments = arguments;
        }

        public ISyntax Left { get; set; }

        // [1]
        // [1, 2]
        public ArgumentSyntax[] Arguments { get; set; }

        Kind IObject.Kind => Kind.IndexAccessExpression;
    }
}