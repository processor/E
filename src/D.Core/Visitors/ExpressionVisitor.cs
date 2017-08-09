using System;

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
        public virtual IExpression VisitTypeInitializer(TypeInitializer initializer)              => throw new NotImplementedException();
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

        public IExpression Visit(IObject expression)
        {
            switch (expression)
            {
                case UnaryExpression unary     : return VisitUnary(unary);
                case BinaryExpression binary   : return VisitBinary(binary);
                case TernaryExpression ternary : return VisitTernary(ternary);
            }

            switch (expression.Kind)
            {
                // Declarations
                case Kind.VariableDeclaration     : return VisitVariableDeclaration((VariableDeclaration)expression);
                    
                case Kind.TypeInitializer       : return VisitTypeInitializer((TypeInitializer)expression);
                case Kind.DestructuringAssignment : return VisitDestructuringAssignment((DestructuringAssignment)expression);

                case Kind.CallExpression          : return VisitCall((CallExpression)expression);
                case Kind.MatchExpression         : return VisitMatch((MatchExpression)expression);
                case Kind.MemberAccessExpression  : return VisitMemberAccess((MemberAccessExpression)expression);
                case Kind.IndexAccessExpression   : return VisitIndexAccess((IndexAccessExpression)expression);
                case Kind.LambdaExpression        : return VisitLambda((LambdaExpression)expression); 

                // Statements
                case Kind.BlockStatement          : return VisitBlock((BlockExpression)expression);
                case Kind.IfStatement             : return VisitIf((IfStatement)expression);
                case Kind.ElseIfStatement         : return VisitElseIf((ElseIfStatement)expression);
                case Kind.ElseStatement           : return VisitElse((ElseStatement)expression);
                case Kind.ReturnStatement         : return VisitReturn((ReturnStatement)expression);

                // Patterns
                case Kind.ConstantPattern         : return VisitConstantPattern((ConstantPattern)expression);
                case Kind.TypePattern             : return VisitTypePattern((TypePattern)expression);

                case Kind.Symbol                  : return VisitSymbol((Symbol)expression);

                case Kind.Number:
                case Kind.Int64:
                case Kind.String                  : return VisitConstant((IExpression)expression);

                default                           : throw new Exception("unexpected expression:" + expression.GetType().Name);
            }
        }
    
    }
}
