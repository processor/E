using System;
using System.Collections.Generic;

namespace D
{
    using Expressions;
    using Syntax;

    public partial class Compiler
    {
        // Phases:
        // - Parse into Syntax Tree
        // - Capture declarations within modules ...
        // - Bind symbols to their declarations
        // - Transform to ExpressionTree

        private readonly Node graph = new Node();

        private Node scope;

        public Compiler()
        {
            this.scope = graph;
        }

        public Module Compile(IEnumerable<SyntaxNode> nodes)
        {
            var module = new Module();
            
            foreach (var node in nodes)
            {
                if (node is FunctionDeclarationSyntax func)
                {
                    var function = VisitFunctionDeclaration(func);

                    module.Add(function);

                    if (function.Name != null)
                    {
                        scope.Add(function.Name, function);
                    }
                }
                else if (node is TypeDeclarationSyntax typeDeclaration)
                {
                    var type = VisitTypeDeclaration(typeDeclaration);

                    module.Add(type);

                    scope.Add(type.Name, type);
                }
                else if (node is ProtocolDeclarationSyntax protocolDeclaration)
                {
                    var protocol = VisitProtocol(protocolDeclaration);

                    module.Add(protocol);

                    scope.Add(protocol.Name, protocol);
                }
                else if (node is ImplementationDeclarationSyntax implDeclaration)
                {
                    var impl = VisitImplementation(implDeclaration);

                    impl.Type.Implementations.Add(impl);
                }
            }

            return module;
        }
        

        public BlockExpression VisitBlock(BlockExpressionSyntax syntax)
        {
            var statements = new IExpression[syntax.Statements.Length];

            for(var i = 0; i < statements.Length; i++)
            { 
                statements[i] = Visit(syntax.Statements[i]);
            }

            return new BlockExpression(statements);
        }

        int i = 0;

        public IExpression Visit(SyntaxNode syntax)
        {
            i++;

            if (i > 300) throw new Exception("rerucssion???");

            if (syntax == null) return null;

            switch (syntax)
            {
                case UnaryExpressionSyntax unary     : return VisitUnary(unary);
                case BinaryExpressionSyntax binary   : return VisitBinary(binary);
                case TernaryExpressionSyntax ternary : return VisitTernary(ternary);
                case BlockExpressionSyntax block     : return VisitBlock(block);

                case LambdaExpressionSyntax lambda   : return VisitLambda(lambda);

                case CallExpressionSyntax call       : return VisitCall(call);
                case MatchExpressionSyntax match     : return VisitMatch(match);
            }

            switch (syntax.Kind)
            {
                case Kind.InterpolatedStringExpression:  return VisitInterpolatedStringExpression((InterpolatedStringExpressionSyntax)syntax);

                // Declarations
                case Kind.VariableDeclaration     : return VisitVariableDeclaration((VariableDeclarationSyntax)syntax);
                case Kind.ObjectInitializer       : return VisitObjectInitializer((ObjectInitializerSyntax)syntax);
                case Kind.DestructuringAssignment : return VisitDestructuringAssignment((DestructuringAssignmentSyntax)syntax);
                case Kind.MemberAccessExpression  : return VisitMemberAccess((MemberAccessExpressionSyntax)syntax);
                case Kind.IndexAccessExpression   : return VisitIndexAccess((IndexAccessExpressionSyntax)syntax);

                // Statements
                case Kind.IfStatement             : return VisitIf((IfStatementSyntax)syntax);
                case Kind.ElseIfStatement         : return VisitElseIf((ElseIfStatementSyntax)syntax);
                case Kind.ElseStatement           : return VisitElse((ElseStatementSyntax)syntax);
                case Kind.ReturnStatement         : return VisitReturn((ReturnStatementSyntax)syntax);
             
                // Patterns
                case Kind.ConstantPattern         : return VisitConstantPattern((ConstantPatternSyntax)syntax);
                case Kind.TypePattern             : return VisitTypePattern((TypePatternSyntax)syntax);
                case Kind.AnyPattern              : return VisitAnyPattern((AnyPatternSyntax)syntax);

                case Kind.Symbol                  : return VisitSymbol((Symbol)syntax);
                case Kind.NumberLiteral           : return VisitNumber((NumberLiteralSyntax)syntax);
                case Kind.UnitLiteral             : return VisitUnit((UnitLiteralSyntax)syntax);
                
                case Kind.ArrayInitializer      : return VisitNewArray((ArrayInitializerSyntax)syntax);
                case Kind.StringLiteral           : return new StringLiteral(syntax.ToString());
            }

            throw new Exception("Unexpected node:" + syntax.Kind + "/" + syntax.GetType().ToString());
        }

        public ArrayInitializer VisitNewArray(ArrayInitializerSyntax syntax)
        {
            var elements = new IExpression[syntax.Elements.Length];

            Type bestElementType = null;

            for (var i = 0; i < elements.Length; i++)
            {
                var element = Visit(syntax.Elements[i]);

                elements[i] = element;

                var type = GetType(element);
                
                if (bestElementType == null)
                {
                    bestElementType = type;
                }
                else if (bestElementType != type)
                {
                    // TODO: Find the best common base type...

                    if (type.BaseType == null)
                    {
                        bestElementType = Type.Get(Kind.Object);
                    }
                    else if (bestElementType.BaseType == type.BaseType)
                    {
                        bestElementType = type.BaseType;
                    }

                    // throw new Exception($"array initialized with different types... {bestElementType} + {type}");
                }

            }

            return new ArrayInitializer(elements, 
                stride      : syntax.Stride, 
                elementType : bestElementType
            );
        }

        public IExpression VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax syntax)
        {
            var members = new IExpression[syntax.Children.Length];

            for (var i = 0; i < members.Length; i++)
            {
                members[i] = Visit(syntax.Children[i]);
            }

            return new InterpolatedStringExpression(members);
        }

        public IExpression VisitUnit(UnitLiteralSyntax unit)
        {
            // Lookup unit...
            // Parser power...

            return new UnitLiteral(Visit(unit.Expression), unit.UnitName, unit.UnitPower);
        }

        public virtual IExpression VisitAnyPattern(AnyPatternSyntax syntax)
            => new AnyPattern();

        public virtual IExpression VisitNumber(NumberLiteralSyntax expression)
        {
            if (expression.Text.Contains("."))
            {
                return new Number(double.Parse(expression.Text));
            }
            else
            {
                return new Integer(long.Parse(expression.Text));
            }
        }

        public virtual BinaryExpression VisitBinary(BinaryExpressionSyntax syntax)
            => new BinaryExpression(syntax.Operator, Visit(syntax.Left), Visit(syntax.Right)) { Grouped = syntax.Grouped };
      
        public virtual UnaryExpression VisitUnary(UnaryExpressionSyntax syntax)
            => new UnaryExpression(syntax.Operator, arg: Visit(syntax.Argument));

        public virtual TernaryExpression VisitTernary(TernaryExpressionSyntax syntax)
        {
            return new TernaryExpression(Visit(syntax.Condition), Visit(syntax.Left), Visit(syntax.Right));
        }

        public virtual CallExpression VisitCall(CallExpressionSyntax syntax)
        {
            Type objectType = null;

            if (char.IsUpper(syntax.Name.Name[0]))
            {
                objectType = scope.GetType(syntax.Name);
            }
           
            return new CallExpression(Visit(syntax.Callee), syntax.Name, VisitArguments(syntax.Arguments), syntax.IsPiped) {
                ReturnType = objectType
            };
        }

        private IArguments VisitArguments(ArgumentSyntax[] arguments)
        {
            if (arguments.Length == 0) return Arguments.None;
            
            var items = new Argument[arguments.Length];

            for (var i = 0; i < items.Length; i++)
            {
                var a = arguments[i];

                items[i] = new Argument(a.Name, Visit(a.Value));
            }

            return Arguments.Create(items);
        }

        public virtual VariableDeclaration VisitVariableDeclaration(VariableDeclarationSyntax variable)
        {
            var value = Visit(variable.Value);
            var type  = GetType(variable.Type ?? value);
            
            scope.Add(variable.Name, type);

            return new VariableDeclaration(variable.Name, type, variable.Flags, value);
        }

        public virtual ObjectInitializer VisitObjectInitializer(ObjectInitializerSyntax syntax)
        {
            var members = new ObjectMember[syntax.Properties.Length];

            for (var i = 0; i < members.Length; i++)
            {
                var m = syntax.Properties[i];

                members[i] = new ObjectMember(m.Name, Visit(m.Value)); 
            }

            return new ObjectInitializer(syntax.Type, members);
        }

        public virtual DestructuringAssignment VisitDestructuringAssignment(DestructuringAssignmentSyntax syntax)
        {
            var elements = new AssignmentElement[syntax.Variables.Length];

            for (var i = 0; i < elements.Length; i++)
            {
                var m = syntax.Variables[i];

                elements[i] = new AssignmentElement(m.Name, Type.Get(Kind.Object));
            }

            return new DestructuringAssignment(elements, Visit(syntax.Instance));
        }
        

        public virtual IndexAccessExpression VisitIndexAccess(IndexAccessExpressionSyntax syntax)
            => new IndexAccessExpression(Visit(syntax.Left), VisitArguments(syntax.Arguments));

        public virtual MemberAccessExpression VisitMemberAccess(MemberAccessExpressionSyntax syntax)
            => new MemberAccessExpression(Visit(syntax.Left), syntax.Name);

        public virtual LambdaExpression VisitLambda(LambdaExpressionSyntax syntax)
            => new LambdaExpression(Visit(syntax.Expression));

        public virtual MatchExpression VisitMatch(MatchExpressionSyntax syntax)
        {
            var cases = new MatchCase[syntax.Cases.Length];

            for (var i = 0; i < cases.Length; i++)
            {
                cases[i] = VisitMatchCase(syntax.Cases[i]);
            }

            return new MatchExpression(Visit(syntax.Expression), cases);
        }

        public virtual MatchCase VisitMatchCase(MatchCaseSyntax syntax)
            => new MatchCase(Visit(syntax.Pattern), Visit(syntax.Condition), VisitLambda(syntax.Body));

        public virtual IfStatement VisitIf(IfStatementSyntax expression)
            => new IfStatement(Visit(expression.Condition), VisitBlock(expression.Body), Visit(expression.ElseBranch));

        public virtual ElseStatement VisitElse(ElseStatementSyntax syntax)
            => new ElseStatement(VisitBlock(syntax.Body));

        public virtual ElseIfStatement VisitElseIf(ElseIfStatementSyntax syntax)
            => new ElseIfStatement(Visit(syntax.Condition), VisitBlock(syntax.Body), Visit(syntax.ElseBranch));

        public virtual ReturnStatement VisitReturn(ReturnStatementSyntax syntax)
            => new ReturnStatement(Visit(syntax.Expression));

        public virtual TypePattern VisitTypePattern(TypePatternSyntax pattern)
            => new TypePattern(pattern.TypeExpression, pattern.VariableName);

        public virtual ConstantPattern VisitConstantPattern(ConstantPatternSyntax pattern)
            => new ConstantPattern(Visit(pattern.Constant));

        public virtual Symbol VisitSymbol(Symbol symbol)
        {
            if (symbol.Status == SymbolStatus.Unresolved)
            {
                if (scope.TryGet<Type>(symbol, out var value))
                {
                    symbol.Initialize(value);
                }
            }

            // Bind... 

            return symbol;
        }

        public Parameter[] ResolveParameters(ParameterSyntax[] parameters)
        {
            var result = new Parameter[parameters.Length];

            // nested function...

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                var type = parameter.Type != null
                    ? scope.Get<Type>(parameter.Type)
                    : new Type(Kind.Object); // TODO: Introduce generic or infer from body?

                // Any

                result[i] = new Parameter(parameter.Name ?? "_" + i, type, false, parameter.DefaultValue);
            }

            return result;
        }
    }
}
