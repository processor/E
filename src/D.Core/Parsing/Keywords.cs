using System.Collections.Generic;

namespace E.Parsing;

internal sealed class Keywords
{
    // TODO: Freeze
    public static readonly Dictionary<string, TokenKind> Map = new() {
        { "ƒ"              , TokenKind.Function },
        { "as"             , TokenKind.Op },
        { "ascending"      , TokenKind.Ascending },
        { "async"          , TokenKind.Async },
        { "false"          , TokenKind.False },
        { "null"           , TokenKind.Null },
        { "catch"          , TokenKind.Catch },
        { "continue"       , TokenKind.Continue },
        { "default"        , TokenKind.Default },
        { "do"             , TokenKind.Do },
        { "else"           , TokenKind.Else },
        { "emit"           , TokenKind.Emit },
        { "enum"           , TokenKind.Enum },
        { "for"            , TokenKind.For },
        { "from"           , TokenKind.From },
        { "function"       , TokenKind.Function },
        { "let"            , TokenKind.Let },
        { "match"          , TokenKind.Match },
        { "module"         , TokenKind.Module },
        { "if"             , TokenKind.If },
        { "impl"           , TokenKind.Implementation },
        { "implementation" , TokenKind.Implementation },
        { "in"             , TokenKind.In },
        { "is"             , TokenKind.Op },
        { "descending"     , TokenKind.Descending },
        { "mutable"        , TokenKind.Mutable },
        { "mutating"       , TokenKind.Mutating },
        { "on"             , TokenKind.On },
        { "observe"        , TokenKind.Observe },
        { "operator"       , TokenKind.Operator },
        { "orderby"        , TokenKind.Orderby },
        { "return"         , TokenKind.Return },
        { "select"         , TokenKind.Select },
        { "this"           , TokenKind.This },
        { "throw"          , TokenKind.Throw },
        { "to"             , TokenKind.To },
        { "true"           , TokenKind.True },
        { "try"            , TokenKind.Try },
        { "until"          , TokenKind.Until },
        { "unit"           , TokenKind.Unit },
        { "using"          , TokenKind.Using },
        { "var"            , TokenKind.Var },
        { "when"           , TokenKind.When },
        { "while"          , TokenKind.While },
        { "with"           , TokenKind.With },
        { "where"          , TokenKind.Where },
        { "yield"          , TokenKind.Yield },
        { "event"          , TokenKind.Event },
        { "protocol"       , TokenKind.Protocol },
        { "record"         , TokenKind.Record },
        { "public"         , TokenKind.Public },
        { "private"        , TokenKind.Private },
        { "internal"       , TokenKind.Internal },

        // Types
        { "class"          , TokenKind.Class },
        { "struct"         , TokenKind.Struct },
        { "actor"          , TokenKind.Actor },
        { "role"           , TokenKind.Role }
    };
}
