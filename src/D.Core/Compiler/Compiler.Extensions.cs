using System;

namespace E;

public partial class Compiler
{
    public NestedScope EnterScope(string name)
    {
        _env = _env.Nested(name);

        return new NestedScope(this);
    }

    internal void LeaveScope()
    {
        _env = _env.Parent!;
    }
}

public readonly struct NestedScope(Compiler compiler) : IDisposable
{
    private readonly Compiler _compiler = compiler;

    public void Dispose()
    {
        _compiler.LeaveScope();
    }
}