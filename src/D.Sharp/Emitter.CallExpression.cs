using System.Collections.Generic;

namespace E.Compilation;

using Expressions;

public partial class CSharpEmitter
{
    // Fully qualify
    private static readonly Dictionary<string, string> funcMap = new (12) {
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
            Emit('(');
                
            // if piped?

            Visit(call.Callee);

            foreach (var arg in call.Arguments)
            {
                Emit(", ");

                Visit((IObject)arg.Value);
            }

            Emit(')');
        }
        else
        {
            WriteArguments(null, call.Arguments);
        }

        return call;
    }

    private static string GetFunctionName(string name)
    {
        if (funcMap.TryGetValue(name, out string? result))
        {
            return result;
        }

        return name;
    }

    public void WriteCall(string name, IObject x, IObject y)
    {
        Emit(name);
        Emit('(');
        WriteArg(x);
        Emit(", ");
        WriteArg(y);
        Emit(')');
    }

    public void WriteArg(IObject arg)
    {
        if (arg is BinaryExpression b)
        {
            VisitBinary(b, true);
        }
        else
        {
            Visit(arg);
        }
    }

    public void WriteArguments(IExpression? @this, IArguments args)
    {
        var i = 0;

        Emit('(');

        if (@this is not null)
        {
            Visit(@this);

            i++;
        }

        foreach (var arg in args)
        {
            if (++i > 1) Emit(", ");

            WriteArg((IObject)arg.Value);
        }

        Emit(')');
    }
}