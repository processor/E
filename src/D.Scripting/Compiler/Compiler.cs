using System;
using System.Collections.Generic;

namespace D.Compilation
{
    using Syntax;
    using Expressions;

    public partial class Compiler
    {
        // Phases:
        // - Parse
        // - Bind
        // - Emit

        private Scope scope = new Scope();

        public CompliationUnit Compile(ISyntax[] nodes)
        {
            var unit = new CompliationUnit();
            
            foreach (var node in nodes)
            {
                if (node is FunctionDeclarationSyntax)
                {
                    var function = VisitFunctionDeclaration((FunctionDeclarationSyntax)node);

                    unit.Functions.Add(function);

                    if (function.Name != null)
                    {
                        scope.Add(function.Name, function);
                    }
                }
                else if (node is TypeDeclarationSyntax)
                {
                    var type = VisitType((TypeDeclarationSyntax)node);

                    unit.Types.Add(VisitType((TypeDeclarationSyntax)node));

                    scope.Add(type.Name, type);
                }
                else if (node is ProtocalDeclarationSyntax)
                {
                    var protocal = VisitProtocal((ProtocalDeclarationSyntax)node);

                    unit.Protocals.Add(protocal);

                    scope.Add(protocal.Name, protocal);
                }
                else if (node is ImplementationDeclarationSyntax)
                {
                    var impl = VisitImplementation((ImplementationDeclarationSyntax)node);

                    List<Implementation> list;

                    if (!unit.Implementations.TryGetValue(impl.Type, out list))
                    {
                        list = new List<Implementation>();

                        unit.Implementations[impl.Type] = list;
                    }

                    list.Add(impl);
                }
            }

            return unit;
        }

        public Protocal VisitProtocal(ProtocalDeclarationSyntax protocal)
        {
            var functions = new Function[protocal.Members.Length];

            for (var i = 0; i < functions.Length; i++)
            {
                functions[i] = VisitFunctionDeclaration(protocal.Members[i]);
            }

            return new Protocal(protocal.Name, functions);
        }
        
        public Function VisitFunctionDeclaration(FunctionDeclarationSyntax f, IType declaringType = null)
        {
            var b = Visit(f.Body);

            var returnType = f.ReturnType != null 
                ? scope.Get<Type>(f.ReturnType) 
                : b.Kind == Kind.LambdaExpression
                    ? GetType((LambdaExpression)b)
                    : GetReturnType((BlockExpression)b);

            var paramaters = ResolveParameters(f.Parameters);

            foreach (var p in paramaters)
            {
                scope.Add(p.Name, (Type)p.Type);
            }

            BlockExpression body;

            if (f.Body == null) 
            {
                body = null; // Protocal functions do not define a body.
            }
            else if (f.Body is BlockExpressionSyntax)
            {
                body = VisitBlock((BlockExpressionSyntax)f.Body);
            }
            else if (f.Body is LambdaExpressionSyntax)
            {
                var lambda = VisitLambda((LambdaExpressionSyntax)f.Body);

                body = new BlockExpression(new ReturnStatement(lambda.Expression));
            }
            else
            {
                throw new Exception("unexpected function body type:" + f.Body.Kind);
            }

            var function = new Function(f.Name, returnType, paramaters);

            function.GenericParameters = ResolveParameters(f.GenericParameters);
            function.Flags             = f.Flags;
            function.Body              = body;
            function.DeclaringType     = declaringType;

            return function;
        }

        public Implementation VisitImplementation(ImplementationDeclarationSyntax impl)
        {
            scope = scope.Nested();

            var type = scope.Get<Type>(impl.Type);
            var protocal = impl.Protocal != null ? scope.Get<Protocal>(impl.Protocal) : null;

            #region Setup environment

            scope.Add("this", type);

            if (type.Properties != null)
            {
                foreach (var property in type.Properties)
                {
                    scope.Add(property.Name, (Type)property.Type);
                }
            }

            #endregion

            var methods    = new List<Function>();
            var variables  = new List<VariableDeclaration>();

            foreach(var member in impl.Members)
            {
                switch (member.Kind)
                {
                    case Kind.FunctionDeclaration: methods.Add(VisitFunctionDeclaration((FunctionDeclarationSyntax)member, type));  break;
                    case Kind.VariableDeclaration: variables.Add(VisitVariableDeclaration((VariableDeclarationSyntax)member));      break;
                }

                // Property       a =>
                // Initializer    from  
                // Converter      to
                // Operator       *
                // Method         a () =>
            }

            scope = scope.Parent;

            return new Implementation(protocal, type, variables.ToArray(), methods.ToArray());
        }

        public Type VisitType(TypeDeclarationSyntax type)
        {
            var genericParameters = new Parameter[type.GenericParameters.Length];

            for (var i = 0; i < type.GenericParameters.Length; i++)
            {
                var member = type.GenericParameters[i];

                genericParameters[i] = new Parameter(member.Name, scope.Get<Type>(member.Type ?? Symbol.Any));

                // context.Add(member.Name, (Type)genericParameters[i].Type);
            }

            var properties = new Property[type.Members.Length];

            for (var i = 0; i < type.Members.Length; i++)
            {
                var member = type.Members[i];

                properties[i] = new Property(member.Name, scope.Get<Type>(member.Type), member.IsMutable);
            }

            var baseType = type.BaseType != null
                ? scope.Get<Type>(type.BaseType)
                : null;

            return new Type(type.Name, baseType, properties, genericParameters);
        }

        #region Binding

        public BlockExpression VisitBlock(BlockExpressionSyntax syntax)
        {
            var statements = new List<IExpression>();

            foreach(var s in syntax.Statements)
            {
                var expression = Visit(s);
              
                statements.Add(expression);
            }

            return new BlockExpression(statements.ToArray());
        }

        int i = 0;

        public IExpression Visit(ISyntax syntax)
        {
            i++;

            if (i > 300) throw new Exception("rerucssion???");

            if (syntax == null) return null;

            if (syntax is UnaryExpressionSyntax)
            {
                return VisitUnary((UnaryExpressionSyntax)syntax);
            }
            else if (syntax is BinaryExpressionSyntax)
            {
                return VisitBinary((BinaryExpressionSyntax)syntax);
            }
            else if (syntax is TernaryExpressionSyntax)
            {
                return VisitTernary((TernaryExpressionSyntax)syntax);
            }

            if (syntax is BlockExpressionSyntax)
            {
                return VisitBlock((BlockExpressionSyntax)syntax);
            }

            switch (syntax.Kind)
            {
                case Kind.LambdaExpression:
                    return VisitLambda((LambdaExpressionSyntax)syntax);

                case Kind.InterpolatedStringExpression:  return VisitInterpolatedStringExpression((InterpolatedStringExpressionSyntax)syntax);

                // Declarations
                case Kind.VariableDeclaration     : return VisitVariableDeclaration((VariableDeclarationSyntax)syntax);
                case Kind.TypeInitializer         : return VisitTypeInitializer((TypeInitializerSyntax)syntax);
                case Kind.DestructuringAssignment : return VisitDestructuringAssignment((DestructuringAssignmentSyntax)syntax);
                case Kind.MemberAccessExpression  : return VisitMemberAccess((MemberAccessExpressionSyntax)syntax);
                case Kind.IndexAccessExpression   : return VisitIndexAccess((IndexAccessExpressionSyntax)syntax);

                // Statements
                case Kind.PipeStatement           : return VisitPipe((PipeStatementSyntax)syntax);
                case Kind.CallExpression          : return VisitCall((CallExpressionSyntax)syntax);
                case Kind.MatchExpression         : return VisitMatch((MatchExpressionSyntax)syntax);
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
                
                case Kind.ArrayLiteral            : return VisitArrayLiteral((ArrayLiteralSyntax)syntax);
                case Kind.MatrixLiteral           : return VisitMatrixLiteral((MatrixLiteralSyntax)syntax);
                case Kind.StringLiteral           : return new StringLiteral(syntax.ToString());
            }

            throw new Exception("Unexpected node:" + syntax.Kind + "/" + syntax.GetType().ToString());
        }
        
        public MatrixLiteral VisitMatrixLiteral(MatrixLiteralSyntax syntax)
        {
            var elements = new IObject[syntax.Elements.Length];

            for (var i = 0; i < elements.Length; i++)
            {
                elements[i] = Visit(syntax.Elements[i]);
            }

            return new MatrixLiteral(elements, syntax.Stride);
        }

        public ArrayLiteral VisitArrayLiteral(ArrayLiteralSyntax syntax)
        {
            var elements = new IExpression[syntax.Elements.Count];

            for (var i = 0; i < elements.Length; i++)
            {
                elements[i] = Visit(syntax.Elements[i]);
            }

            return new ArrayLiteral(elements);
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
                return new Float(double.Parse(expression.Text));
            }
            else
            {
                return new Integer(long.Parse(expression.Text));
            }
        }

        public virtual PipeStatement VisitPipe(PipeStatementSyntax syntax)
            => new PipeStatement(Visit(syntax.Callee), Visit(syntax.Expression));

        public virtual BinaryExpression VisitBinary(BinaryExpressionSyntax syntax)
            => new BinaryExpression(syntax.Operator, Visit(syntax.Left), Visit(syntax.Right)) { Grouped = syntax.Grouped };
      
        public virtual UnaryExpression VisitUnary(UnaryExpressionSyntax syntax)
            => new UnaryExpression(syntax.Operator, arg: Visit(syntax.Argument));

        public virtual TernaryExpression VisitTernary(TernaryExpressionSyntax syntax)
            => new TernaryExpression(Visit(syntax.Condition), Visit(syntax.Left), Visit(syntax.Right));

        public virtual CallExpression VisitCall(CallExpressionSyntax syntax)
            => new CallExpression(Visit(syntax.Callee), syntax.FunctionName, VisitArguments(syntax.Arguments));

        private IArguments VisitArguments(ArgumentSyntax[] arguments)
        {
            if (arguments.Length == 0) return Arguments.None;
            
            var items = new IObject[arguments.Length];

            for (var i = 0; i < items.Length; i++)
            {
                // TODO: Named arg support
                items[i] = Visit(arguments[i].Value);
            }

            return Arguments.Create(items);
            
        }

        public virtual VariableDeclaration VisitVariableDeclaration(VariableDeclarationSyntax syntax)
        {
            var value = Visit(syntax.Value);
            var type = GetType(syntax.Type ?? value);

            return new VariableDeclaration(syntax.Name, type, syntax.IsMutable, value);
        }

        public virtual TypeInitializer VisitTypeInitializer(TypeInitializerSyntax syntax)
        {
            var members = new RecordMember[syntax.Members.Length];

            for (var i = 0; i < members.Length; i++)
            {
                var m = syntax.Members[i];

                members[i] = new RecordMember(m.Name, Visit(m.Value)); 
            }

            return new TypeInitializer(syntax.Type, members);
        }

        public virtual DestructuringAssignment VisitDestructuringAssignment(DestructuringAssignmentSyntax syntax)
        {
            var elements = new AssignmentElement[syntax.Variables.Length];

            for (var i = 0; i < elements.Length; i++)
            {
                var m = syntax.Variables[i];

                elements[i] = new AssignmentElement(m.Name, Type.Get(Kind.Any));
            }

            return new DestructuringAssignment(elements, Visit(syntax.Instance));
        }
        

        public virtual IndexAccessExpression VisitIndexAccess(IndexAccessExpressionSyntax syntax)
            => new IndexAccessExpression(Visit(syntax.Left), VisitArguments(syntax.Arguments));

        public virtual MemberAccessExpression VisitMemberAccess(MemberAccessExpressionSyntax syntax)
            => new MemberAccessExpression(Visit(syntax.Left), syntax.MemberName);

        public virtual LambdaExpression VisitLambda(LambdaExpressionSyntax syntax)
            => new LambdaExpression(Visit(syntax.Expression));

        public virtual MatchExpression VisitMatch(MatchExpressionSyntax syntax)
        {
            var cases = new MatchCase[syntax.Cases.Count];

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
            => symbol;

        public Parameter[] ResolveParameters(ParameterSyntax[] parameters)
        {
            var result = new Parameter[parameters.Length];

            // nested function...

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                var type = parameter.Type != null
                    ? scope.Get<Type>(parameter.Type)
                    : new Type(Kind.Any);                 // TODO: Introduce generic or infer from body?

                // Any

                result[i] = new Parameter(parameter.Name ?? "_" + i, type, false, parameter.DefaultValue);
            }

            return result;
        }

        #endregion

        #region Inference

       

        #endregion
    }
}
