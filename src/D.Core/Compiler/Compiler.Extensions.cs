using System;

namespace E
{
    public partial class Compiler
    {
        public Nested EnterScope(string name)
        {
            env = env.Nested(name);

            return new Nested(this);
        }

        internal void LeaveScope()
        {
            env = env.Parent;
        }
    }

    public readonly struct Nested : IDisposable
    {
        private readonly Compiler compiler;

        public Nested(Compiler compiler)
        {
            this.compiler = compiler;
        }

        public void Dispose()
        {
            compiler.LeaveScope();
        }
    }
}
