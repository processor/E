namespace E.Compilation;

using System.Collections.Frozen;

using Expressions;

public partial class CSharpEmitter
{
    // Fully qualify
    private static readonly FrozenDictionary<string, string> s_funcMap = new KeyValuePair<string,string>[] {
        new("abs"     , "Math.Abs"),
        new("acos"    , "Math.Acos"),
        new("asin"    , "Math.Asin"),
        new("atan"    , "Math.Atan"),
        new("ceiling" , "Math.Ceiling"),
        new("cos"     , "Math.Cos"),
        new("cosh"    , "Math.Cosh"),
        new("min"     , "Math.Min"),
        new("max"     , "Math.Max"),
        new("floor"   , "Math.Floor"),
        new("sin"     , "Math.Sin"),
        new("sqrt"    , "Math.Sqrt")
    }.ToFrozenDictionary();

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
        if (s_funcMap.TryGetValue(name, out string? result))
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