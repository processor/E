using System.Text;

namespace D.Expressions
{
    public class CallExpression : IExpression
    {
        public CallExpression(IExpression callee, Symbol functionName, IArguments arguments)
        {
            Callee = callee;
            FunctionName = functionName;
            Arguments = arguments;
        }

        // Nullable
        public IExpression Callee { get; }

        public Symbol FunctionName { get; }

        public IArguments Arguments { get; }

        Kind IObject.Kind => Kind.CallExpression;
    }

    
    // .member
    public class MemberAccessExpression : IExpression
    {
        public MemberAccessExpression(IExpression left, Symbol memberName)
        {
            Left = left;
            MemberName = memberName;
        }

        // Type: Array | Property
        public IExpression Left { get; }

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
    public class IndexAccessExpression : IExpression
    {
        public IndexAccessExpression(IExpression left, IArguments arguments)
        {
            Left = left;
            Arguments = arguments;
        }

        public IExpression Left { get; set; }

        // [1]
        // [1, 2]
        public IArguments Arguments { get; set; }

        Kind IObject.Kind => Kind.IndexAccessExpression;
    }
}