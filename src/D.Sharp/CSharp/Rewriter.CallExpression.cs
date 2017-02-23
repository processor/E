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



        // a |> b |> c

        // a |> b(100) |> c(100, 18)

        // c(b(a, 100), 100, 18))

        public override IExpression VisitCall(CallExpression call)
        {
            Emit(GetFunctionName(call.FunctionName));


            if (call.IsPiped)
            {
                Emit("(");
                
                // if piped?

                Visit(call.Callee);

                foreach (var arg in call.Arguments)
                {
                    Emit(", ");

                    Visit(arg.Value);
                }

                writer.Write(")");
            }
            else
            {
                WriteArguments(null, call.Arguments);
            }

            return call;
        }


        private string GetFunctionName(string name)
        {
            if (functionMap.TryGetValue(name, out string result))
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
