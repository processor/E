using System;

namespace D
{
    public partial class Compiler
    {
        public Nested EnterScope(string name)
        {
            scope = scope.Nested(name);

            return new Nested(this);
        }

        internal void LeaveScope()
        {
            scope = scope.Parent;
        }
    }

    public struct Nested : IDisposable
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
