﻿using System;

namespace D.Syntax
{
    public class AnnotationExpressionSyntax : ISyntaxNode
    {
        public AnnotationExpressionSyntax(Symbol name, ArgumentSyntax[] arguments)
        {
            Name      = name ?? throw new ArgumentNullException(nameof(name));
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public Symbol Name { get; }

        public ArgumentSyntax[] Arguments { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.AnnotationExpression;
    }
}