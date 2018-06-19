using System.Collections;
using System.Collections.Generic;
using D.Expressions;

namespace D
{
    public class Module : IExpression
    {
        public readonly IDictionary<string, object> exports = new Dictionary<string, object>();


        private readonly List<IExpression> statements = new List<IExpression>();

        public Module(string name = null, Module parent = null)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; }

        public Module Parent { get; }

        public Kind Kind => Kind.Module;

        // Imported
        // Exported

        public void Add(IExpression value)
        {
            // TODO: Check visibility
            statements.Add(value);
        }

        public void AddExport(INamedObject value)
        {
            exports[value.Name] = value;
        }

        public void AddExport(string name, object value)
        {
            exports[name] = value;
        }

        public IDictionary<string, object> Exports => exports;

        public IReadOnlyList<IExpression> Statements => statements;
    
    }
}