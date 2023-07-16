using E.Protocols;
using E.Symbols;

namespace E.Syntax;

// A protocol { }

public sealed class ProtocolDeclarationSyntax(
    Symbol name,
    IProtocolMessage[] messages,
    FunctionDeclarationSyntax[] members) : ISyntaxNode
{
    public Symbol Name { get; } = name;

    public IProtocolMessage[] Messages { get; } = messages;

    public FunctionDeclarationSyntax[] Members { get; } = members;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ProtocolDeclaration;
}