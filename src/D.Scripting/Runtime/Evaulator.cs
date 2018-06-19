using System;
using System.Collections.Generic;

namespace D
{
    using Expressions;
    using Parsing;
    using Syntax;
    using Units;

    public class Evaluator
    {
        private Scope scope = new Scope();
        private Compiler compiler = new Compiler();

        private Node env;

        int count = 0;

        public Evaluator()
            : this(new Node()) { }

        public Evaluator(Node env)
        {
            this.env = env;
        }

        public Evaluator(IObject start, Node env)
        {
            scope.This = start;

            this.env = env;
        }

        public object This
        {
            get => scope.This;
            set => scope.This = value;
        }

        public Scope Scope => scope;

        public object Evaluate(string script)
        {
            object last = null;

            using (var parser = new Parser(script, env))
            {
                foreach (var syntax in parser.Enumerate())
                {
                    last = Evaluate(syntax);
                }
            }

            return last;
        }

        public object Evaluate(ISyntaxNode sytax)
        {
            var expression = compiler.Visit(sytax);

            return Evaluate(expression);
        }

        public object Evaluate(object expression)
        {
            if (count > 1000) throw new Exception("too many calls");

            count++;

            object result = null;
            
            switch (expression)
            {
                case BinaryExpression binary      : result = EvaluateBinary(binary);           break;
                case ConstantExpression constant  : result = EvaluateConstant(constant);       break;
                case Symbol symbol                : result = EvaluateSymbol(symbol);           break;
                case CallExpression call          : result = EvaluateCall(call);               break;
                case UnitValueLiteral unit        : result = EvaluateUnit(unit);               break;

                case IUnitValue unitValue when unitValue.Unit.Dimension == Dimension.None:
                    return new Number(unitValue.Real);

                default:

                    // if ((long)expression.Kind > 255) throw new Exception($"expected kind: was {expression.Kind}");

                    result = expression;

                    break;
            }

            scope.This = result;

            return result;
        }
        
        public IObject EvaluateConstant(ConstantExpression expression)
        {
            // Pull out the value

            return (IObject)expression.Value; 
        }

        public IObject EvaluateUnit(UnitValueLiteral expression)
        {
            if (!UnitInfo.TryParse(expression.UnitName, out var unit))
            {
                throw new Exception($"Unit '{expression.UnitName}' was not found");
            }

            if (expression.UnitPower != 0 && expression.UnitPower != 1)
            {
                unit = unit.WithExponent(expression.UnitPower);
            }

            double value = ((INumber)expression.Expression).Real;

            if (unit.Dimension == Dimension.None && unit.DefinitionUnit is INumber definationUnit)
            {
                return new Number(value * definationUnit.Real);
            }

            return UnitValue.Create(value, unit);
        }

        public object EvaluateSymbol(Symbol expression)
        {
            var value = scope.Get(expression.Name);

            // Make sure this isn't called again...

            if (value == null) return expression;

            if (value is IObject vo && (long)vo.Kind < 255)
            {
                return value;
            }
            
            return Evaluate((IExpression)value);
        }
      
        public object EvaluateCall(CallExpression expression)
        {
            var argList = EvaluateArguments(expression.Arguments, expression.IsPiped);
            var args = argList.Arguments;

            if (argList.ContainsUnresolvedSymbols)
            {
                var parameters = new List<Parameter>();

                foreach (var arg in expression.Arguments)
                {
                    if (arg.Value is Symbol name)
                    {
                        parameters.Add(Expression.Parameter(name.ToString()));
                    }
                }

                return new FunctionExpression(parameters.ToArray(), new LambdaExpression(expression));
            }

            if (env.TryGetValue(expression.FunctionName, out IFunction func))
            {
                return func.Invoke(args);
            }

            throw new Exception($"function {expression.FunctionName} not found");
        }

        private readonly struct ArgumentEvaluationResult
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
            var result = new object[args.Count + (includeThis ? 1 : 0)];

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

        public object EvaluateBinary(BinaryExpression expression)
        {
            var l = Evaluate(expression.Left);
            var r = Evaluate(expression.Right);

            if (l is Symbol lSymbol && expression.Kind == Kind.AssignmentExpression)
            {
                scope.Set(lSymbol.Name, r);

                return r;
            }

            q++;

            #region Maybe function

            // Simplify logic here?

            if (l is Symbol || l is FunctionExpression || r is Symbol || r is FunctionExpression)
            {
                var args = new List<Parameter>();

                if (l is Symbol)
                {
                    args.Add(Expression.Parameter(l.ToString()));
                }
                else if (l is FunctionExpression lf)
                {
                    args.AddRange(lf.Parameters);

                    l = lf.Body;
                }

                if (r is Symbol)
                {
                    args.Add(Expression.Parameter(r.ToString()));
                }
                else if (r is FunctionExpression rf)
                {
                    args.AddRange(rf.Parameters);

                    r = rf.Body;
                }

                // Check if it's a predicate
                // x > 5
                // y < 100

                if (l is Symbol name)
                {
                    switch (expression.Operator.Name)
                    {
                        case ">":
                        case ">=":
                        case "<":
                        case "<=": 
                            return new Predicate(expression.Operator, name, (IObject)r);
                    }
                }

                // i > 10

                return new FunctionExpression(args.ToArray(), new BinaryExpression(expression.Operator, (IObject)l, (IObject)r));
            }

            #endregion

            if (env.TryGetValue(expression.Operator.Name, out IFunction func))
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