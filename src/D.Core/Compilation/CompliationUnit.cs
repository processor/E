using System.Collections.Generic;

namespace D.Compilation
{
    public class CompliationUnit
    {
        public List<Type> Types { get; } = new List<Type>();

        // LooseFunctions

        public List<Function> Functions { get; } = new List<Function>();

        public Dictionary<Type, List<Implementation>> Implementations { get; } = new Dictionary<Type, List<Implementation>>();

        public List<Protocal> Protocals { get; } = new List<Protocal>();

        // GetTypeMembers

        // Statements
    }
}