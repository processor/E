using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

using TypeSystem;

namespace D.Inference
{
    public class STests
    {
        [Fact]
        public void ABC()
        {
            var syntax = new SExpressionSyntax().Include(
                // Not-quite-Lisp-indeed; just tolen from our host, C#, as-is
                SExpressionSyntax.Token("\\/\\/.*", SExpressionSyntax.Commenting),
                SExpressionSyntax.Token("false", (token, match) => false),
                SExpressionSyntax.Token("true", (token, match) => true),
                SExpressionSyntax.Token("null", (token, match) => null),

                // Integers (unsigned)
                SExpressionSyntax.Token("[0-9]+", (token, match) => int.Parse(match)),

                // String literals
                SExpressionSyntax.Token("\\\"(\\\\\\n|\\\\t|\\\\n|\\\\r|\\\\\\\"|[^\\\"])*\\\"", (token, match) 
                    => match.Substring(1, match.Length - 2)),

                // For identifiers...
                SExpressionSyntax.Token("[\\$_A-Za-z][\\$_0-9A-Za-z\\-]*", SExpressionSyntax.NewSymbol),

                // ... and such
                SExpressionSyntax.Token("[\\!\\&\\|\\<\\=\\>\\+\\-\\*\\/\\%\\:]+", SExpressionSyntax.NewSymbol)
            );

            var system = TypeSystem.Default;
            var env = new Dictionary<string, IType>();

            // Classic
            var @bool = system.NewType(typeof(bool).Name);
            var @int = system.NewType(typeof(int).Name);
            var @string = system.NewType(typeof(string).Name);

            // Generic list of some `item' type : List<item>
            var ItemType = system.NewGeneric();
            var ListType = system.NewType("List", new[] { ItemType });



            // Populate the top level typing environment
            env[@bool.Name] = @bool;
            env[@int.Name] = @int;
            env[@string.Name] = @string;
            env[ListType.Name] = env["nil"] = ListType;

            // Bake some operator function types (to have something to infer about, in familiar-looking arith. expressions)
            var binary = system.NewGeneric();

            system.Infer(env, new Let("+", new FuncNode(new[] { new Variable("left", binary), new Variable("right", binary) }, binary, new Variable("left"))));
            system.Infer(env, new Let("-", new FuncNode(new[] { new Variable("left", binary), new Variable("right", binary) }, binary, new Variable("left"))));
            system.Infer(env, new Let("*", new FuncNode(new[] { new Variable("left", binary), new Variable("right", binary) }, binary, new Variable("left"))));
            system.Infer(env, new Let("/", new FuncNode(new[] { new Variable("left", binary), new Variable("right", binary) }, binary, new Variable("left"))));
            system.Infer(env, new Let("<", new FuncNode(new[] { new Variable("left", binary), new Variable("right", binary) }, @bool,  new Constant(@bool))));
            system.Infer(env, new Let(">", new FuncNode(new[] { new Variable("left", binary), new Variable("right", binary) }, @bool,  new Constant(@bool))));

            // A ternary if-then-else will come in handy too
            var ifThenElse = system.NewGeneric();
            system.Infer(env, new Let("if", new FuncNode(new[] { new Variable("condition", @bool), new Variable("then", ifThenElse), new Variable("else", ifThenElse) }, ifThenElse, new Variable("then"))));

            // And List ops, too
            system.Infer(env, new Let(":",     new FuncNode(new[] { new Variable("item", ItemType), new Variable("list", ListType) }, ListType, new Constant(ListType))));
            system.Infer(env, new Let("empty", new FuncNode(new[] { new Variable("list", ListType) }, @bool,    new Constant(@bool))));
            system.Infer(env, new Let("head",  new FuncNode(new[] { new Variable("list", ListType) }, ItemType, new Constant(ItemType))));
            system.Infer(env, new Let("tail",  new FuncNode(new[] { new Variable("list", ListType) }, ListType, new Constant(ListType))));

            // DRY helpers
            var isFunction = null as Func<IType, bool>;
            isFunction = type => type != null ? (type.Constructor != null ? type.Constructor.Name == TypeSystem.Function.Name : isFunction(type.Instance)) : false;
            Func<object, bool> isArray = value => value is object[];
            Func<object, object[]> array = value => (object[])value;

            // A kind of poor man's visitor (over the S-expr) : just a bunch of lambdas
            Func<object, object> visitSExpr = null;
            Func<object, object> visitLet = null;
            Func<object, Node> visitDefine = null;
            Func<object, Node> visitApply = null;
            Func<object, Node> visitLambda = null;
            Func<object, Node> visitConst = null;
            Func<object, Node> visitVar = null;

            visitSExpr =
                sexpr =>
                    isArray(sexpr) ?
                    (
                        array(sexpr).Length > 1 ?
                        (
                            (SExpressionSyntax.Symbol(array(sexpr)[0]) == null) || (SExpressionSyntax.Moniker(array(sexpr)[0]) != "let") ?
                            (
                                (SExpressionSyntax.Symbol(array(sexpr)[1]) == null) || (SExpressionSyntax.Moniker(array(sexpr)[1]) != "=>") ?
                                visitApply(sexpr)
                                :
                                visitLambda(sexpr)
                            )
                            :
                            visitLet(sexpr)
                        )
                        :
                        array(sexpr).Length > 0 ? visitSExpr(array(sexpr)[0]) : null
                    )
                    :
                    SExpressionSyntax.Symbol(sexpr) != null ? visitVar(sexpr) : visitConst(sexpr);
            visitLet =
                let =>
                    array(array(let)[1]).Select(define => visitDefine(define)).ToArray();
            visitDefine =
                define =>
                    new Let(SExpressionSyntax.Moniker(array(define)[0]), (Node)visitSExpr(array(define)[1]));
            visitApply =
                apply =>
                    new Call
                    (
                        SExpressionSyntax.Moniker(array(apply)[0]),
                        array(apply).Skip(1).Select(arg => visitSExpr(arg)).Cast<Node>().ToArray(),
                        !env.ContainsKey((string)(apply = SExpressionSyntax.Moniker(array(apply)[0]))) ||
                        !isFunction(env[(string)apply])
                        ?
                        (
                            env.ContainsKey((string)apply) ?
                            (
                                !isFunction(env[(string)apply]) ?
                                env[(string)apply]
                                :
                                null
                            )
                            :
                            null
                        )
                        :
                        null
                    );
            visitLambda =
                lambda =>
                    new FuncNode
                    (
                        array(array(lambda)[0]).Select(arg => visitVar(arg)).ToArray(),
                        (Node)visitSExpr(array(lambda)[2])
                    );
            visitConst =
                @const => new Constant(@const.GetType().Name);
            visitVar =
                var =>
                    new Variable(SExpressionSyntax.Moniker(var));

            // Parse some S-expr (in string representation)
            var source = syntax.Parse(@"
                (
                    let
                    (
                    	// Type inference ""playground""

						// Classic..                    	
                        ( id ( ( x ) => x ) ) // identity
                        
                        ( o ( ( f g ) => ( ( x ) => ( f ( g x ) ) ) ) ) // composition
                        
                        ( factorial ( ( n ) => ( if ( > n 0 ) ( * n ( factorial ( - n 1 ) ) ) 1 ) ) )
                        
                        // More interesting..
                        ( fmap (
                            ( f l ) =>
                            ( if ( empty l )
                                ( : ( f ( head l ) ) ( fmap f ( tail l ) ) )
                                nil
                            )
                        ) )
                        
                        // your own...

                        ( a 1 )
                        ( b ""hi"" )
                    )
                    ( )
                )
            ");

            // Visit the parsed S-expr, turn it into a more friendly AST for H-M
            // (see Node, et al, above) and infer some types from the latter

            var sb = new StringBuilder();

            var nodes = (Node[])visitSExpr(source);

            foreach (var type in Analyze(system, env, nodes))
            {
                sb.AppendLine(type.Name);
            }

            throw new Exception(sb.ToString());
        }


        public IEnumerable<IType> Analyze(ITypeSystem system, Dictionary<string, IType> env, IList<Node> nodes)
        {
            foreach (var node in nodes)
            {
                // node.id

                yield return system.Infer(env, node);
            }
        }
    }
}
