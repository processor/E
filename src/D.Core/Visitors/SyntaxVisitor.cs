using System;

namespace D.Syntax
{
    public abstract class SyntaxVisitor
    {
        public virtual SyntaxNode VisitBinary(BinaryExpressionSyntax syntax)                         => throw new NotImplementedException();
        public virtual SyntaxNode VisitUnary(UnaryExpressionSyntax syntax)                           => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitTernary(TernaryExpressionSyntax syntax)                       => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitBlock(BlockSyntax syntax)                           => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitCall(CallExpressionSyntax syntax)                             => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitVariableDeclaration(PropertyDeclarationSyntax syntax)         => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitTypeInitializer(ObjectInitializerSyntax syntax)               => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitDestructuringAssignment(DestructuringAssignmentSyntax syntax) => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitIndexAccess(IndexAccessExpressionSyntax syntax)               => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitMemberAccess(MemberAccessExpressionSyntax syntax)             => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitLambda(LambdaExpressionSyntax syntax)                         => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitMatch(MatchExpressionSyntax syntax)                           => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitIf(IfStatementSyntax syntax)                                  => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitElse(ElseStatementSyntax syntax)                              => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitElseIf(ElseIfStatementSyntax syntax)                          => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitReturn(ReturnStatementSyntax syntax)                          => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitTypePattern(TypePatternSyntax syntax)                         => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitConstantPattern(ConstantPatternSyntax syntax)                 => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitSymbol(Symbol symbol)                                         => throw new NotImplementedException(); 
        public virtual SyntaxNode VisitConstant(SyntaxNode constant)                                 => throw new NotImplementedException(); 
        public virtual SyntaxNode FunctionDeclarationSyntax(FunctionDeclarationSyntax syntax)        => throw new NotImplementedException(); 

        public SyntaxNode Visit(SyntaxNode syntax)
        {
            switch (syntax)
            {
                case UnaryExpressionSyntax unary     : return VisitUnary(unary);
                case BinaryExpressionSyntax binary   : return VisitBinary(binary);
                case TernaryExpressionSyntax ternary : return VisitTernary(ternary);
                case BlockSyntax block     : return VisitBlock(block);
            }
            
            switch (syntax.Kind)
            {
                // Declarations
                case SyntaxKind.PropertyDeclaration       : return VisitVariableDeclaration((PropertyDeclarationSyntax)syntax);
                case SyntaxKind.TypeInitializer         : return VisitTypeInitializer((ObjectInitializerSyntax)syntax);
                case SyntaxKind.DestructuringAssignment   : return VisitDestructuringAssignment((DestructuringAssignmentSyntax)syntax);
                case SyntaxKind.MemberAccessExpression    : return VisitMemberAccess((MemberAccessExpressionSyntax)syntax);
                case SyntaxKind.IndexAccessExpression     : return VisitIndexAccess((IndexAccessExpressionSyntax)syntax);          
                case SyntaxKind.LambdaExpression          : return VisitLambda((LambdaExpressionSyntax)syntax); 
                    
                case SyntaxKind.CallExpression            : return VisitCall((CallExpressionSyntax)syntax);
                    
                case SyntaxKind.MatchExpression           : return VisitMatch((MatchExpressionSyntax)syntax);
                case SyntaxKind.IfStatement               : return VisitIf((IfStatementSyntax)syntax);
                case SyntaxKind.ElseIfStatement           : return VisitElseIf((ElseIfStatementSyntax)syntax);
                case SyntaxKind.ElseStatement             : return VisitElse((ElseStatementSyntax)syntax);
                case SyntaxKind.ReturnStatement           : return VisitReturn((ReturnStatementSyntax)syntax);

                // Patterns
                case SyntaxKind.ConstantPattern           : return VisitConstantPattern((ConstantPatternSyntax)syntax);
                case SyntaxKind.TypePattern               : return VisitTypePattern((TypePatternSyntax)syntax);
                    

                case SyntaxKind.Symbol: return VisitSymbol((Symbol)syntax);
                    
                case SyntaxKind.NumberLiteral:
                case SyntaxKind.StringLiteral:
                
                    return VisitConstant(syntax);
                    

                default: throw new Exception("unexpected expression:" + syntax.GetType().Name);
            }
        }
    
    }
}
