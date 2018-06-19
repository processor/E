using System;

namespace D.Syntax
{
    public abstract class SyntaxVisitor
    {
        public virtual ISyntaxNode VisitBinary(BinaryExpressionSyntax syntax)                         => throw new NotImplementedException();
        public virtual ISyntaxNode VisitUnary(UnaryExpressionSyntax syntax)                           => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitTernary(TernaryExpressionSyntax syntax)                       => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitBlock(BlockSyntax syntax)                                     => throw new NotImplementedException();
        public virtual ISyntaxNode VisitModule(ModuleSyntax syntax)                                   => throw new NotImplementedException();
        public virtual ISyntaxNode VisitCall(CallExpressionSyntax syntax)                             => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitVariableDeclaration(PropertyDeclarationSyntax syntax)         => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitTypeInitializer(ObjectInitializerSyntax syntax)               => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitDestructuringAssignment(DestructuringAssignmentSyntax syntax) => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitIndexAccess(IndexAccessExpressionSyntax syntax)               => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitMemberAccess(MemberAccessExpressionSyntax syntax)             => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitLambda(LambdaExpressionSyntax syntax)                         => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitMatch(MatchExpressionSyntax syntax)                           => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitIf(IfStatementSyntax syntax)                                  => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitElse(ElseStatementSyntax syntax)                              => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitElseIf(ElseIfStatementSyntax syntax)                          => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitReturn(ReturnStatementSyntax syntax)                          => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitTypePattern(TypePatternSyntax syntax)                         => throw new NotImplementedException(); 
        public virtual ISyntaxNode VisitAnyPattern(AnyPatternSyntax syntax)                           => throw new NotImplementedException();
        public virtual ISyntaxNode VisitConstantPattern(ConstantPatternSyntax syntax)                 => throw new NotImplementedException();
        public virtual ISyntaxNode VisitSymbol(Symbol symbol)                                         => throw new NotImplementedException(); 
        public virtual ISyntaxNode FunctionDeclarationSyntax(FunctionDeclarationSyntax syntax)        => throw new NotImplementedException();
        public virtual ISyntaxNode VisitStringLiteral(StringLiteralSyntax syntax)                     => throw new NotImplementedException();
        public virtual ISyntaxNode VisitNumberLiteral(NumberLiteralSyntax syntax)                     => throw new NotImplementedException();

        int i = 0;

        public ISyntaxNode Visit(ISyntaxNode syntax)
        {
            i++;

            if (i > 1000) throw new Exception("rerucssion???");

            switch (syntax)
            {
                case UnaryExpressionSyntax unary     : return VisitUnary(unary);
                case BinaryExpressionSyntax binary   : return VisitBinary(binary);
                case TernaryExpressionSyntax ternary : return VisitTernary(ternary);
                case BlockSyntax block               : return VisitBlock(block);
                case ModuleSyntax module             : return VisitModule(module);
            }
            
            switch (syntax.Kind)
            {
                // Declarations
                case SyntaxKind.PropertyDeclaration       : return VisitVariableDeclaration((PropertyDeclarationSyntax)syntax);
                case SyntaxKind.TypeInitializer           : return VisitTypeInitializer((ObjectInitializerSyntax)syntax);
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
                case SyntaxKind.AnyPattern                : return VisitAnyPattern((AnyPatternSyntax)syntax);

                case SyntaxKind.Symbol: return VisitSymbol((Symbol)syntax);
                    
                case SyntaxKind.NumberLiteral: return VisitNumberLiteral((NumberLiteralSyntax)syntax);
                case SyntaxKind.StringLiteral: return VisitStringLiteral((StringLiteralSyntax)syntax);

                    
                default: throw new Exception("unexpected expression:" + syntax.GetType().Name);
            }
        }
    }
}