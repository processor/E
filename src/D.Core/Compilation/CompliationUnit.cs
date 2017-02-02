using System.Collections.Generic;

namespace D.Compilation
{
    using Expressions;

    public class CompliationUnit
    {
        public List<Type> Types { get; } = new List<Type>();

        // LooseFunctions

        public List<FunctionExpression> Functions { get; } = new List<FunctionExpression>();


        public List<ProtocalExpression> Protocals { get; } = new List<ProtocalExpression>();

        // GetTypeMembers

        // Statements
    }
}