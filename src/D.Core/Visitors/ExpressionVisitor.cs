using System;

namespace D.Expressions
{
    public abstract class ExpressionVisitor
    {
        public virtual void VisitBinary(BinaryExpression expression)                         { throw new NotImplementedException(); }
        public virtual void VisitUnary(UnaryExpression expression)                           { throw new NotImplementedException(); }
        public virtual void VisitTernary(TernaryExpression expression)                       { throw new NotImplementedException(); }
        public virtual void VisitBlock(BlockStatement block)                                 { throw new NotImplementedException(); }
        public virtual void VisitCall(CallExpression call)                                   { throw new NotImplementedException(); }
        public virtual void VisitVariableDeclaration(VariableDeclaration declaration)        { throw new NotImplementedException(); }
        public virtual void VisitTypeInitializer(TypeInitializer a)                          { throw new NotImplementedException(); }
        public virtual void VisitDestructuringAssignment(DestructuringAssignment assignment) { throw new NotImplementedException(); }
        public virtual void VisitIndexAccess(IndexAccessExpression expression)               { throw new NotImplementedException(); }
        public virtual void VisitMemberAccess(MemberAccessExpression expression)             { throw new NotImplementedException(); }  
        public virtual void VisitLambda(LambdaExpression expression)                         { throw new NotImplementedException(); }
        public virtual void VisitPipe(PipeStatement pipe)                                    { throw new NotImplementedException(); }
        public virtual void VisitMatch(MatchExpression expression)                           { throw new NotImplementedException(); }
        public virtual void VisitIf(IfStatement expression)                                  { throw new NotImplementedException(); }
        public virtual void VisitElse(ElseStatement expression)                              { throw new NotImplementedException(); }
        public virtual void VisitElseIf(ElseIfStatement expression)                          { throw new NotImplementedException(); }
        public virtual void VisitReturn(ReturnStatement expression)                          { throw new NotImplementedException(); }
        public virtual void VisitTypePattern(TypePattern pattern)                            { throw new NotImplementedException(); }
        public virtual void VisitConstantPattern(ConstantPattern pattern)                    { throw new NotImplementedException(); }
        public virtual void VisitSymbol(Symbol symbol)                                       { throw new NotImplementedException(); }
        public virtual void VisitConstant(IObject constant)                                  { throw new NotImplementedException(); }

        public void Visit(IObject expression)
        {
            if (expression is UnaryExpression)
            {
                VisitUnary((UnaryExpression)expression);

                return;
            }
            else if (expression is BinaryExpression)
            {
                VisitBinary((BinaryExpression)expression);

                return;
            }
            else if (expression is TernaryExpression)
            {
                VisitTernary((TernaryExpression)expression);

                return;
            }

            if (expression is BlockStatement)
            {
                VisitBlock((BlockStatement)expression);

                return;
            }

            switch (expression.Kind)
            {
                // Declarations
                case Kind.VariableDeclaration:
                    VisitVariableDeclaration((VariableDeclaration)expression);

                    break;
                case Kind.TypeInitializer:
                    VisitTypeInitializer((TypeInitializer)expression);
                    break;
                case Kind.DestructuringAssignment:
                    VisitDestructuringAssignment((DestructuringAssignment)expression);
                    break;

                case Kind.MemberAccessExpression:
                    VisitMemberAccess((MemberAccessExpression)expression);
                    break;
                case Kind.IndexAccessExpression:
                    VisitIndexAccess((IndexAccessExpression)expression);
                    break;
                case Kind.LambdaExpression:
                    VisitLambda((LambdaExpression)expression); break;

                // Statements
                case Kind.CallExpression:
                    VisitCall((CallExpression)expression);
                    break;
                case Kind.PipeStatement:
                    VisitPipe((PipeStatement)expression);
                    break;

                case Kind.MatchStatement:
                    VisitMatch((MatchExpression)expression);
                    break;
                case Kind.IfStatement:
                    VisitIf((IfStatement)expression);
                    break;
                case Kind.ElseIfStatement:

                    VisitElseIf((ElseIfStatement)expression);
                    break;

                case Kind.ElseStatement:
                    VisitElse((ElseStatement)expression);
                    break;
                case Kind.ReturnStatement:
                    VisitReturn((ReturnStatement)expression);
                    break;

                // Patterns
                case Kind.ConstantPattern:
                    VisitConstantPattern((ConstantPattern)expression);
                    break;
                case Kind.TypePattern:
                    VisitTypePattern((TypePattern)expression);
                    break;

                case Kind.Symbol:
                    VisitSymbol((Symbol)expression);
                    break;

                case Kind.Integer:
                case Kind.String:
                case Kind.Float:
                    VisitConstant(expression);
                    break;

                default:
                    throw new Exception("unexpected expression:" + expression.GetType().Name);

                    /*
                default:
                    writer.Write(expression.ToString()); break;
                    */
            }
        }
    
    }
}
