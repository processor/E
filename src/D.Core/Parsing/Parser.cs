using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D.Parsing
{
    using Syntax;
    using Units;

    using static OperatorType;
    using static TokenKind;

    public class Parser : IDisposable
    {
        private readonly TokenReader reader;
        private readonly Env env;

        public Parser(string text)
            : this(text, new Env()) { }

        public Parser(string text, Env env)
        {
            this.reader = new TokenReader(new Tokenizer(text, env));
            this.env = env;

            modes.Push(Mode.Root);
        }

        public static SyntaxNode Parse(string text)
        {
            using (var parser = new Parser(text))
            {
                return parser.Next();
            }
        }

        #region Mode

        internal enum Mode
        {
            Root,
            Statement,
            Parenthesis,
            Type,
            Arguments,
            For,
            Block,
            Implementation,
            InterpolatedString
        }

        private readonly Stack<Mode> modes = new Stack<Mode>();

        private void EnterMode(Mode mode)
        {
            modes.Push(mode);
        }

        private void LeaveMode(Mode mode)
        {
            var m = modes.Pop();

            if (m != mode)
            {
                throw new Exception($"Expected {mode} when leaving {m}");
            }
        }

        private bool InMode(Mode mode)
        {
            return modes.Peek() == mode;
        }

        #endregion

        public IEnumerable<SyntaxNode> Enumerate()
        {
            while (!reader.IsEof)
            {
                yield return ReadExpression();
            }
        }

        public SyntaxNode Next()
        {
            if (reader.IsEof) return null;

            return ReadExpression();
        }

        private SyntaxNode ReadExpression()
        {
            if (reader.IsEof) return null;

            count++;

            if (count > 500) throw new Exception("recurssive call: " + reader.Current.Kind);

            switch (reader.Current.Kind)
            {
                case Null                   : reader.Consume(); return NullLiteralSyntax.Instance;
                case True                   : reader.Consume(); return BooleanLiteralSyntax.True;
                case False                  : reader.Consume(); return BooleanLiteralSyntax.False;

                case Quote                  : return ReadStringLiteral();               // "string"
                case Apostrophe             : return ReadCharacterLiteral();            // 'c'

                case InterpolatedStringOpen : return ReadInterpolatedString();          // $"{expression}"
                case Match                  : return ReadMatch();                       // match expression with ...

                case Var                    : return ReadLet(Var);                      // var
                case Let                    : return ReadLet(Let);                      // let

                case For                    : return ReadFor();                         // for 
                case While                  : return ReadWhile();                       // while
                case If                     : return ReadIf();                          // if
                case Return                 : return ReadReturn();                      // return
                case Emit                   : return ReadEmit();                        // emit

                case Observe                : 
                case On                     : return ReadObserveStatement();            // on | observe
                case From                   : return ReadQuery();                       // from x in Collection

                case BracketOpen            : return ReadNewArray();                    // [
                case BraceOpen              : return ReadNewObject(null);               // { 
                case DotDotDot              : return ReadSpread();                      // ... 

                // case Exclamation         : return ReadUnary(Consume(Exclamation));   // !{expression}

                case Using                  : return ReadUsingStatement();
            }


            return TopExpression(); // functionName args, 5px * 5
        }

        // <tag>
        
        // </tag>
        /*
        public TagExpression ReadTag()
        {
        }
        */

        #region Queries

        public QueryExpression ReadQuery()
        {
            Consume(From);                          // ! from

            // from point in Points
            // where point.a > 0
            // select { a, b }

            var maybeVariable = ReadExpression();

            bool wasVariable = ConsumeIf(In);

            var collection = wasVariable            // ? in
                ? ReadExpression()
                : maybeVariable; // nope, collection

            var variable = wasVariable ? maybeVariable : null;

            // using index_name
            var _using = ConsumeIf(Using)           // ? using
                ? ReadExpression()
                : null;

            var filter = (ConsumeIf(Where))         // ? where
                ? ReadExpression()
                : null;

            EnterMode(Mode.Block);

            var map = ConsumeIf(Select)             // ? select
                ? IsKind(BraceOpen)
                    ? ReadNewObject(null)     // ? { record }
                    : ReadExpression()              // ? variable
                : null;

            LeaveMode(Mode.Block);


            var orderby = ConsumeIf(Orderby)        // ? orderby
               ? new OrderByStatement(
                   member     : ReadExpression(),
                   descending : ConsumeIf(Descending) // ? descending
               )
               : null;

            ConsumeIf(Ascending);                   // ? ascending

            var skip = (ConsumeIf("skip")) ? long.Parse(Consume(Number)) : 0; // ? skip (number)
            var take = (ConsumeIf("take")) ? long.Parse(Consume(Number)) : 0; // ? take (number)

            return new QueryExpression(collection, variable, filter, map, orderby, skip, take);
        }

        #endregion

        #region Statements: Using, For, While, If, Return

        public UsingStatement ReadUsingStatement()
        {
            Consume(Using);            // ! using

            var domains = new List<Symbol>();

            do
            {
                domains.Add(ReadSymbol(SymbolFlags.Module));
            }
            while (!IsEof && ConsumeIf(Comma)); // ? , 

            ConsumeIf(Semicolon); // ? ;

            return new UsingStatement(domains.ToArray());
        }

        public LambdaExpressionSyntax ReadLambda()
        {
            Consume(LambdaOperator);            // ! =>

            var expression = IsKind(BraceOpen)  // ? {
                ? ReadBlock()
                : ReadExpression();

            ConsumeIf(Semicolon);               // ? ;

            return new LambdaExpressionSyntax(expression);
        }

        public ReturnStatementSyntax ReadReturn()
        {
            Consume(Return);                    // ! return

            var expression = ReadExpression();  // ! (expression)

            ConsumeIf(Semicolon);               // ? ;

            return new ReturnStatementSyntax(expression);
        }

        public EmitStatement ReadEmit()
        {
            Consume(Emit);                      // ! emit

            var expression = ReadExpression();  // ! (expression)

            ConsumeIf(Semicolon);               // ? ;

            return new EmitStatement(expression);
        }

        public IfStatementSyntax ReadIf()
        {
            Consume(If);                        // ! if

            EnterMode(Mode.Statement);

            var condition = ReadExpression();   // ! (condition)

            var body = ReadBlock();             // { ... }

            var elseBranch = IsKind(Else)       // ? else 
                ? ReadElse()
                : null;

            LeaveMode(Mode.Statement);

            return new IfStatementSyntax(condition, body, elseBranch);
        }

        public SyntaxNode ReadElse()
        {
            Consume(Else);                      // ! else

            var condition = ConsumeIf(If)       // ? if 
                ? ReadExpression()              // ! (condition)
                : null;

            var body = ReadBlock();             // { ... }

            var elseBranch = IsKind(Else)       // ? else
                ? ReadElse()
                : null;


            return condition != null
                ? (SyntaxNode)new ElseIfStatementSyntax(condition, body, elseBranch)
                : new ElseStatementSyntax(body);

        }

        public WhileStatementSyntax ReadWhile()
        {
            Consume(While); // ! while

            EnterMode(Mode.Statement);

            var condition = ReadExpression();
            var body      = ReadBlock();

            LeaveMode(Mode.Statement);

            return new WhileStatementSyntax(condition, body);
        }

        // for 1..10 { }  
        // for x { } 
        // for x in y { }

        public ForStatement ReadFor()
        {
            Consume(For);

            EnterMode(Mode.For);

            SyntaxNode generatorExpression;
            SyntaxNode variableExpression = null;  // variable | pattern

            var first = IsOneOf(ParenthesisOpen, Underscore, BraceOpen)
                ? ReadPattern()
                : ReadExpression();

            if (ConsumeIf(In))                      // ? in
            {
                variableExpression = first;
                generatorExpression = ReadExpression();
            }
            else
            {
                generatorExpression = first;
            }

            LeaveMode(Mode.For);

            return new ForStatement(variableExpression, generatorExpression, ReadBlock());
        }

        // on instance Event'Type e { }
        private ObserveStatementSyntax ReadObserveStatement()
        {
            reader.Consume(); // ! on | observe

            var observable  = ReadExpression(); // TODO: handle member access

            var eventType = ReadSymbol();
            var varName = (! (IsKind(BraceOpen) | IsKind(LambdaOperator)))
                ? ReadSymbol(SymbolFlags.Function).Name
                : null;

            var body = ReadBody();

            var until = IsKind(Until)
                ? ReadUntilExpression()
                : null;

            return new ObserveStatementSyntax(observable, eventType, varName, body, until);
        }

        private UntilConditionSyntax ReadUntilExpression()
        {
            Consume(Until); // ! until

            var untilObservable = ReadExpression();
            var untilEventType  = ReadSymbol();

            return new UntilConditionSyntax(untilObservable, untilEventType);
        }

        public BlockExpressionSyntax ReadBlock()
        {
            Consume(BraceOpen); // ! {

            EnterMode(Mode.Block);

            var statements = new List<SyntaxNode>();

            while (!IsEof && !IsKind(BraceClose))
            {
                statements.Add(ReadExpression());
            }

            Consume(BraceClose); // ! }

            LeaveMode(Mode.Block);

            return new BlockExpressionSyntax(statements.ToArray());
        }

        public SpreadExpressionSyntax ReadSpread()
        {
            Consume(DotDotDot); // ! ...

            return new SpreadExpressionSyntax(ReadPrimary());
        }

        #endregion

        #region Declarations

        // let x = 5
        // let x: i8 = 5
        // let x = (5: i8)
        // let x = i8 | None = 8
        // let x: i8 > 0 = 100
        // let x = ƒ(x, y) => x + y

        // Multiple
        // let a = 1, b = 2, c: i32 = 50
        // let x, y, z: Number
        // var a: i8
        // var a = 1

        // Destructuring
        // let (a: Integer, b, c) = instance

        private readonly List<VariableDeclarationSyntax> variableList = new List<VariableDeclarationSyntax>();
        
        public SyntaxNode ReadLet(TokenKind kind)
        {
            Consume(kind); // ! let | var

            if (ConsumeIf(ParenthesisOpen))
            {
                var list = new List<AssignmentElementSyntax>();

                do
                {
                    var name = ReadSymbol();

                    var type = (ConsumeIf(Colon))
                        ? ReadSymbol()
                        : null;

                    list.Add(new AssignmentElementSyntax(name, type));

                } while (ConsumeIf(Comma));

                Consume(ParenthesisClose);

                reader.Consume("="); // ! =

                var right = ReadExpression();

                ConsumeIf(Semicolon); // ? ;

                return new DestructuringAssignmentSyntax(list.ToArray(), right);
            }

            var declaration = ReadVariableDeclaration(kind == Var);

            if (IsKind(Comma))
            {
                variableList.Add(declaration);

                while (ConsumeIf(Comma))
                {
                    variableList.Add(ReadVariableDeclaration(kind == Var));
                }

                bool inferedFromLast = false;

                foreach (var v in variableList)
                {
                    if (v.Type == null && v.Value == null)
                    {
                        inferedFromLast = true;
                    }
                }

                if (inferedFromLast)
                {
                    var l = new List<VariableDeclarationSyntax>(variableList.Count);

                    var k = variableList[variableList.Count - 1].Type;

                    foreach (var var in variableList)
                    {
                        l.Add(new VariableDeclarationSyntax(var.Name, k, null, var.Flags));
                    }

                    variableList.Clear();
                    variableList.AddRange(l);
                }

                ConsumeIf(Semicolon); // ? ;

                return new CompoundVariableDeclaration(variableList.Extract());
            }
            else
            {
                ConsumeIf(Semicolon); // ? ;

                return declaration;
            }
        }

        private VariableDeclarationSyntax ReadVariableDeclaration(bool mutable)
        {
            mutable = ConsumeIf(Mutable) || mutable;

            ConsumeIf(ParenthesisOpen);     // ? (

            var flags = mutable ? VariableFlags.Mutable : VariableFlags.None;

            var name = ReadSymbol(SymbolFlags.Variable | SymbolFlags.Local);

            ConsumeIf(ParenthesisClose);    // ? )

            var type = ConsumeIf(Colon)     // ? :
                ? ReadSymbol()
                : null;

            var value = ConsumeIf("=")       // ? =
                ? IsKind(Function) ? ReadFunctionDeclaration(name) : ReadExpression()
                : null;

            return new VariableDeclarationSyntax(name.ToString(), type, value, flags);

        }

        private FunctionDeclarationSyntax ReadInitializer()
        {
            var flags = FunctionFlags.Initializer;

            Consume(From); // ! from

            return ReadFunctionDeclaration(new Symbol("initializer", SymbolFlags.Function), flags);
        }

        // to String =>
        private FunctionDeclarationSyntax ReadConverter()
        {
            var flags = FunctionFlags.Converter;

            Consume(To); // ! to

            var returnType = ReadSymbol();

            var body = ReadBody();

            ConsumeIf(Semicolon); // ? ;

            return new FunctionDeclarationSyntax(Array.Empty<ParameterSyntax>(), body, returnType, flags);
        }

        // [index: Integer] -> T { ..
        private FunctionDeclarationSyntax ReadIndexerDeclaration()
        {
            var flags = FunctionFlags.Indexer;

            Consume(BracketOpen);

            var parameters = ReadParameters().ToArray();

            Consume(BracketClose);

            var returnType = ConsumeIf(ReturnArrow)
                   ? ReadSymbol(SymbolFlags.Argument)
                   : null;

            var body = ReadBody();

            ConsumeIf(Semicolon); // ? ;

            return new FunctionDeclarationSyntax(parameters, body, returnType, flags);
        }

        private FunctionDeclarationSyntax ReadFunctionDeclaration(FunctionFlags flags = FunctionFlags.None)
        {
            var isOperator = IsKind(Op);

            if (isOperator) flags |= FunctionFlags.Operator;

            var name = isOperator ? new Symbol(reader.Consume(), SymbolFlags.Function) : ReadSymbol(SymbolFlags.Function);

            return ReadFunctionDeclaration(name, flags);
        }

        private FunctionDeclarationSyntax ReadFunctionDeclaration(Symbol name, FunctionFlags flags = FunctionFlags.None)
        {
            ConsumeIf(Function);        // ? ƒ | function

            if (name != null && char.IsUpper(name.Name[0]))
            {
                flags |= FunctionFlags.Initializer;
            }

            // generic parameters <T: Number> 
            var genericParameters = ReadGenericParameters();

            ParameterSyntax[] parameters;

            if (ConsumeIf(ParenthesisOpen)) // ! (
            {
                parameters = ReadParameters().ToArray();

                Consume(ParenthesisClose);  // ! )
            }
            else if (IsKind(Identifier))
            {
                var varName = reader.Consume();

                bool isType = (char.IsUpper(varName.Text[0]));

                parameters = new[] { isType 
                    ? ParameterSyntax.Ordinal(0, Symbol.Argument(varName)) 
                    : new ParameterSyntax(varName)
                };
            }
            else
            {
                flags |= FunctionFlags.Property | FunctionFlags.Instance;

                parameters = Array.Empty<ParameterSyntax>();
            }

            var returnType = ConsumeIf(ReturnArrow)
                ? ReadSymbol(SymbolFlags.Argument)
                : null;

            SyntaxNode body;

            if (IsKind(Semicolon))
            {
                body = null;

                flags |= FunctionFlags.Abstract;
            }
            else
            {
                body = ReadBody();
            }

            ConsumeIf(Semicolon); // ? ;

            return new FunctionDeclarationSyntax(name, genericParameters, parameters, returnType, body, flags);
        }

        public ParameterSyntax[] ReadGenericParameters()
        {
            if (ConsumeIf(TagOpen))
            {
                var i = 0;

                var list = new List<ParameterSyntax>();

                do
                {
                    var genericName = ReadSymbol();

                    var genericType = ConsumeIf(Colon)         // ? : {type}
                        ? ReadSymbol()
                        : null;

                    list.Add(new ParameterSyntax(genericName, genericType));

                    i++;
                }
                while (ConsumeIf(Comma));

                Consume(TagClose);

                return list.ToArray();
            }
           
            return Array.Empty<ParameterSyntax>();
        }

        private SyntaxNode ReadBody()
        {
            switch (Current.Kind)
            {
                case LambdaOperator : return ReadLambda();      // expression bodied?
                case BraceOpen      : return ReadBlock(); 
            }

            throw new UnexpectedTokenException("Expected block or lambda reading lambda", Current);
        }

        private FunctionDeclarationSyntax ReadAnonymousFunctionDeclaration(Symbol variableName)
        {
            // TODO: determine whether it captures any outside variables 

            var lambda = ReadLambda();

            ConsumeIf(Semicolon); // ? ;

            return new FunctionDeclarationSyntax(
                parameters  : new[] { new ParameterSyntax(variableName) },
                body        : lambda.Expression, 
                flags       : FunctionFlags.Anonymous
            );
        }

        public IEnumerable<ParameterSyntax> ReadParameters()
        {
            if (IsKind(ParenthesisClose)) yield break;

            var i = 0;

            do
            {
                yield return ReadParameter(i);

                i++;
            }
            while (reader.ConsumeIf(Comma));
        }

        // Integer,
        // Integer)
        // i: Integer
        // this Type
        public ParameterSyntax ReadParameter(int index)
        {
            var name = ReadSymbol(SymbolFlags.Argument); // name

            var type = ConsumeIf(Colon)         // ? : {type}
                ? ReadSymbol()
                : null;

            var predicate = (IsKind(Op) && Current.Text != "=") // ? op
                ? MaybeBinary(name, 0)
                : null;
           
            var defaultValue = ConsumeIf("=")   // ? = {defaultValue}
                ? ReadExpression()
                : null;

            if (type == null && IsOneOf(Comma, ParenthesisClose) && char.IsUpper(name.Name[0]))
            {
                return ParameterSyntax.Ordinal(index, type: name);
            }

            return new ParameterSyntax(name, type, defaultValue, predicate, index: index);
        }

        // type | event (?record) | record

        public CompoundTypeDeclarationSyntax ReadCompoundTypeDeclaration(Symbol[] names)
        {
            ConsumeIf(Type);

            var flags = ConsumeIf("event") ? TypeFlags.Event : TypeFlags.None;

            if (ConsumeIf(Record)) flags |= TypeFlags.Record;

            var baseTypes = ConsumeIf(Colon)      // ? :
                ? ReadSymbol(SymbolFlags.Type)     // baseType
                : null;

            var members = ReadTypeDeclarationBody();

            ConsumeIf(Semicolon); // ? ;

            return new CompoundTypeDeclarationSyntax(names, flags, baseTypes, members);
        }

        // Float : Number @size(32) { 
        // Int32 type @size(32)
        // Point type <T:Number> : Vector3 { 
        public TypeDeclarationSyntax ReadTypeDeclaration(Symbol typeName)
        {
            ConsumeIf(Type);

            var flags = ConsumeIf("event") ? TypeFlags.Event : TypeFlags.None;

            if (ConsumeIf(Record))    flags |= TypeFlags.Record;

            // <T: Number>
            var genericParameters = ReadGenericParameters();

            var baseType = ConsumeIf(Colon)    // ? :
                ? ReadSymbol(SymbolFlags.Type)  // baseType
                : null;

            var annotations = ReadAnnotations().ToArray();
            var properties = ReadTypeDeclarationBody();

            ConsumeIf(Semicolon); // ? ;

            return new TypeDeclarationSyntax(typeName, genericParameters, baseType, annotations, properties, flags: flags);
        }

        // Pa unit : Pressure @name("Pascal") = 1

        public UnitDeclarationSyntax ReadUnitDeclaration(Symbol name)
        {
            var flags = UnitFlags.None;

            if (ConsumeIf("SI")) flags |= UnitFlags.SI;

            ConsumeIf(TokenKind.Unit);

            var baseType = ConsumeIf(Colon)    // ? :
                ? ReadSymbol(SymbolFlags.Type)  // baseType
                : null;

            var annotations = ReadAnnotations().ToArray();

            SyntaxNode expression = ConsumeIf("=")
                ? Next()
                : null;

            ConsumeIf(Semicolon); // ? ;

            return new UnitDeclarationSyntax(name, baseType, annotations, expression);
        }

        private PropertyDeclarationSyntax[] ReadTypeDeclarationBody()
        {
            if (ConsumeIf(BraceOpen))  // ? {
            {
                var properties = ReadTypeMembers().ToArray();
                
                Consume(BraceClose);  // ! }

                return properties;
            }
            
            return Array.Empty<PropertyDeclarationSyntax>();
        }

        private readonly List<Symbol> names = new List<Symbol>();

        private IEnumerable<AnnotationExpressionSyntax> ReadAnnotations()
        {
            // @primitive
            // @size(10)

            while (ConsumeIf(At))
            {
                var name = ReadSymbol(); // !{name}

                var args = IsKind(ParenthesisOpen) ? ReadArguments() : Array.Empty<ArgumentSyntax>();

                yield return new AnnotationExpressionSyntax(name, args);
            }
        }

        private IEnumerable<PropertyDeclarationSyntax> ReadTypeMembers()
        {
            while (!IsEof && !IsKind(BraceClose))
            {
                // mutable name: Type | Type,

                var flags = VariableFlags.None;

                if (ConsumeIf(Mutable)) flags |= VariableFlags.Mutable;

                do
                {
                    names.Add(ReadSymbol(SymbolFlags.Property));
                }
                while (ConsumeIf(Comma));

                Consume(Colon); // ! :

                var type = ReadSymbol(SymbolFlags.Type);

                ConsumeIf(Semicolon); // ? ;

                foreach (var n in names.Extract())
                {
                    yield return new PropertyDeclarationSyntax(n, type, flags);
                }
            }
        }

        private readonly List<SyntaxNode> members = new List<SyntaxNode>();
        private readonly List<FunctionDeclarationSyntax> methods = new List<FunctionDeclarationSyntax>();

        // Account protocal { }
        // Point protocal : Vector3 { } 

        public ProtocalDeclarationSyntax ReadProtocal(Symbol name)
        {
            Consume(Protocal);      // ! protocal

            Symbol baseProtocal = ConsumeIf(Colon)
                ? ReadSymbol()
                : null;


            var annotations = ReadAnnotations().ToArray();

            Consume(BraceOpen);     // ! {

            var channelProtocal = Array.Empty<IProtocalMessage>();

            if (!IsKind(BraceClose))
            {
                channelProtocal = reader.Current.Text == "*"
                    ? ReadProtocalChannel().ToArray()
                    : Array.Empty<IProtocalMessage>();

                while (!IsEof && !IsKind(BraceClose))
                {
                    methods.Add(ReadProtocalMember());
                }
            }

            
            Consume(BraceClose);  // ! }
            ConsumeIf(Semicolon); // ? ;

            return new ProtocalDeclarationSyntax(name, channelProtocal, methods.Extract());
        }

        public List<IProtocalMessage> ReadProtocalChannel()
        {

            var messages = new List<IProtocalMessage>();
            var options = new List<ProtocalMessage>();

            while (ConsumeIf("*"))  // ! ∙
            {
                ConsumeIf(Bar);         // ? |  // Optional leading bar in a oneof set

                var message = ReadProtocalMessage();

                if (message.Fallthrough)
                {
                    var flags = MessageFlags.None;

                    options.Add(message);

                    while (message.Fallthrough && !IsKind(Repeats) && reader.Current.Text != "*")
                    {
                        options.Add(ReadProtocalMessage());
                    }

                    if (ConsumeIf(Repeats))
                    {
                        flags |= MessageFlags.Repeats;

                        if (ConsumeIf(Colon))
                        {
                            var label = ReadSymbol(SymbolFlags.Label);
                        }
                    }

                    if (ConsumeIf(End)) // ? ∎
                    {
                        flags |= MessageFlags.End;
                    }

                    var oneof = new MessageChoice(options.Extract(), flags);

                    messages.Add(oneof);
                }
                else
                {
                    messages.Add(message);
                }
            }

            return messages;
        }

        //  settle  'Transaction  ƒ (Transaction) -> Transaction' Settlement

        public FunctionDeclarationSyntax ReadProtocalMember()
        {
            var name = ReadSymbol(SymbolFlags.Label);
            
            ConsumeIf(Function);                             // ? ƒ

            var flags = FunctionFlags.Abstract;

            ParameterSyntax[] parameters;

            if (ConsumeIf(ParenthesisOpen))                  // ! (
            {
                parameters = ReadParameters().ToArray();

                Consume(ParenthesisClose);                   // ! )
            }
            else
            {
                flags |= FunctionFlags.Property;

                parameters = Array.Empty<ParameterSyntax>(); 
            }

            var returnType = ConsumeIf(ReturnArrow)
                ? ReadSymbol()
                : Symbol.Void;

            ConsumeIf(Semicolon);

            return new FunctionDeclarationSyntax(name, Array.Empty<ParameterSyntax>(), parameters, returnType, null, flags);
        }

        // dissolve ∎   : dissolved
        // settling 'Transaction  |
        public ProtocalMessage ReadProtocalMessage()
        {
            var flags = ConsumeIf(Question)   // ? ?
                ? MessageFlags.Optional 
                : MessageFlags.None;

            var name = ReadSymbol(SymbolFlags.Function);

            if (ConsumeIf(End)) // ? ∎
            {
                flags |= MessageFlags.End;
            }

            if (ConsumeIf(Bar)) // ? |
            {
                flags |= MessageFlags.Fallthrough;
            }

            var label = ConsumeIf(Colon)    // ? :
                ? ReadSymbol(SymbolFlags.Label).Name
                : null;

            return new ProtocalMessage(name.Name, label, flags);
        }

        #endregion


        #region Modules

        public ModuleSyntax ReadModule(Symbol name)
        {

            var block = ReadBlock();

            return new ModuleSyntax(name, block.Statements);
        }

        #endregion

        #region Class / Implementation

        // Curve implemention for Bezier {

        public ImplementationDeclarationSyntax ReadImplementation(Symbol name)
        {
            Consume(Implementation);  // ! implementation  

            Symbol protocal = null;
            Symbol type;

            if (ConsumeIf(For)) // ? for
            {
                protocal = name;
                type = ReadSymbol(SymbolFlags.Type);
            }
            else
            {
                type = name;
            }

            Consume(BraceOpen); // ! {

            EnterMode(Mode.Block);

            while (!IsEof && !IsKind(BraceClose))
            {
                members.Add(ReadImplMember());
            }

            LeaveMode(Mode.Block);

            Consume(BraceClose); // ! }

            return new ImplementationDeclarationSyntax(protocal, type, members.Extract());
        }

        private SyntaxNode ReadImplMember()
        {
            switch (Current.Kind)
            {
                case Let            : return ReadLet(Let);
                case BracketOpen    : return ReadIndexerDeclaration();    // [name: String] -> Point { 
                case To             : return ReadConverter();             // to Type
                case From           : return ReadInitializer();           // from pattern                                

                case Op             : 
                case Identifier     : return ReadFunctionDeclaration(FunctionFlags.Instance);   // function         |  * | + | ..
            }

            throw new UnexpectedTokenException("Unexpected token reading member", Current);
        }

        #endregion

        #region Symbols

        // A, B type { }
        // A type { 
        // A type : B {

        // }

        public Symbol ReadDollarSymbol()
        {
            reader.Consume(Dollar); // !$

            var number = reader.Consume(Number);

            return Symbol.Variable("$" + number.Text);
        }

        /*
        Point
        * Point
        [ ] Point
        [ ] Point<T>
        [ ] geometry::Point<Number>
        (A, B) -> C                     | Function<A, B, C>
        A | B                           | Variant<A, B, C>
        A & B                           | Intersection<A, B>
        A?                              | Optional<A>
        */

        // Symbol...

        private Symbol ReadSymbol(SymbolFlags flags = SymbolFlags.Type)
        {
            if (flags == SymbolFlags.Type && ConsumeIf(ParenthesisOpen))
            {
                var args = new List<Symbol>();

                do
                {
                    args.Add(ReadSymbol());
                }
                while (ConsumeIf(Comma));

                Consume(ParenthesisClose);

                bool wasFunction;

                if ((wasFunction = ConsumeIf(ReturnArrow)))
                {
                    args.Add(ReadSymbol());
                }

                return new Symbol(wasFunction ? "Function" : "Tuple", flags, args.ToArray());
            }


            if ((ConsumeIf(BracketOpen))) // [
            {
                Consume(BracketClose); // ]

                return new Symbol("List", flags, arguments: ReadSymbol(flags));
            }

            if (ConsumeIf("*"))
            {
                return new Symbol("Channel", flags, arguments: ReadSymbol());
            }

            var name = reader.Consume(); // Identifer | This | Operator

            string domain = null;

            if (IsKind(Backtick))
            {
                var sb = new StringBuilder();

                sb.Append(name);

                while (ConsumeIf(Backtick))
                {
                    name = reader.Consume();
                    
                    sb.Append(name);
                }

                name = new Token(name.Kind, name.Start, sb.ToString(), name.Trailing);
            }

            if (ConsumeIf(ColonColon))
            {
                domain = name;

                name = reader.Consume(Identifier);
            }

            Symbol[] arguments;

            if (ConsumeIf(TagOpen)) // <
            {
                var list = new List<Symbol>();

                do
                {
                    list.Add(ReadSymbol());
                }
                while (ConsumeIf(Comma));

                Consume(TagClose); // >                

                arguments = list.ToArray();
            }
            else
            {
                arguments = Array.Empty<Symbol>();
            }

            var result = new Symbol(domain, name, flags, arguments);

            // Variant      :  A | B 
            // Intersection : A & B

            if (flags == SymbolFlags.Type && IsKind(Bar))
            {
                var list = new List<Symbol>();

                list.Add(result);

                while (ConsumeIf(Bar))
                {
                    list.Add(ReadSymbol());
                }

                return new Symbol("Variant", flags, list.ToArray());
            }
            else if (flags == SymbolFlags.Type && reader.Current == "&")
            {
                var list = new List<Symbol>();

                list.Add(result);

                while (ConsumeIf("&"))
                {
                    list.Add(ReadSymbol());
                }

                return new Symbol("Intersection", flags, list.ToArray());
            }

            else if (flags == SymbolFlags.Type && IsKind(ReturnArrow))
            {
                var list = new List<Symbol>();

                list.Add(result);

                Consume(ReturnArrow);

                list.Add(ReadSymbol());

                return new Symbol("Function", flags, list.ToArray());
            }

            // Optional ?
            if (name.Trailing == null && ConsumeIf("?")) // ? 
            {
                return new Symbol("Optional", flags, arguments: result);
            }

            return result;
        }

        #endregion

        #region Literals

        // { a: 1, b: 2 }
        // { a, b }
        public ObjectInitializerSyntax ReadNewObject(Symbol type)
        {
            var members = new List<ObjectPropertySyntax>();

            // EnterMode(Mode.Block); // ! {

            Consume(BraceOpen);

            while (!IsEof && !IsKind(BraceClose))
            {
                var name = ReadSymbol(SymbolFlags.Member);

                var member = ConsumeIf(Colon)
                    ? new ObjectPropertySyntax(name, value: ReadExpression())
                    : new ObjectPropertySyntax(name);

                members.Add(member);

                ConsumeIf(Comma); // Allow trailing comma
            }

            Consume(BraceClose); // ! }

            // LeaveMode(Mode.Block);

            return new ObjectInitializerSyntax(type, members.ToArray());
        }

        // 1
        // 1_000
        // 1e100
        // 1.1
        // 1.1 Pa


        public SyntaxNode ReadNumber()
        {
            // precision & scale...

            // _ support

            var line = Current.Start.Line;

            var literal = reader.Current;

            reader.Next();

            var text = literal.Text.Contains('_') ? literal.Text.Replace("_", "") : literal.Text;
            
            if (text.Contains("e"))
            {
                var parts = text.Split('e');

                var a = double.Parse(parts[0]);
                var b = double.Parse(parts[1]);

                var result = a * Math.Pow(10, b);

                text = b > 0 ? result.ToString() : result.ToString();
            }

            // Read any immediately preceding unit prefixes, types, and expondents on the same line
            if (IsKind(Identifier) && Current.Start.Line == line) 
            {
                var unitName = Consume(Identifier); // read the unit identifier

                int pow = 1;

                if (IsKind(Superscript))
                {
                    pow = D.Superscript.Parse(reader.Consume().Text);
                }

                var num = new NumberLiteralSyntax(text);

                return new UnitLiteralSyntax(num, unitName, pow);
            }
          
            return new NumberLiteralSyntax(text);
        }

        public readonly List<SyntaxNode> children = new List<SyntaxNode>();

        public InterpolatedStringExpressionSyntax ReadInterpolatedString()
        {
            Consume(InterpolatedStringOpen); // ! $"

            EnterMode(Mode.InterpolatedString);

            while (!IsEof && !IsKind(Quote))
            {
                var expression = IsKind(BraceOpen)
                    ? ReadInterpolatedExpression()
                    : ReadInterpolatedSpan();

                children.Add(expression);
            }

            Consume(Quote);                     // ! "

            LeaveMode(Mode.InterpolatedString);

            return new InterpolatedStringExpressionSyntax(children.Extract());
        }

        public SyntaxNode ReadInterpolatedExpression()
        {
            Consume(BraceOpen); // ! {

            var expression = ReadExpression();

            Consume(BraceClose); // }

            return expression;
        }

        public StringLiteralSyntax ReadInterpolatedSpan()
        {
            var sb = new StringBuilder();

            Token token;

            while (!IsEof && !IsOneOf(Quote, BraceOpen))
            {
                token = reader.Consume();

                sb.Append(token.Text + token.Trailing);
            }

            return sb.ToString();
        }

        public StringLiteralSyntax ReadStringLiteral()
        {
            Consume(Quote);  // "

            var text = Consume(String);

            Consume(Quote); // "

            return new StringLiteralSyntax(text);
        }

        public CharacterLiteralSyntax ReadCharacterLiteral()
        {
            Consume(Apostrophe); // '

            var text = Consume(Character);

            Consume(Apostrophe); // '

            return new CharacterLiteralSyntax(text.Text[0]);
        }

        public SyntaxNode ReadNewArray()
        {
            Consume(BracketOpen); // [

            // Maybe symbol?
            if (ConsumeIf(BracketClose))
            {
                return Symbol.Type("List", ReadSymbol());
            }

            var rows = 0;
            var stride = 0;

            var elements = new List<SyntaxNode>();

            var elementKind = Kind.Object;
            var uniform = true;

            while (!IsEof && !IsKind(BracketClose))
            {
                var element = ReadPrimary();

                #region Check for uniformity

                if (uniform && element is ArrayInitializerSyntax nestedArray)
                {
                    if (rows == 0)
                    {
                        elementKind = nestedArray.Elements[0].Kind;
                        stride = nestedArray.Elements.Length;
                    }

                    if (nestedArray.Elements.Length != stride)
                    {
                        uniform = false; // jagged
                    }
                    else
                    {
                        foreach (var a in nestedArray.Elements)
                        {
                            if (a.Kind != elementKind)
                            {
                                uniform = false;

                                break;
                            }
                        }
                    }

                    rows++;
                }

                #endregion

                elements.Add(element);

                if (!ConsumeIf(Comma)) break;
            }            
            // Note: Allows trailing comma

            Consume(BracketClose); // ! ]

            // [5] Type -> new List(capacity: 5)

            /*
            if (elements.Count == 1 && elements[0].Kind == Kind.NumberLiteral && IsKind(Identifier) && char.IsUpper(reader.Current.Text[0]))
            {
                var name = Symbol.Type("List", ReadSymbol(SymbolKind.Type));

                return new CallExpressionSyntax(null, name, new[] { new ArgumentSyntax(elements[0]) });
            }
            */


            int? s = null;

            if (uniform && rows > 0)
            {
                s = stride;
            }

            return new ArrayInitializerSyntax(elements.Extract(), stride: s);
        }

        // (a, b, c)
        // (a: Integer, b: String)
        public TupleExpressionSyntax ReadTuple()
        {
            Consume(ParenthesisOpen);       // ! (

            EnterMode(Mode.Parenthesis);

            return FinishReadingTuple(ReadTupleElement());
        }

        public TupleExpressionSyntax FinishReadingTuple(SyntaxNode first)
        {
            var elements = new List<SyntaxNode>();

            elements.Add(first);

            while (ConsumeIf(Comma)) // ? ,
            {
                elements.Add(ReadTupleElement());
            }

            Consume(ParenthesisClose); // ! )

            LeaveMode(Mode.Parenthesis);

            return new TupleExpressionSyntax(elements.Extract());
        }

        public SyntaxNode ReadTupleElement()
        {
            var first = ReadPrimary();

            if (ConsumeIf(Colon))
            {
                if (first is Symbol)
                {
                    var type = ReadSymbol(SymbolFlags.Type);

                    return new NamedType(first.ToString(), type);
                }
                else
                {
                    var second = Consume(Identifier);

                    return new NamedElement(first.ToString(), Symbol.Label(second));
                }
            }
          
            return first;
        }

        #endregion

        #region Matching

        public MatchExpressionSyntax ReadMatch()
        {
            Consume(Match);                     // ! match

            EnterMode(Mode.Statement);

            var expression = ReadExpression();  // ! {expression}

            // ConsumeIf(With);                 // ? with

            ConsumeIf(BraceOpen);               // ? {

            var cases = new List<MatchCaseSyntax>();

            // pattern => action
            // ...

            while (!IsEof && !IsKind(BraceClose))
            {
                var pattern = ReadPattern();

                SyntaxNode when = null;

                if (ConsumeIf(When))            // ? when
                {
                    when = ReadExpression();
                }

                var lambda = ReadLambda();

                cases.Add(new MatchCaseSyntax(pattern, when, lambda));

                ConsumeIf(Comma); // ? ,
            }

            ConsumeIf(BraceClose); // ? }

            LeaveMode(Mode.Statement);

            return new MatchExpressionSyntax(expression, cases.Extract());
        }

        #endregion

        #region Patterns

        // record  : { a, b, c }
        // tuple   : (a, b, c)
        // type    : (alias: Type)
        // variant : A | B 
        // any     : _
        // range   : 0..10

        public SyntaxNode ReadPattern()
        {
            switch (reader.Current.Kind)
            {
                case BraceOpen       : return ReadRecordPattern();
                case Underscore      : Consume(Underscore); return new AnyPatternSyntax();

                case ParenthesisOpen:
                    var tuple = ReadTuple();

                    if (tuple.Size == 1)
                    {
                        var element = (NamedType)tuple.Elements[0];

                        return new TypePatternSyntax(element.Type, Symbol.Variable(element.Name));
                    }

                    return new TuplePatternSyntax(tuple);

                default:
                    var value = MaybeTuple();

                    if (value is RangeExpression range)
                    {
                        return new RangePatternSyntax(range.Start, range.End);
                    }

                    return new ConstantPatternSyntax(value);
            }
        }

        public TypePatternSyntax ReadRecordPattern()
        {
            throw new Exception("Not yet implemented");
        }

        #endregion

        #region Expressions

        private SyntaxNode TopExpression(int minPrecedence = 0)
        {
            var left = MaybeTuple();

            return MaybeBinary(left, minPrecedence);
        }

        // https://en.wikipedia.org/wiki/Operator-precedence_parser

        Operator op;

        // 1 + 5 ** 3 + 8

        // *=

        private SyntaxNode MaybeBinary(SyntaxNode left, int minPrecedence)
        {
            // x = a || b && c

            while (IsKind(Op) && (op = env.Operators[Infix, reader.Current]).Precedence >= minPrecedence) // ??
            {
                reader.Consume(Op);

                // *-, +=, ...
                if (ConsumeIf("="))
                {
                    var r = new BinaryExpressionSyntax(op, left, right: ReadExpression());

                    return new BinaryExpressionSyntax(Operator.Assign, left, r);
                }

                var o = op;

                var right = MaybeMemberAccess();

                while (IsKind(Op) && (op = env.Operators[Infix, reader.Current]).Precedence >= o.Precedence)
                {
                    right = MaybeBinary(right, o.Precedence);
                }

                left = new BinaryExpressionSyntax(o, left, right) {
                    Grouped = InMode(Mode.Parenthesis)
                };

                ConsumeIf(Semicolon);
            }

            // HACK: Tenerary
            if (IsKind(Question))
            {
                return ReadTernaryExpression(left);
            }
            
            return left;
        }

        public UnaryExpressionSyntax ReadUnary(Token opToken)
        {
            // maybe postfix?

            var op = env.Operators[Prefix, opToken];

            if (op == null)
            { 
                throw new Exception("Unexpected unary operator:" + opToken);
            }

            var expression = ReadExpression();

            return new UnaryExpressionSyntax(op, expression);
        }

        public TernaryExpressionSyntax ReadTernaryExpression(SyntaxNode condition)
        {
            Consume(Question); // ! ?

            var left = ReadExpression();

            Consume(Colon); // !:

            var right = ReadExpression();

            return new TernaryExpressionSyntax(condition, left, right);
        }

        #endregion

        #region Primary Expressions

        public SyntaxNode MaybeTuple()
        {
            if (InMode(Mode.Parenthesis))
            {
                var left = MaybeMemberAccess();

                if (ConsumeIf(Colon)) // :
                {
                    var right = ReadSymbol();

                    var element = new NamedType(left.ToString(), right);

                    return FinishReadingTuple(element);
                }

                if (IsKind(Comma))
                {
                    left = FinishReadingTuple(left);
                }

                return left;
            }

            return MaybeRange();
        }

        // a..z
        // A..z
        // 1..3
        // 1..100
        // i..<10
        // i..i32.max
        public SyntaxNode MaybeRange()
        {
            var left = MaybeType();

            if (ConsumeIf(DotDotDot))   // ? ...
            {
                return new RangeExpression(left, ReadExpression(), RangeFlags.Inclusive);
            }
            else if (ConsumeIf(HalfOpenRange)) // ..<
            {
                return new RangeExpression(left, ReadExpression(), RangeFlags.HalfOpen);
            }
            
            return left;
        }

        private List<Symbol> symbolList = new List<Symbol>(20);

        // {name} {type|event|record|protocal|module}
        // {name} { Object }
        public SyntaxNode MaybeType()
        {
            var left = MaybeMemberAccess();

            if (left is Symbol name)
            {
                if (IsKind(Comma) && InMode(Mode.Root))                     // ? ,
                {
                    symbolList.Add(name);

                    while (ConsumeIf(Comma))
                    {
                        symbolList.Add(ReadSymbol(SymbolFlags.Type));
                    }
                }

                switch (Current.Kind)
                {
                    case BraceOpen:
                        if (InMode(Mode.Root) || InMode(Mode.Block))
                        {
                            return ReadNewObject(name);
                        }

                        break;
                    case Colon:
                        if (InMode(Mode.Root))
                        {
                            return symbolList.Count > 0
                                ? (SyntaxNode)ReadCompoundTypeDeclaration(symbolList.Extract())
                                : ReadTypeDeclaration(name);
                        }

                        break;

                    case TokenKind.Unit: return ReadUnitDeclaration(name);

                    case Module: return ReadModule(name);

                    case Type:
                    case Event:
                    case Record:
                        return symbolList.Count > 0
                            ? (SyntaxNode)ReadCompoundTypeDeclaration(symbolList.Extract())
                            : ReadTypeDeclaration(name);  // type : hello

                    case Implementation: return ReadImplementation(name);
                    case Protocal: return ReadProtocal(name);
                    case Function: return ReadFunctionDeclaration(name);
                }
            }

            return left;
        }

        int depth = 0;
        int count = 0;

        // A |> B   A.Call
        // A.B
        public SyntaxNode MaybeMemberAccess()
        {
            var left = ReadPrimary();

            // Maybe member access
            while (IsOneOf(Dot, BracketOpen, ParenthesisOpen)                         // . | [
                || (IsKind(PipeForward) && !InMode(Mode.Arguments))) // |> 
            {
                if (IsKind(PipeForward))
                {
                    Consume(PipeForward); // |>

                    var call = ReadCall(left);

                    call.IsPiped = true;

                    left = call;
                }
                else if (IsKind(ParenthesisOpen))
                {
                    left = ReadCall(null, (Symbol)left);
                }
                else if (ConsumeIf(BracketOpen)) // ? [
                {
                    var args = ReadArguments();

                    Consume(BracketClose);

                    left = new IndexAccessExpressionSyntax(left, args);
                }
                else if (ConsumeIf(Dot))         // ? .
                {
                    var name = ReadSymbol(SymbolFlags.Member);

                    left = IsKind(ParenthesisOpen)  // ? (
                        ? (SyntaxNode)new CallExpressionSyntax(left, name, arguments: ReadArguments())
                        : new MemberAccessExpressionSyntax(left, name);
                }
            }


            return left;
        }

        public SyntaxNode ReadPrimary()
        {
            if (depth > 1)
            {
                throw new UnexpectedTokenException($"token not read. current mode {modes.Peek()}", Current);
            }

            // Operators
            if (IsKind(Op))
            {
                var op = Consume(Op);

                if (env.Operators[Prefix, op] != null)
                {
                    return ReadUnary(op);
                }

                throw new Exception("unexpected operator:" + op.ToString());
            }


            if (ConsumeIf(ParenthesisOpen))       // ? (
            {
                EnterMode(Mode.Parenthesis);

                var left = ReadExpression();

                if (ConsumeIf(ParenthesisClose))  // ? )
                {
                    LeaveMode(Mode.Parenthesis);
                }

                return left;
            }

            switch (reader.Current.Kind)
            {
                case This:
                case Identifier:
                    depth = 0;

                    var symbol = ReadSymbol(SymbolFlags.Member);

                    if (InMode(Mode.Arguments) && IsKind(LambdaOperator))  // ? =>
                    {
                        return ReadAnonymousFunctionDeclaration(symbol);
                    }

                    return symbol;

                case Number          : depth = 0; return ReadNumber();
                case BracketOpen     : depth = 0; return ReadNewArray();

                case Dollar          : depth = 0; return ReadDollarSymbol();
            }

            depth++;

            return ReadExpression();
        }

        #endregion

        #region Calls

  
        // a =>
        // (arg1, arg2, arg3)
        // (a: 1, a: 2, a: 3)

        private ArgumentSyntax[] ReadArguments()
        {
            if (!MoreArguments()) return Array.Empty<ArgumentSyntax>();

            var parenthesized = ConsumeIf(ParenthesisOpen); // ? (

            if (parenthesized && ConsumeIf(ParenthesisClose))
            {
                return Array.Empty<ArgumentSyntax>();
            }

            EnterMode(Mode.Arguments);

            ArgumentSyntax[] args;

            var arg = ReadArgument();

            if (IsKind(Comma))
            {
                var arguments = new List<ArgumentSyntax>();

                arguments.Add(arg);

                while (ConsumeIf(Comma))
                {
                    arguments.Add(ReadArgument());
                }

                args = arguments.ToArray();
            }
            else
            {
                args = new[] { arg };
            }

            if (parenthesized)
            {
                ConsumeIf(ParenthesisClose); // ? )
            }

            LeaveMode(Mode.Arguments);

            return args;
        }

        public ArgumentSyntax ReadArgument()
        {
            var first = ReadExpression();

            Symbol name;
            SyntaxNode value;

            if (ConsumeIf(Colon))
            {
                name = (Symbol)first;
                value = ReadExpression();
            }
            else
            {
                name = null;
                value = first;
            }

            return new ArgumentSyntax(name, value);
        }

        private bool MoreArguments()
        {
            switch (Current.Kind)
            {
                case EOF                :   
                case Bar                : // |
                case PipeForward        : // |>
                case ParenthesisClose   : // )
                case BracketClose       : // ]
                case Semicolon          : return false;
                default                 : return true;
            }
        }

        public CallExpressionSyntax ReadCall(SyntaxNode callee)
            => ReadCall(callee, ReadSymbol(SymbolFlags.Function));

        // Question: Scope read if arg count is fixed ?

        public CallExpressionSyntax ReadCall(SyntaxNode callee, Symbol functionName)
            => new CallExpressionSyntax(callee, functionName, ReadArguments());

        public void Dispose()
        {
            reader.Dispose();
        }

        #endregion

        #region Helpers

        public bool IsEof
            => reader.IsEof;

        public Token Current
            => reader.Current;

        Token Consume(TokenKind kind)
            => reader.Consume(kind);

        Token Consume(string text)
           => reader.Consume(text);

        bool ConsumeIf(TokenKind kind)
            => reader.ConsumeIf(kind);

        bool ConsumeIf(string text)
        {
            if (reader.Current.Text == text)
            {
                reader.Consume();

                return true;
            }

            return false;
        }

        bool IsOneOf(TokenKind a, TokenKind b)
            => reader.Current.Kind == a
            || reader.Current.Kind == b;

        bool IsOneOf(TokenKind a, TokenKind b, TokenKind c)
           =>  reader.Current.Kind == a 
            || reader.Current.Kind == b 
            || reader.Current.Kind == c;

        bool IsKind(TokenKind kind)
            => reader.Current.Kind == kind;

        #endregion
    }
}
