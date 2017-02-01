using System.Collections.Generic;

namespace D.Compilation
{
    public class CompliationUnit
    {
        public List<Type> Types { get; } = new List<Type>();

        // LooseFunctions

        public List<FunctionExpression> Functions { get; } = new List<FunctionExpression>();


        public List<Protocal> Protocals { get; } = new List<Protocal>();

        // GetTypeMembers

        // Statements
    }
}