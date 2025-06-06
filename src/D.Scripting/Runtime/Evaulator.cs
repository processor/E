﻿#pragma warning disable CA1822 // Mark members as static

using System;
using System.Collections.Generic;

using E.Expressions;
using E.Parsing;
using E.Symbols;
using E.Syntax;
using E.Units;

namespace E;

// TODO: precision

public class Evaluator
{
    private readonly Scope scope = new();
    private readonly Compiler compiler = new();

    private readonly Node _env;

    int count = 0;

    public Evaluator()
        : this(new Node()) { }

    public Evaluator(Node env)
    {
        _env = env;
    }

    public Evaluator(IObject start, Node env)
    {
        scope.This = start;

        _env = env;
    }

    public object? This
    {
        get => scope.This;
        set => scope.This = value;
    }

    public Scope Scope => scope;

    public System.Type NumericType { get; init; } = typeof(double);

    public object? Evaluate(string script)
    {
        object? last = null;

        var parser = new Parser(script, _env);

        while (parser.TryReadNext(out var syntax))
        {
            last = Evaluate(syntax);
        }
        
        return last;
    }

    public object Evaluate(ISyntaxNode syntax)
    {
        if (syntax is null) return null!;

        var expression = compiler.Visit(syntax);

        return Evaluate(expression);
    }

    public object Evaluate(object expression)
    {
        if (count > 1000) throw new Exception("too many calls");

        count++;
            
        object? result = expression switch
        {
            BinaryExpression binary      => EvaluateBinary(binary),    
            ConstantExpression constant  => EvaluateConstant(constant),
            Symbol symbol                => EvaluateSymbol(symbol),    
            CallExpression call          => EvaluateCall(call),        
            QuantityLiteral quantity     => EvaluateQuantity(quantity),        
            IQuantity<double> { Unit.Dimension: Dimension.None } unitValue => new Number<double>(unitValue.As<double>()), // TODO: optimize
            _                            => expression // if ((long)expression.Kind > 255) throw new Exception($"expected kind: was {expression.Kind}");

        };

        scope.This = result;

        return result;
    }
        
    public IObject EvaluateConstant(ConstantExpression expression)
    {
        // Pull out the value

        return (IObject)expression.Value; 
    }

    public IObject EvaluateQuantity(QuantityLiteral expression)
    {
        var unit = UnitInfo.Get(expression.UnitName);

        if (expression is { UnitPower: not 0, UnitPower: not 1 })
        {
            unit = unit.WithExponent(expression.UnitPower);
        }

        double value = ((INumberObject)expression.Expression).As<double>();

        // e.g. %
        if (unit is { Dimension: Dimension.None, DefinitionUnit: INumberObject definitionUnit })
        {
            return new Number<double>(value * definitionUnit.As<double>());
        }

        return Quantity.Create(value, unit);
    }

    public object EvaluateSymbol(Symbol expression)
    {
        var value = scope.Get(expression.Name);

        // Make sure this isn't called again...

        if (value is null) return expression;

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
            var parameters = new ListBuilder<Parameter>();

            foreach (var arg in expression.Arguments)
            {
                if (arg.Value is Symbol name)
                {
                    parameters.Add(Expression.Parameter(name.ToString()));
                }
            }

            return new FunctionExpression(parameters.ToArray(), new LambdaExpression(expression));
        }

        if (_env.TryGetValue(expression.FunctionName, out IFunction? func))
        {
            return func.Invoke(args);
        }

        throw new Exception($"ƒ {expression.FunctionName} not found");
    }

    private readonly struct ArgumentEvaluationResult(IArguments args, bool hasSymbols)
    {
        public IArguments Arguments { get; } = args;

        public bool ContainsUnresolvedSymbols { get; } = hasSymbols;
    }

    private ArgumentEvaluationResult EvaluateArguments(IArguments args, bool includeThis = false)
    {
        var result = new object?[args.Count + (includeThis ? 1 : 0)];

        int offset = 0;
        int unresolvedSymbolCount = 0;

        if (includeThis)
        {
            offset = 1;
            result[0] = scope.This;
        }

        for (int i = 0; i < args.Count; i++)
        {
            var arg = Evaluate(args[i]);

            result[i + offset] = arg;

            if (arg is Symbol) unresolvedSymbolCount++;
        }

        return new ArgumentEvaluationResult(
            Arguments.Create(result), 
            hasSymbols: unresolvedSymbolCount > 0);
    }
        
    public object EvaluateBinary(BinaryExpression expression)
    {
        IObject l = (IObject)Evaluate(expression.Left);
        IObject r = (IObject)Evaluate(expression.Right);

        if (l is Symbol lSymbol && expression.Kind is ObjectType.AssignmentExpression)
        {
            scope.Set(lSymbol.Name, r);

            return r;
        }

        #region Maybe ƒ

        // Simplify logic here?

        if ((l.Kind is ObjectType.Symbol or ObjectType.Expression) || (r.Kind is ObjectType.Symbol or ObjectType.Expression))
        {
            var args = new List<Parameter>();

            if (l is Symbol lhsSymbol)
            {
                args.Add(Expression.Parameter(lhsSymbol.ToString()));
            }
            else if (l is FunctionExpression lf)
            {
                args.AddRange(lf.Parameters);

                l = lf.Body!;
            }

            if (r is Symbol parameterSymbol)
            {
                args.Add(Expression.Parameter(parameterSymbol.Name));
            }
            else if (r is FunctionExpression rf)
            {
                args.AddRange(rf.Parameters);

                r = rf.Body!;
            }

            // Check if it's a predicate
            // x > 5
            // y < 100
            // y == 1

            if (l is Symbol name && (expression.Operator.IsComparison))
            {
                return new Predicate(expression.Operator, name, r);
            }

            return new FunctionExpression(
                parameters : [.. args], 
                body       : new BinaryExpression(expression.Operator, l, r)
            );
        }

        #endregion

        if (_env.TryGetValue(expression.Operator.Name, out IFunction? func))
        {
            return func.Invoke(Arguments.Create([l, r]));
        }
        else
        {
            throw new Exception($"ƒ(a,b) for '{expression.Operator.Name}' not found");
        }
    }
}