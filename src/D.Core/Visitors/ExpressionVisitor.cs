using System;

using D.Symbols;

namespace D.Expressions
{
    public abstract class ExpressionVisitor
    {
        public virtual IExpression VisitBinary(BinaryExpression expression)                         => throw new NotImplementedException();
        public virtual IExpression VisitUnary(UnaryExpression expression)                           => throw new NotImplementedException();
        public virtual IExpression VisitTernary(TernaryExpression expression)                       => throw new NotImplementedException();
        public virtual IExpression VisitBlock(BlockExpression block)                                => throw new NotImplementedException();
        public virtual IExpression VisitCall(CallExpression call)                                   => throw new NotImplementedException();
        public virtual IExpression VisitVariableDeclaration(VariableDeclaration declaration)        => throw new NotImplementedException();
        public virtual IExpression VisitTypeInitializer(TypeInitializer initializer)                => throw new NotImplementedException();
        public virtual IExpression VisitDestructuringAssignment(DestructuringAssignment assignment) => throw new NotImplementedException();
        public virtual IExpression VisitIndexAccess(IndexAccessExpression expression)               => throw new NotImplementedException();
        public virtual IExpression VisitMemberAccess(MemberAccessExpression expression)             => throw new NotImplementedException();
        public virtual IExpression VisitLambda(LambdaExpression expression)                         => throw new NotImplementedException();
        public virtual IExpression VisitMatch(MatchExpression expression)                           => throw new NotImplementedException();
        public virtual IExpression VisitIf(IfStatement expression)                                  => throw new NotImplementedException();
        public virtual IExpression VisitElse(ElseStatement expression)                              => throw new NotImplementedException();
        public virtual IExpression VisitElseIf(ElseIfStatement expression)                          => throw new NotImplementedException();
        public virtual IExpression VisitReturn(ReturnStatement expression)                          => throw new NotImplementedException();
        public virtual IExpression VisitTypePattern(TypePattern pattern)                            => throw new NotImplementedException();
        public virtual IExpression VisitConstantPattern(ConstantPattern pattern)                    => throw new NotImplementedException();
        public virtual IExpression VisitSymbol(Symbol symbol)                                       => throw new NotImplementedException();
        public virtual IExpression VisitConstant(IExpression expression)                            => throw new NotImplementedException();
        public virtual IExpression VisitFor(ForStatement expression)                                => throw new NotImplementedException();
        public virtual IExpression VisitFunction(FunctionExpression function)                       => throw new NotImplementedException();
        public virtual IExpression VisitTypeDeclaration(TypeDeclaration declaration)                => throw new NotImplementedException();

        public IExpression Visit(IObject expression)
        {
            switch (expression.Kind)
            {
                // Declarations
                case ObjectType.PropertyDeclaration     : return VisitVariableDeclaration((VariableDeclaration)expression);
                case ObjectType.Function                : return VisitFunction((FunctionExpression)expression);
                case ObjectType.TypeDeclaration         : return VisitTypeDeclaration((TypeDeclaration)expression);
                case ObjectType.TypeInitializer         : return VisitTypeInitializer((TypeInitializer)expression);
                case ObjectType.DestructuringAssignment : return VisitDestructuringAssignment((DestructuringAssignment)expression);
                case ObjectType.CallExpression          : return VisitCall((CallExpression)expression);
                case ObjectType.MatchExpression         : return VisitMatch((MatchExpression)expression);
                case ObjectType.MemberAccessExpression  : return VisitMemberAccess((MemberAccessExpression)expression);
                case ObjectType.IndexAccessExpression   : return VisitIndexAccess((IndexAccessExpression)expression);
                case ObjectType.LambdaExpression        : return VisitLambda((LambdaExpression)expression); 

                // Statements
                case ObjectType.BlockStatement          : return VisitBlock((BlockExpression)expression);
                case ObjectType.ForStatement            : return VisitFor((ForStatement)expression);     
                case ObjectType.IfStatement             : return VisitIf((IfStatement)expression);
                case ObjectType.ElseIfStatement         : return VisitElseIf((ElseIfStatement)expression);
                case ObjectType.ElseStatement           : return VisitElse((ElseStatement)expression);
                case ObjectType.ReturnStatement         : return VisitReturn((ReturnStatement)expression);

                // Patterns
                case ObjectType.ConstantPattern         : return VisitConstantPattern((ConstantPattern)expression);
                case ObjectType.TypePattern             : return VisitTypePattern((TypePattern)expression);

                case ObjectType.Symbol                  : return VisitSymbol((Symbol)expression);

                case ObjectType.Number:
                case ObjectType.Int64:
                case ObjectType.String                  : return VisitConstant((IExpression)expression);
            }

            return expression switch
            {
                UnaryExpression unary     => VisitUnary(unary),
                BinaryExpression binary   => VisitBinary(binary),
                TernaryExpression ternary => VisitTernary(ternary),

                _ => throw new Exception("Unexpected expression:" + expression.GetType().Name),
            };
        }
    }
}
