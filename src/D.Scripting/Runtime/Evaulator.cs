using System;
using System.Collections.Generic;

namespace D
{
    using Syntax;
    using Compilation;
    using Expressions;
    using Parsing;
    using Units;

    public class Evaulator
    {
        private Scope scope = new Scope();
        private Compiler compiler = new Compiler();

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
                foreach (var syntax in parser.Enumerate())
                {
                    last = Evaluate(syntax);
                }
            }

            return last;
        }

        public IObject Evaluate(SyntaxNode sytax)
        {
            var expression = compiler.Visit(sytax);

            return Evaluate(expression);
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
                    case Kind.ConstantExpression : result = EvaluateConstant((ConstantExpression)expression); break;
                    case Kind.PipeStatement      : result = EvaluatePipe((PipeStatement)expression);          break;
                    case Kind.Symbol             : result = EvaluateSymbol((Symbol)expression);               break;
                    case Kind.CallExpression     : result = EvaluateCall((CallExpression)expression);         break;
                    case Kind.UnitLiteral        : result = EvaluateUnit((UnitLiteral)expression);            break;
                    default:
                    
                        if ((long)expression.Kind > 255) throw new Exception($"expected kind: was {expression.Kind}");

                        result = expression;
                            break;    
                }
            }

            scope.This = result;

            return result;
        }

        
        public IObject EvaluateConstant(ConstantExpression expression)
        {
            // Pull out the value


            return (IObject)expression.Value; 
        }

        public IObject EvaluateUnit(UnitLiteral expression)
        {
            var number = (INumber)expression.Expression;

            if (!Unit<double>.TryParse(expression.UnitName, out Unit<double> unit))
            {
                throw new Exception("no unit found for:" + expression.UnitName);
            }

            return unit.With(number.Real, expression.UnitPower);
        }

        public IObject EvaluateSymbol(Symbol expression)
        {
            var value = scope.Get(expression.Name);

            // Make sure this isn't called again...

            if (value == null) return expression;

            var v = value;

            
            if ((long)v.Kind < 255) return v;

            return Evaluate((IExpression)value);
        }
        

        public IObject EvaluatePipe(PipeStatement expression)
        {
            var call = (CallExpression)expression.Expression;

            // TODO: Handle match

            return EvaluateCall(call, piped: true);
        }

        public IObject EvaluateCall(CallExpression expression, bool piped = false)
        {
            var argList = EvaluateArguments(expression.Arguments, piped);
            var args = argList.Arguments;

            if (argList.ContainsUnresolvedSymbols)
            {
                var parameters = new List<Parameter>();

                foreach (var arg in expression.Arguments)
                {
                    if (arg.Value is Symbol)
                    {
                        parameters.Add(Expression.Parameter(arg.Value.ToString()));
                    }
                }

                return new Function(parameters.ToArray(), new LambdaExpression(expression));
            }

            if (env.TryGet(expression.FunctionName, out IObject func))
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

            if (l is Symbol || l is Function || r is Symbol || r is Function)
            {
                var args = new List<Parameter>();

                if (l is Symbol)
                {
                    args.Add(Expression.Parameter(l.ToString()));
                }
                else if (l is Function lf)
                {
                    args.AddRange(lf.Parameters);

                    l = lf.Body;
                }

                if (r is Symbol)
                {
                    args.Add(Expression.Parameter(r.ToString()));
                }
                else if (r is Function rf)
                {
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

                return new Function(args.ToArray(), new BinaryExpression(expression.Operator, l, r));
            }

            #endregion

            if (env.TryGet(expression.Operator.Name, out IFunction func))
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
