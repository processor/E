using System;
using System.Collections.Generic;

namespace D.Compilation
{
    using Expressions;

    public class Compiler
    {
        private CompliationContext context = new CompliationContext();

        public CompliationUnit Compile(IExpression[] expressions)
        {
            var unit = new CompliationUnit();
            
            foreach (var node in expressions)
            {
                if (node is FunctionDeclaration)
                {
                    var function = VisitFunction((FunctionDeclaration)node);

                    unit.Functions.Add(function);

                    context.Add(function.Name, function);
                }
                else if (node is TypeDeclaration)
                {
                    var type = VisitType((TypeDeclaration)node);

                    unit.Types.Add(VisitType((TypeDeclaration)node));

                    context.Add(type.Name, type);
                }
                else if (node is ProtocalDeclaration)
                {
                    var protocal = VisitProtocal((ProtocalDeclaration)node);

                    unit.Protocals.Add(protocal);

                    context.Add(protocal.Name, protocal);
                }
                else if (node is ImplementationDeclaration)
                {
                    var implementation = VisitImplementation((ImplementationDeclaration)node);

                    List<Implementation> list;

                    if (!unit.Implementations.TryGetValue(implementation.Type, out list))
                    {
                        list = new List<Implementation>();

                        unit.Implementations[implementation.Type] = list;
                    }

                    list.Add(implementation);
                }
            }

            return unit;
        }

        public Protocal VisitProtocal(ProtocalDeclaration protocal)
        {
            // TODO: Bind all the members

            var functions = new Function[protocal.Members.Length];

            for (var i = 0; i < functions.Length; i++)
            {
                functions[i] = VisitFunction(protocal.Members[i]);
            }

            return new Protocal(protocal.Name, functions);
        }
        
        public Function VisitFunction(FunctionDeclaration f, IType declaringType = null)
        {
            var returnType = f.ReturnType != null 
                ? context.Get<Type>(f.ReturnType) 
                : f.Body.Kind == Kind.LambdaExpression
                    ? GetType((LambdaExpression)f.Body)
                    : GetReturnType((BlockStatement)f.Body);

            var paramaters = ResolveParameters(f.Parameters);

            foreach (var p in paramaters)
            {
                context.Add(p.Name, (Type)p.Type);
            }

            BlockStatement body;

            if (f.Body == null)
            {
                body = null;
            }
            else if (f.Body is BlockStatement)
            {
                body = VisitBlock((BlockStatement)f.Body);
            }
            else if (f.Body is LambdaExpression)
            {
                var lambda = (LambdaExpression)f.Body;

                body = new BlockStatement(new ReturnStatement(lambda.Expression));
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

        public Implementation VisitImplementation(ImplementationDeclaration implementation)
        {
            context = context.Nested();

            var type = context.Get<Type>(implementation.Type);
            var protocal = implementation.Protocal != null ? context.Get<Protocal>(implementation.Protocal) : null;

            #region Setup environment

            context.Add("this", type);

            if (type.Properties != null)
            {
                foreach (var property in type.Properties)
                {
                    context.Add(property.Name, (Type)property.Type);
                }
            }

            #endregion

            var methods    = new List<Function>();
            var variables  = new List<Variable>();

            foreach(var member in implementation.Members)
            {
                if (member is FunctionDeclaration)
                {
                    methods.Add(VisitFunction((FunctionDeclaration)member, type));
                }
                else if (member is VariableDeclaration)
                {
                    variables.Add(VisitVariable((VariableDeclaration)member));
                }

                // Property       a =>
                // Initializer    from  
                // Converter      to
                // Operator       *
                // Method         a () =>
            }

            context = context.Parent;

            return new Implementation(protocal, type, variables.ToArray(), methods.ToArray());
        }

        public Type VisitType(TypeDeclaration type)
        {
            var genericParameters = new Parameter[type.GenericParameters.Length];

            for (var i = 0; i < type.GenericParameters.Length; i++)
            {
                var member = type.GenericParameters[i];

                genericParameters[i] = new Parameter(member.Name, context.Get<Type>(member.Type ?? Symbol.Any));

                // context.Add(member.Name, (Type)genericParameters[i].Type);
            }

            var properties = new Property[type.Members.Length];

            for (var i = 0; i < type.Members.Length; i++)
            {
                var member = type.Members[i];

                properties[i] = new Property(member.Name, context.Get<Type>(member.Type), member.IsMutable);
            }

            var baseType = type.BaseType != null
                ? context.Get<Type>(type.BaseType)
                : null;

            return new Type(type.Name, baseType, properties, genericParameters);
        }

        #region Binding

        public BlockStatement VisitBlock(BlockStatement block)
        {
            var statements = new List<IExpression>();

            foreach(var s in block.Statements)
            {
                /*
                IExpression statement = s

                switch (s.Kind)
                {
                    case Kind.Function: statement = VisitFunction((FunctionDeclaration)s); break;
                    default: 
                }
                */

                statements.Add(s);



                // TODO: Bind and replace variables
            }

            return new BlockStatement(statements.ToArray());
        }

        public Parameter[] ResolveParameters(ParameterExpression[] parameters)
        {
            var result = new Parameter[parameters.Length];

            // nested function...

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                var type = parameter.Type != null
                    ? context.Get<Type>(parameter.Type)
                    : new Type(Kind.Any);                 // TODO: Introduce generic or infer from body?

                // Any

                result[i] = new Parameter(parameter.Name ?? "_" + i, type, false, parameter.DefaultValue);
            }

            return result;
        }

        #endregion

        #region Inference

        public Variable VisitVariable(VariableDeclaration variable)
        {

            var type = GetType(variable.Type ?? variable.Value);

            return new Variable(type, variable.Value, variable.IsMutable);
        }

        public Type GetReturnType(BlockStatement block)
        {
            foreach (var x in block.Statements)
            {
                if (x is ReturnStatement)
                {
                    var returnStatement = (ReturnStatement)x;

                    return GetType(returnStatement.Expression);
                }
            }

            throw new Exception("Block has no return statement");
        }

        public Type GetType(IExpression expression)
        {
            if (expression is Symbol)
            {
                IObject obj;

                if (context.TryGet((Symbol)expression, out obj))
                {
                    return (Type)obj;
                }
            }

            switch (expression.Kind)
            {
                case Kind.Integer                       : return Type.Get(Kind.Integer);
                case Kind.Float                         : return Type.Get(Kind.Float);
                case Kind.Decimal                       : return Type.Get(Kind.Decimal);
                case Kind.String                        : return Type.Get(Kind.String);
                case Kind.InterpolatedStringExpression  : return Type.Get(Kind.String);
                case Kind.MatrixLiteral                 : return Type.Get(Kind.Matrix);
            }

            if (expression is BinaryExpression || expression is Symbol || expression is CallExpression || expression is UnaryExpression)
            {
                // TODO: Infer
                return Type.Get(Kind.Any);
            }

            if (expression.Kind == Kind.TypeInitializer)
            {
                var initializer = (TypeInitializer)expression;

                return context.Get<Type>(initializer.Type);
            }

            // Any
            if (expression is IndexAccessExpression) return Type.Get(Kind.Any);

            throw new Exception("Unexpected expression:" + expression.Kind + "/" + expression.ToString());
        }

        public Type GetType(LambdaExpression lambda)
        {
            return GetType(lambda.Expression);
        }

        #endregion
    }
}
