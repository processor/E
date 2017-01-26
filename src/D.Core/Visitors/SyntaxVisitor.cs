using System;

namespace D.Syntax
{
    public abstract class SyntaxVisitor
    {
        public virtual void VisitBinary(BinaryExpressionSyntax expression)                         { throw new NotImplementedException(); }
        public virtual void VisitUnary(UnaryExpressionSyntax expression)                           { throw new NotImplementedException(); }
        public virtual void VisitTernary(TernaryExpressionSyntax expression)                       { throw new NotImplementedException(); }
        public virtual void VisitBlock(BlockStatementSyntax block)                                 { throw new NotImplementedException(); }
        public virtual void VisitCall(CallExpressionSyntax call)                                   { throw new NotImplementedException(); }
        public virtual void VisitVariableDeclaration(VariableDeclarationSyntax declaration)        { throw new NotImplementedException(); }
        public virtual void VisitTypeInitializer(TypeInitializerSyntax a)                          { throw new NotImplementedException(); }
        public virtual void VisitDestructuringAssignment(DestructuringAssignmentSyntax assignment) { throw new NotImplementedException(); }
        public virtual void VisitIndexAccess(IndexAccessExpressionSyntax expression)               { throw new NotImplementedException(); }
        public virtual void VisitMemberAccess(MemberAccessExpressionSyntax expression)             { throw new NotImplementedException(); }  
        public virtual void VisitLambda(LambdaExpressionSyntax expression)                         { throw new NotImplementedException(); }
        public virtual void VisitPipe(PipeStatementSyntax pipe)                                    { throw new NotImplementedException(); }
        public virtual void VisitMatch(MatchExpressionSyntax expression)                           { throw new NotImplementedException(); }
        public virtual void VisitIf(IfStatementSyntax expression)                                  { throw new NotImplementedException(); }
        public virtual void VisitElse(ElseStatementSyntax expression)                              { throw new NotImplementedException(); }
        public virtual void VisitElseIf(ElseIfStatementSyntax expression)                          { throw new NotImplementedException(); }
        public virtual void VisitReturn(ReturnStatementSyntax expression)                          { throw new NotImplementedException(); }
        public virtual void VisitTypePattern(TypePatternSyntax pattern)                            { throw new NotImplementedException(); }
        public virtual void VisitConstantPattern(ConstantPatternSyntax pattern)                    { throw new NotImplementedException(); }
        public virtual void VisitSymbol(Symbol symbol)                                             { throw new NotImplementedException(); }
        public virtual void VisitConstant(IObject constant)                                        { throw new NotImplementedException(); }
        public virtual void FunctionDeclarationSyntax(FunctionDeclarationSyntax function)          { throw new NotImplementedException(); }

        public void Visit(IObject expression)
        {
            if (expression is UnaryExpressionSyntax)
            {
                VisitUnary((UnaryExpressionSyntax)expression);

                return;
            }
            else if (expression is BinaryExpressionSyntax)
            {
                VisitBinary((BinaryExpressionSyntax)expression);

                return;
            }
            else if (expression is TernaryExpressionSyntax)
            {
                VisitTernary((TernaryExpressionSyntax)expression);

                return;
            }

            if (expression is BlockStatementSyntax)
            {
                VisitBlock((BlockStatementSyntax)expression);

                return;
            }

            switch (expression.Kind)
            {
                // Declarations
                case Kind.VariableDeclaration:
                    VisitVariableDeclaration((VariableDeclarationSyntax)expression);

                    break;
                case Kind.TypeInitializer:
                    VisitTypeInitializer((TypeInitializerSyntax)expression);
                    break;
                case Kind.DestructuringAssignment:
                    VisitDestructuringAssignment((DestructuringAssignmentSyntax)expression);
                    break;

                case Kind.MemberAccessExpression:
                    VisitMemberAccess((MemberAccessExpressionSyntax)expression);
                    break;
                case Kind.IndexAccessExpression:
                    VisitIndexAccess((IndexAccessExpressionSyntax)expression);
                    break;
                case Kind.LambdaExpression:
                    VisitLambda((LambdaExpressionSyntax)expression); break;

                // Statements
                case Kind.CallExpression:
                    VisitCall((CallExpressionSyntax)expression);
                    break;
                case Kind.PipeStatement:
                    VisitPipe((PipeStatementSyntax)expression);
                    break;

                case Kind.MatchExpression:
                    VisitMatch((MatchExpressionSyntax)expression);
                    break;
                case Kind.IfStatement:
                    VisitIf((IfStatementSyntax)expression);
                    break;
                case Kind.ElseIfStatement:

                    VisitElseIf((ElseIfStatementSyntax)expression);
                    break;

                case Kind.ElseStatement:
                    VisitElse((ElseStatementSyntax)expression);
                    break;
                case Kind.ReturnStatement:
                    VisitReturn((ReturnStatementSyntax)expression);
                    break;

                // Patterns
                case Kind.ConstantPattern:
                    VisitConstantPattern((ConstantPatternSyntax)expression);
                    break;
                case Kind.TypePattern:
                    VisitTypePattern((TypePatternSyntax)expression);
                    break;

                case Kind.Symbol:
                    VisitSymbol((Symbol)expression);
                    break;

                case Kind.Number:
                case Kind.StringLiteral:
                
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
