using System.Collections.Generic;

namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        // Fully qualify
        private static readonly Dictionary<string, string> functionMap = new Dictionary<string, string> {
            { "abs"     , "Math.Abs" },
            { "acos"    , "Math.Acos" },
            { "asin"    , "Math.Asin" },
            { "atan"    , "Math.Atan" },
            { "ceiling" , "Math.Ceiling" },
            { "cos"     , "Math.Cos" },
            { "cosh"    , "Math.Cosh" },
            { "min"     , "Math.Min" },
            { "max"     , "Math.Max" },
            { "floor"   , "Math.Floor" },
            { "sin"     , "Math.Sin" },
            { "sqrt"    , "Math.Sqrt" }
        };

        public override void VisitCall(CallExpression call)
        {
            Emit(GetFunctionName(call.FunctionName));

            WriteArguments(null, call.Arguments);
        }

        private string GetFunctionName(string name)
        {
            string result;

            if (functionMap.TryGetValue(name, out result))
            {
                return result;
            }

            return name;
        }

        public void WriteCall(string name, IObject x, IObject y)
        {
            Emit(name);
            Emit("(");
            WriteArg(x);
            Emit(", ");
            WriteArg(y);
            Emit(")");
        }

        public void WriteArg(IObject arg)
        {
            if (arg is BinaryExpression)
            {
                VisitBinary((BinaryExpression)arg, true);
            }
            else
            {
                Visit(arg);
            }
        }

        public void WriteArguments(IExpression _this, IArguments args)
        {
            var i = 0;

            Emit("(");

            if (_this != null)
            {
                Visit(_this);

                i++;
            }

            foreach (var arg in args)
            {
                if (++i > 1) Emit(", ");

                WriteArg(arg.Value);
            }

            Emit(")");
        }
    }
}
