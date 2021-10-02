using System;

namespace E;

public partial class Compiler
{
    public NestedScope EnterScope(string name)
    {
        env = env.Nested(name);

        return new NestedScope(this);
    }

    internal void LeaveScope()
    {
        env = env.Parent!;
    }
}

public readonly struct NestedScope : IDisposable
{
    private readonly Compiler _compiler;

    public NestedScope(Compiler compiler)
    {
        _compiler = compiler;
    }

    public void Dispose()
    {
        _compiler.LeaveScope();
    }
}
