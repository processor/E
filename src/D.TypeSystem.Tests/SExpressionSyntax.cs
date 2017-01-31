using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TypeSystem
{
    public delegate object Acceptor(Token token, string match);

    public class Symbol
    {
        public Symbol(string id) { Id = id ?? Guid.NewGuid().ToString("P"); }

        public override string ToString() => Id;

        public string Id { get; private set; }
    }

    public class Token : Symbol
    {
        internal Token(string id)
            : base(id) { }

        public Token(string pattern, Acceptor acceptor) : base(pattern)
        {
            Regex = new Regex(string.Format("^({0})", !string.IsNullOrEmpty(Pattern = pattern) ? Pattern : ".*"), RegexOptions.Compiled);
            ValueOf = acceptor;
        }
        public string Pattern { get; private set; }

        public Regex Regex { get; private set; }

        public Acceptor ValueOf { get; private set; }

    }
    public class SExpressionSyntax
    {
        private static readonly Token Space = Token("\\s+", Echo);
        private static readonly Token Open = Token("\\(", Echo);
        private static readonly Token Close = Token("\\)", Echo);
        private static readonly Token Quote = Token("\\'", Echo);

        private Token comment;

        private static Exception Error(string message, params object[] arguments) => new Exception(string.Format(message, arguments));

        private static object Echo(Token token, string match) => new Token(token.Id);

        private static object Quoting(Token token, string match) => NewSymbol(token, match);

        private Tuple<Token, string, object> Read(ref string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var found = null as Match;
                var sofar = input;
                var tuple = Lexicon.FirstOrDefault(current => (found = current.Item2.Regex.Match(sofar)).Success && (found.Length > 0));
                var token = tuple?.Item2;
                var match = token != null ? found.Value : null;
                input = match != null ? input.Substring(match.Length) : input;
                return token != null ? Tuple.Create(token, match, token.ValueOf(token, match)) : null;
            }
            return null;
        }

        private Tuple<Token, string, object> Next(ref string input)
        {
            Tuple<Token, string, object> read;
            while (((read = Read(ref input)) != null) && ((read.Item1 == Comment) || (read.Item1 == Space))) ;
            return read;
        }

        public object Parse(ref string input, Tuple<Token, string, object> next)
        {
            var value = null as object;
            if (next != null)
            {
                var token = next.Item1;
                if (token == Open)
                {
                    var list = new List<object>();
                    while (((next = Next(ref input)) != null) && (next.Item1 != Close))
                    {
                        list.Add(Parse(ref input, next));
                    }
                    if (next == null)
                    {
                        throw Error("unexpected EOF");
                    }
                    value = list.ToArray();
                }
                else if (token == Quote)
                {
                    var quote = next.Item3;
                    next = Next(ref input);
                    value = new[] { quote, Parse(ref input, next) };
                }
                else
                {
                    value = next.Item3;
                }
            }
            else
            {
                throw Error("unexpected EOF");
            }
            return value;
        }

        protected Token TokenOf(Acceptor acceptor)
        {
            var found = Lexicon.FirstOrDefault(pair => pair.Item2.ValueOf == acceptor);
            var token = found?.Item2;
            if ((token == null) && (acceptor != Commenting))
            {
                // throw Error("missing required token definition: {0}", acceptor.Method.Name);
            }

            return token;
        }

        protected IList<Tuple<string, Token>> Lexicon { get; private set; }

        protected Token Comment
        {
            get { return comment = comment ?? TokenOf(Commenting); }
        }

        public static Token Token(string pattern, Acceptor acceptor)
            => new Token(pattern, acceptor);

        public static object Commenting(Token token, string match)
            => Echo(token, match);

        public static object NewSymbol(Token token, string match)
            => new Symbol(match);

        public static Symbol Symbol(object value)
            => value as Symbol;

        public static string Moniker(object value)
            => Symbol(value)?.Id;

        public static string ToString(object value)
        {
            return
                value is object[] ?
                (
                    ((object[])value).Length > 0 ?
                    ((object[])value).Aggregate(new StringBuilder("("), (result, obj) => result.AppendFormat(" {0}", ToString(obj))).Append(" )").ToString()
                    :
                    "( )"
                )
                :
                (value != null ? (value is string ? string.Concat('"', (string)value, '"') : (value is bool ? value.ToString().ToLower() : value.ToString())).Replace("\\\r\n", "\r\n").Replace("\\\n", "\n").Replace("\\t", "\t").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\\"", "\"") : null) ?? "(null)";
        }

        public SExpressionSyntax()
        {
            Lexicon = new List<Tuple<string, Token>>();
            Include(Space, Open, Close, Quote);
        }

        public SExpressionSyntax Include(params Token[] tokens)
        {
            foreach (var token in tokens)
            {
                Lexicon.Add(new Tuple<string, Token>(token.Id, token));
            }
            return this;
        }

        public object Parse(string input)
        {
            var next = Next(ref input);

            var value = Parse(ref input, next);

            if ((next = Next(ref input)) != null)
            {
                throw Error("unexpected ", next.Item1);
            }

            return value;
        }
    }
}
