using System;

using E.Symbols;

namespace E.Syntax;

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

        if (i > 10_000) throw new Exception("exceeded 10,000 visits");

        switch (syntax)
        {
            case UnaryExpressionSyntax unary     : return VisitUnary(unary);
            case BinaryExpressionSyntax binary   : return VisitBinary(binary);
            case TernaryExpressionSyntax ternary : return VisitTernary(ternary);
            case BlockSyntax block               : return VisitBlock(block);
            case ModuleSyntax module             : return VisitModule(module);
        }

        return syntax.Kind switch
        {
            // Declarations
            SyntaxKind.PropertyDeclaration      => VisitVariableDeclaration((PropertyDeclarationSyntax)syntax),
            SyntaxKind.TypeInitializer          => VisitTypeInitializer((ObjectInitializerSyntax)syntax),
            SyntaxKind.DestructuringAssignment  => VisitDestructuringAssignment((DestructuringAssignmentSyntax)syntax),
            SyntaxKind.MemberAccessExpression   => VisitMemberAccess((MemberAccessExpressionSyntax)syntax),
            SyntaxKind.IndexAccessExpression    => VisitIndexAccess((IndexAccessExpressionSyntax)syntax),
            SyntaxKind.LambdaExpression         => VisitLambda((LambdaExpressionSyntax)syntax),
            SyntaxKind.CallExpression           => VisitCall((CallExpressionSyntax)syntax),
            SyntaxKind.MatchExpression          => VisitMatch((MatchExpressionSyntax)syntax),
            SyntaxKind.IfStatement              => VisitIf((IfStatementSyntax)syntax),
            SyntaxKind.ElseIfStatement          => VisitElseIf((ElseIfStatementSyntax)syntax),
            SyntaxKind.ElseStatement            => VisitElse((ElseStatementSyntax)syntax),
            SyntaxKind.ReturnStatement          => VisitReturn((ReturnStatementSyntax)syntax),
                
            // Patterns
            SyntaxKind.ConstantPattern          => VisitConstantPattern((ConstantPatternSyntax)syntax),
            SyntaxKind.TypePattern              => VisitTypePattern((TypePatternSyntax)syntax),
            SyntaxKind.AnyPattern               => VisitAnyPattern((AnyPatternSyntax)syntax),
            SyntaxKind.Symbol                   => VisitSymbol((Symbol)syntax),
            SyntaxKind.NumberLiteral            => VisitNumberLiteral((NumberLiteralSyntax)syntax),
            SyntaxKind.StringLiteral            => VisitStringLiteral((StringLiteralSyntax)syntax),
            _                                   => throw new Exception("Unexpected expression:" + syntax.GetType().Name),
        };
    }
}