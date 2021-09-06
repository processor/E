﻿using System;
using System.Collections.Generic;
using System.Globalization;

using E.Expressions;
using E.Inference;
using E.Symbols;
using E.Syntax;
using E.Units;

namespace E
{
    public partial class Compiler
    {
        // Phases:
        // - Parse Syntax Tree into a LIR
        // - Capture declarations within modules ...
        // - Bind symbols to their declarations
        // - Transform to ExpressionTree

        private Node env;
        private readonly Flow flow = new Flow();

        public Compiler()
            : this(new Node()) { }

        public Compiler(Node env)
        {
            this.env = env;
        }

        public Compilation Compile(IEnumerable<ISyntaxNode> nodes, string? moduleName = null)
        {
            var compilation = new Compilation();

            var module = new Module(moduleName);

            foreach (var node in nodes)
            {
                module.Add(Visit(node));
            }

            compilation.Expressions.Add(module);

            return compilation;
        }

        public BlockExpression VisitBlock(BlockSyntax syntax)
        {
            var statements = new IExpression[syntax.Statements.Count];

            for (int i = 0; i < statements.Length; i++)
            { 
                statements[i] = Visit(syntax.Statements[i]);
            }

            return new BlockExpression(statements);
        }

        public Module VisitModule(ModuleSyntax syntax)
        {
            var module = new Module(syntax.Name);

            for (var i = 0; i < syntax.Statements.Count; i++)
            {                
                module.Add(Visit(syntax.Statements[i]));   
            }
            
            return module;
        }

        int i = 0;

        public IExpression Visit(ISyntaxNode syntax)
        {
            if (syntax is null) throw new ArgumentNullException(nameof(syntax));

            i++;

            if (i > 300) throw new Exception("recursion???");

            switch (syntax)
            {
                case UnaryExpressionSyntax unary     : return VisitUnary(unary);
                case BinaryExpressionSyntax binary   : return VisitBinary(binary);
                case TernaryExpressionSyntax ternary : return VisitTernary(ternary);
                case BlockSyntax block               : return VisitBlock(block);
                case CallExpressionSyntax call       : return VisitCall(call);
                case MatchExpressionSyntax match     : return VisitMatch(match);
                case ModuleSyntax module             : return VisitModule(module);
                case TupleExpressionSyntax tuple     : return VisitTuple(tuple);
            }

            return syntax.Kind switch
            {
                SyntaxKind.LambdaExpression             => VisitLambda((LambdaExpressionSyntax)syntax),
                SyntaxKind.ImplementationDeclaration    => VisitImplementation((ImplementationDeclarationSyntax)syntax),
                SyntaxKind.InterpolatedStringExpression => VisitInterpolatedStringExpression((InterpolatedStringExpressionSyntax)syntax),

                // Declarations
                SyntaxKind.TypeDeclaration              => VisitTypeDeclaration((TypeDeclarationSyntax)syntax),
                SyntaxKind.FunctionDeclaration          => VisitFunctionDeclaration((FunctionDeclarationSyntax)syntax),
                SyntaxKind.ProtocolDeclaration          => VisitProtocol((ProtocolDeclarationSyntax)syntax),
                SyntaxKind.PropertyDeclaration          => VisitVariableDeclaration((PropertyDeclarationSyntax)syntax),
                SyntaxKind.TypeInitializer              => VisitObjectInitializer((ObjectInitializerSyntax)syntax),
                SyntaxKind.DestructuringAssignment      => VisitDestructuringAssignment((DestructuringAssignmentSyntax)syntax),
                SyntaxKind.MemberAccessExpression       => VisitMemberAccess((MemberAccessExpressionSyntax)syntax),
                SyntaxKind.IndexAccessExpression        => VisitIndexAccess((IndexAccessExpressionSyntax)syntax),

                // Statements                                 
                SyntaxKind.ForStatement                 => VisitFor((ForStatementSyntax)syntax),
                SyntaxKind.IfStatement                  => VisitIf((IfStatementSyntax)syntax),
                SyntaxKind.ElseIfStatement              => VisitElseIf((ElseIfStatementSyntax)syntax),
                SyntaxKind.ElseStatement                => VisitElse((ElseStatementSyntax)syntax),
                SyntaxKind.ReturnStatement              => VisitReturnStatement((ReturnStatementSyntax)syntax),

                // Patterns                                   
                SyntaxKind.ConstantPattern              => VisitConstantPattern((ConstantPatternSyntax)syntax),
                SyntaxKind.TypePattern                  => VisitTypePattern((TypePatternSyntax)syntax),
                SyntaxKind.AnyPattern                   => VisitAnyPattern((AnyPatternSyntax)syntax),

                SyntaxKind.Symbol                       => VisitSymbol((Symbol)syntax),
                SyntaxKind.NumberLiteral                => VisitNumber((NumberLiteralSyntax)syntax),
                SyntaxKind.UnitValueLiteral             => VisitUnitValue((UnitValueSyntax)syntax),
                SyntaxKind.StringLiteral                => new StringLiteral(syntax.ToString()),
                SyntaxKind.ArrayInitializer             => VisitNewArray((ArrayInitializerSyntax)syntax),

                _ => throw new Exception("Unexpected syntax:" + syntax.Kind + "/" + syntax.GetType().ToString()),
            };
        }

        public ArrayInitializer VisitNewArray(ArrayInitializerSyntax syntax)
        {
            var elements = new IExpression[syntax.Elements.Length];

            Type? bestElementType = null;

            for (var i = 0; i < elements.Length; i++)
            {
                var element = Visit(syntax.Elements[i]);

                elements[i] = element;

                Type type = GetType(element);
                
                if (bestElementType is null)
                {
                    bestElementType = type;
                }
                else if (bestElementType.Id != type.Id)
                {
                    if (type.BaseType is null || bestElementType.BaseType is null)
                    {
                        bestElementType = Type.Get(ObjectType.Object);
                    }
                    else if (bestElementType.BaseType.Id == type.BaseType.Id)
                    {
                        bestElementType = type.BaseType;
                    }
                }

                // TODO | If object, find by common implementation

            }

            return new ArrayInitializer(elements, 
                stride      : syntax.Stride, 
                elementType : bestElementType
            );
        }

        public IExpression VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax syntax)
        {
            var members = new IExpression[syntax.Children.Length];

            for (int i = 0; i < members.Length; i++)
            {
                members[i] = Visit(syntax.Children[i]);
            }

            return new InterpolatedStringExpression(members);
        }

        public IExpression VisitUnitValue(UnitValueSyntax value)
        {
            var lhs = Visit(value.Expression);

            if (UnitSet.Default.TryGet(value.UnitName, out var unit))
            {
                if (unit.Dimension is Dimension.None && unit.DefinitionUnit is Number definationUnit)
                {
                    return new BinaryExpression(Operator.Multiply, lhs, definationUnit) { Grouped = true };
                }
            }

            return new UnitValueLiteral(lhs, value.UnitName, value.UnitPower);
        }

        public virtual IExpression VisitAnyPattern(AnyPatternSyntax syntax) => new AnyPattern();

        public virtual IExpression VisitNumber(NumberLiteralSyntax syntax)
        {
            if (syntax.Text.IndexOf('.') > -1)
            {
                return new Number(double.Parse(syntax.Text, CultureInfo.InvariantCulture));
            }
            else
            {
                return new Integer(long.Parse(syntax.Text, CultureInfo.InvariantCulture));
            }
        }

        public virtual BinaryExpression VisitBinary(BinaryExpressionSyntax syntax)
        {
            return new BinaryExpression(syntax.Operator, Visit(syntax.Left), Visit(syntax.Right)) { Grouped = syntax.IsParenthesized };
        }

        public virtual UnaryExpression VisitUnary(UnaryExpressionSyntax syntax)
        {
            return new UnaryExpression(syntax.Operator, arg: Visit(syntax.Argument));
        }

        public virtual TernaryExpression VisitTernary(TernaryExpressionSyntax syntax)
        {
            return new TernaryExpression(Visit(syntax.Condition), Visit(syntax.Left), Visit(syntax.Right));
        }

        public virtual TupleExpression VisitTuple(TupleExpressionSyntax syntax)
        {
            var elements = new IExpression[syntax.Elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                var elementSyntax = syntax.Elements[i];

                elements[i] = Visit(elementSyntax);
            }

            return new TupleExpression(elements);
        }

        public virtual CallExpression VisitCall(CallExpressionSyntax syntax)
        {
            Type? objectType = null;

            if (char.IsUpper(syntax.Name.Name[0]))
            {
                objectType = env.GetType(syntax.Name);
            }
           
            return new CallExpression(
                callee       : syntax.Callee is not null ? Visit(syntax.Callee) : null,
                functionName : syntax.Name,
                arguments    : VisitArguments(syntax.Arguments),
                isPiped      : syntax.IsPiped) {
                ReturnType = objectType
            };
        }

        private IArguments VisitArguments(IReadOnlyList<ArgumentSyntax> arguments)
        {
            if (arguments.Count == 0) return Arguments.None;
            
            var items = new Argument[arguments.Count];

            for (int i = 0; i < items.Length; i++)
            {
                var arg = arguments[i];

                var value = Visit(arg.Value);

                items[i] = new Argument(arg.Name, value);
            }

            return Arguments.Create(items);
        }

        public virtual VariableDeclaration VisitVariableDeclaration(PropertyDeclarationSyntax syntax)
        {
            var value = Visit(syntax.Value!);
            var type  = GetType(syntax.Type ?? value);
            
            flow.Define(syntax.Name, type);

            return new VariableDeclaration(syntax.Name, type, syntax.Flags, value);
        }

        public virtual TypeInitializer VisitObjectInitializer(ObjectInitializerSyntax syntax)
        {
            var members = new Argument[syntax.Arguments.Count];

            for (var i = 0; i < members.Length; i++)
            {
                var m = syntax.Arguments[i];

                Symbol? name = m.Name;

                // Infer the name from the value symbol
                if (name is null && m.Value is Symbol valueName)
                {
                    name = valueName;
                }

                members[i] = new Argument(name, Visit(m.Value)); 
            }

            return new TypeInitializer(syntax.Type, members);
        }

        public virtual DestructuringAssignment VisitDestructuringAssignment(DestructuringAssignmentSyntax syntax)
        {
            var elements = new AssignmentElement[syntax.Variables.Count];

            for (var i = 0; i < elements.Length; i++)
            {
                var m = syntax.Variables[i];

                elements[i] = new AssignmentElement(m.Name, Type.Get(ObjectType.Object));
            }

            return new DestructuringAssignment(elements, Visit(syntax.Instance));
        }

        public virtual IndexAccessExpression VisitIndexAccess(IndexAccessExpressionSyntax syntax)
            => new IndexAccessExpression(Visit(syntax.Left), VisitArguments(syntax.Arguments));

        public virtual MemberAccessExpression VisitMemberAccess(MemberAccessExpressionSyntax syntax)
            => new MemberAccessExpression(Visit(syntax.Left), syntax.Name);

        public virtual LambdaExpression VisitLambda(LambdaExpressionSyntax syntax)
        {
            return new LambdaExpression(Visit(syntax.Expression));
        }

        public virtual MatchExpression VisitMatch(MatchExpressionSyntax syntax)
        {
            var cases = new MatchCase[syntax.Cases.Count];

            for (var i = 0; i < cases.Length; i++)
            {
                cases[i] = VisitCase(syntax.Cases[i]);
            }

            return new MatchExpression(Visit(syntax.Expression), cases);
        }

        public virtual MatchCase VisitCase(MatchCaseSyntax syntax)
        {
            return new MatchCase(
                pattern   : Visit(syntax.Pattern), 
                condition : syntax.Condition is not null ? Visit(syntax.Condition) : null,
                body      : VisitLambda(syntax.Body)
            );
        }

        public virtual TypePattern VisitTypePattern(TypePatternSyntax pattern) => 
            new TypePattern(pattern.TypeExpression, pattern.VariableName);

        public virtual ConstantPattern VisitConstantPattern(ConstantPatternSyntax pattern) => 
            new ConstantPattern(Visit(pattern.Constant));

        public virtual Symbol VisitSymbol(Symbol symbol)
        {
            if (symbol is TypeSymbol typeSymbol && typeSymbol.Status is SymbolStatus.Unresolved)
            {
                if (env.TryGetValue<Type>(typeSymbol, out var value))
                {
                    typeSymbol.Initialize(value);
                }
            }

            // Bind... 

            return symbol;
        }

        public Parameter[] ResolveParameters(IReadOnlyList<ParameterSyntax> parameters)
        {
            var result = new Parameter[parameters.Count];

            // nested function...

            for (var i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];

                var type = parameter.Type is not null
                    ? env.Get<Type>(parameter.Type)
                    : new Type(ObjectType.Object); // TODO: Introduce generic or infer from body?

                // Any

                result[i] = new Parameter(parameter.Name ?? "_" + i, type, false, parameter.DefaultValue);
            }

            return result;
        }
    }
}
