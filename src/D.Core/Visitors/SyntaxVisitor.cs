using System;

namespace D.Syntax
{
    public abstract class SyntaxVisitor
    {
        public virtual SyntaxNode VisitBinary(BinaryExpressionSyntax syntax)                         { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitUnary(UnaryExpressionSyntax syntax)                           { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitTernary(TernaryExpressionSyntax syntax)                       { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitBlock(BlockExpressionSyntax syntax)                           { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitCall(CallExpressionSyntax syntax)                             { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax syntax)         { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitTypeInitializer(NewObjectExpressionSyntax syntax)             { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitDestructuringAssignment(DestructuringAssignmentSyntax syntax) { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitIndexAccess(IndexAccessExpressionSyntax syntax)               { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitMemberAccess(MemberAccessExpressionSyntax syntax)             { throw new NotImplementedException(); }  
        public virtual SyntaxNode VisitLambda(LambdaExpressionSyntax syntax)                         { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitPipe(PipeStatementSyntax syntax)                              { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitMatch(MatchExpressionSyntax syntax)                           { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitIf(IfStatementSyntax syntax)                                  { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitElse(ElseStatementSyntax syntax)                              { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitElseIf(ElseIfStatementSyntax syntax)                          { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitReturn(ReturnStatementSyntax syntax)                          { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitTypePattern(TypePatternSyntax syntax)                         { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitConstantPattern(ConstantPatternSyntax syntax)                 { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitSymbol(Symbol symbol)                                         { throw new NotImplementedException(); }
        public virtual SyntaxNode VisitConstant(IObject constant)                                    { throw new NotImplementedException(); }
        public virtual SyntaxNode FunctionDeclarationSyntax(FunctionDeclarationSyntax syntax)        { throw new NotImplementedException(); }

        public SyntaxNode Visit(SyntaxNode syntax)
        {
            switch (syntax)
            {
                case UnaryExpressionSyntax unary     : return VisitUnary(unary);
                case BinaryExpressionSyntax binary   : return VisitBinary(binary);
                case TernaryExpressionSyntax ternary : return VisitTernary(ternary);
                case BlockExpressionSyntax block     : return VisitBlock(block);
            }
            
            switch (syntax.Kind)
            {
                // Declarations
                case Kind.VariableDeclaration       : return VisitVariableDeclaration((VariableDeclarationSyntax)syntax);
                case Kind.NewObjectExpression       : return VisitTypeInitializer((NewObjectExpressionSyntax)syntax);
 
                case Kind.DestructuringAssignment   : return VisitDestructuringAssignment((DestructuringAssignmentSyntax)syntax);
                    
                case Kind.MemberAccessExpression    : return VisitMemberAccess((MemberAccessExpressionSyntax)syntax);
                   
                case Kind.IndexAccessExpression     : return VisitIndexAccess((IndexAccessExpressionSyntax)syntax);
                                    
                case Kind.LambdaExpression          :  return VisitLambda((LambdaExpressionSyntax)syntax); 
                    
                case Kind.CallExpression            : return VisitCall((CallExpressionSyntax)syntax);
                    
                case Kind.PipeStatement             : return VisitPipe((PipeStatementSyntax)syntax);
                case Kind.MatchExpression           : return VisitMatch((MatchExpressionSyntax)syntax);
                case Kind.IfStatement               : return VisitIf((IfStatementSyntax)syntax);
                case Kind.ElseIfStatement           : return VisitElseIf((ElseIfStatementSyntax)syntax);
                case Kind.ElseStatement             : return VisitElse((ElseStatementSyntax)syntax);
                case Kind.ReturnStatement           : return VisitReturn((ReturnStatementSyntax)syntax);

                // Patterns
                case Kind.ConstantPattern           : return VisitConstantPattern((ConstantPatternSyntax)syntax);
                case Kind.TypePattern               : return  VisitTypePattern((TypePatternSyntax)syntax);
                    

                case Kind.Symbol: return VisitSymbol((Symbol)syntax);
                    
                case Kind.Number:
                case Kind.StringLiteral:
                
                    return VisitConstant(syntax);
                    

                default:
                    throw new Exception("unexpected expression:" + syntax.GetType().Name);

                    /*
                default:
                    writer.Write(expression.ToString()); break;
                    */
            }
        }
    
    }
}
