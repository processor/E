﻿using System;
using System.Collections.Generic;

namespace D
{
    using Expressions;
    using Parsing;

    public class Evaulator
    {
        private Scope scope = new Scope();
        private Env env;

        int count = 0;

        public Evaulator()
            : this(new Env()) { }

        public Evaulator(Env env)
        {
            this.env = env;
        }

        public Evaulator(IObject start, Env env)
        {
            scope.This = start;

            this.env = env;
        }

        public IObject This
        {
            get { return scope.This; }
            set { scope.This = value; }
        }

        public Scope Scope => scope;

        public IObject Evaluate(string script)
        {
            IObject last = null;

            using (var parser = new Parser(script, env))
            {
                foreach (var statement in parser.Enumerate())
                {
                    last = Evaluate(statement);
                }
            }

            return last;
        }

        public IObject Evaluate(IObject expression)
        {
            if (count > 1000) throw new Exception("too many calls");

            count++;

            IObject result = null;

            if (expression is BinaryExpression)
            {
                result = EvaluateBinaryExpression((BinaryExpression)expression);
            }

            else
            { 
                switch (expression.Kind)
                {
                    case Kind.PipeStatement   : result = Evaluate((PipeStatement)expression);        break;
                    case Kind.Symbol          : result = Evaluate((Symbol)expression);               break;
                    case Kind.CallExpression  : result = Evaluate((CallExpression)expression);       break;
                    default:
                    
                        if ((long)expression.Kind > 255) throw new Exception("expected kind");

                        result = expression;
                            break;    
                }
            }

            scope.This = result;

            return result;
        }

        public IObject Evaluate(Symbol expression)
        {
            var value = scope.Get(expression.Name);

            // Make sure this isn't called again...

            if (value == null) return expression;

            var v = value;

            
            if ((long)v.Kind < 255) return v;

            return Evaluate(value);
        }
        

        public IObject Evaluate(PipeStatement expression)
        {
            var call = (CallExpression)expression.Expression;

            // TODO: Handle match

            return Evaluate(call, piped: true);
        }

        public IObject Evaluate(CallExpression expression, bool piped = false)
        {
            var argList = EvaluateArguments(expression.Arguments, piped);
            var args = argList.Arguments;

            if (argList.ContainsUnresolvedSymbols)
            {
                var parameters = new List<ParameterExpression>();

                foreach (var arg in expression.Arguments)
                {
                    if (arg.Value is Symbol)
                    {
                        parameters.Add(Expression.Parameter(arg.Value.ToString()));
                    }
                }

                return new FunctionDeclaration(parameters.ToArray(), new LambdaExpression(expression));
            }

            IObject func;

            if (env.TryGet(expression.FunctionName, out func))
            {
                return ((IFunction)func).Invoke(args);
            }
            

            throw new Exception($"function {expression.FunctionName} not found");
        }

        private struct ArgumentEvaluationResult
        {
            public ArgumentEvaluationResult(IArguments args, bool hasSymbols)
            {
                Arguments = args;
                ContainsUnresolvedSymbols = hasSymbols;
            }

            public IArguments Arguments { get; }

            public bool ContainsUnresolvedSymbols { get; }
        }

        private ArgumentEvaluationResult EvaluateArguments(IArguments args, bool includeThis = false)
        {
            var result = new IObject[args.Count + (includeThis ? 1 : 0)];

            var offset = 0;
            var unresolvedSymbolCount = 0;

            if (includeThis)
            {
                offset = 1;
                result[0] = scope.This;
            }

            for (var i = 0; i < args.Count; i++)
            {
                var arg = Evaluate(args[i]);

                result[i + offset] = arg;

                if (arg is Symbol) unresolvedSymbolCount++;
            }


            return new ArgumentEvaluationResult(Arguments.Create(result), unresolvedSymbolCount > 0);
        }
        
        int q = 0;

        public IObject EvaluateBinaryExpression(BinaryExpression expression)
        {
            var l = Evaluate(expression.Left);
            var r = Evaluate(expression.Right);

            if (l is Symbol && expression.Kind == Kind.AssignmentExpression)
            {
                scope.Set(l.ToString(), r);

                return r;
            }

            q++;

            #region Maybe function

            // Simplify logic here?

            if (l is Symbol || l is FunctionDeclaration || r is Symbol || r is FunctionDeclaration)
            {
                var args = new List<ParameterExpression>();

                if (l is Symbol)
                {
                    args.Add(Expression.Parameter(l.ToString()));
                }
                else if (l is FunctionDeclaration)
                {
                    var lf = (FunctionDeclaration)l;

                    args.AddRange(lf.Parameters);

                    l = lf.Body;
                }

                if (r is Symbol)
                {
                    args.Add(Expression.Parameter(r.ToString()));
                }
                else if (r is FunctionDeclaration)
                {
                    var rf = (FunctionDeclaration)r;

                    args.AddRange(rf.Parameters);

                    r = rf.Body;
                }


                // Check if it's an invariant
                // x > 5
                // y < 100

                if (l is Symbol)
                {
                    switch (expression.Operator.Name)
                    {
                        case ">":
                        case ">=":
                        case "<":
                        case "<=": 
                            return new Predicate(expression.Operator, (Symbol)l, r);
                    }
                }

                // i > 10

                return new FunctionDeclaration(args.ToArray(), new BinaryExpression(expression.Operator, l, r));
            }

            #endregion

            IFunction func;

            if (env.TryGet(expression.Operator.Name, out func))
            {
                return func.Invoke(Arguments.Create(l, r));
            }
            else
            {
                throw new Exception(expression.Operator.Name + " has no function");
            }
        }
    }
}
