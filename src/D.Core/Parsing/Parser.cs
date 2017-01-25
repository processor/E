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

        public static ISyntax Parse(string text)
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

        public IEnumerable<ISyntax> Enumerate()
        {
            while (!reader.IsEof)
            {
                yield return ReadExpression();
            }
        }

        public ISyntax Next()
        {
            if (reader.IsEof) return null;

            return ReadExpression();
        }

        private ISyntax ReadExpression()
        {
            if (reader.IsEof) return null;

            count++;

            if (count > 500) throw new Exception("recurssive call: " + reader.Current.Kind);

            switch (reader.Current.Kind)
            {
                case Null                   : reader.Consume(); return NullLiteral.Instance;
                case True                   : reader.Consume(); return BooleanLiteral.True;
                case False                  : reader.Consume(); return BooleanLiteral.False;

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

                case BracketOpen            : return ReadArrayOrMatrix();               // [
                case BraceOpen              : return ReadTypeInitializer(null);         // { 
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
                    ? ReadTypeInitializer(null)     // ? { record }
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
                domains.Add(ReadSymbol(SymbolKind.Domain));
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

        public ISyntax ReadElse()
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
                ? (ISyntax)new ElseIfStatementSyntax(condition, body, elseBranch)
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

            ISyntax generatorExpression;
            ISyntax variableExpression = null;  // variable | pattern

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
        private ObserveStatement ReadObserveStatement()
        {
            reader.Consume(); // ! on | observe

            var observable  = ReadExpression(); // TODO: handle member access

            var eventType = ReadSymbol();
            var varName = (! (IsKind(BraceOpen) | IsKind(LambdaOperator)))
                ? ReadSymbol(SymbolKind.Function).Name
                : null;

            var body = ReadBody();

            var until = IsKind(Until)
                ? ReadUntilExpression()
                : null;

            return new ObserveStatement(observable, eventType, varName, body, until);
        }

        private UntilExpression ReadUntilExpression()
        {
            Consume(Until); // ! until

            var untilObservable = ReadExpression();
            var untilEventType  = ReadSymbol();

            return new UntilExpression(untilObservable, untilEventType);
        }

        public BlockStatementSyntax ReadBlock()
        {
            Consume(BraceOpen); // ! {

            EnterMode(Mode.Block);

            var statements = new List<ISyntax>();

            while (!IsEof && !IsKind(BraceClose))
            {
                statements.Add(ReadExpression());
            }

            Consume(BraceClose); // ! }

            LeaveMode(Mode.Block);

            return new BlockStatementSyntax(statements.ToArray());
        }

        public SpreadExpression ReadSpread()
        {
            Consume(DotDotDot); // ! ...

            return new SpreadExpression(ReadPrimary());
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
        
        public ISyntax ReadLet(TokenKind kind)
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
                        l.Add(new VariableDeclarationSyntax(var.Name, k, var.IsMutable));
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

            var name = ReadSymbol(SymbolKind.LocalVariable);

            ConsumeIf(ParenthesisClose);    // ? )

            var type = ConsumeIf(Colon)     // ? :
                ? ReadSymbol()
                : null;

            var value = ConsumeIf("=")       // ? =
                ? IsKind(Function) ? ReadFunctionDeclaration(name) : ReadExpression()
                : null;

            return new VariableDeclarationSyntax(name.ToString(), type, mutable, value);

        }

        private FunctionDeclarationSyntax ReadInitializer()
        {
            var flags = FunctionFlags.Initializer;

            Consume(From); // ! from

            return ReadFunctionDeclaration(new Symbol("initializer", SymbolKind.Function), flags);
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
                   ? ReadSymbol(SymbolKind.Argument)
                   : null;

            var body = ReadBody();

            ConsumeIf(Semicolon); // ? ;

            return new FunctionDeclarationSyntax(parameters, body, returnType, flags);
        }

        private FunctionDeclarationSyntax ReadFunctionDeclaration(FunctionFlags flags = FunctionFlags.None)
        {
            var isOperator = IsKind(Op);

            if (isOperator) flags |= FunctionFlags.Operator;

            var name = isOperator ? new Symbol(reader.Consume(), SymbolKind.Function) : ReadSymbol(SymbolKind.Function);

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
                ? ReadSymbol(SymbolKind.Argument)
                : null;

            ISyntax body;

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

        private ISyntax ReadBody()
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
            var name = ReadSymbol(SymbolKind.Argument); // name

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
                ? ReadSymbol(SymbolKind.Type)     // baseType
                : null;

            var members = ReadTypeDeclarationBody();

            ConsumeIf(Semicolon); // ? ;

            return new CompoundTypeDeclarationSyntax(names, flags, baseTypes, members);
        }


        // primitives may have a single member (size)

        // Int32 primitive { size: 32 }
        public PrimitiveDeclarationSyntax ReadPrimitiveDeclaration(Symbol typeName)
        {
            Consume(Primitive); // ! primitive

            int? size = null;

            if (ConsumeIf(BraceOpen))
            {
                while (!IsKind(BraceClose))
                {
                    var name = Consume("size");

                    Consume(Colon);

                    size = int.Parse(ReadNumeric().ToString());

                    ConsumeIf(Semicolon);
                }

                Consume(BraceClose);
            }

            ConsumeIf(Semicolon); // ? ;

            return new PrimitiveDeclarationSyntax(typeName, size);

        }

        // Point type <T:Number> : Vector3 { 
        public TypeDeclarationSyntax ReadTypeDeclaration(Symbol typeName)
        {
            ConsumeIf(Type);

            var flags = ConsumeIf("event") ? TypeFlags.Event  : TypeFlags.None;

            if (ConsumeIf(Record)) flags |= TypeFlags.Record;

            // <T: Number>
            var genericParameters = ReadGenericParameters();

            var baseType = ConsumeIf(Colon)    // ? :
                ? ReadSymbol(SymbolKind.Type)  // baseType
                : null;

            var members = ReadTypeDeclarationBody();

            ConsumeIf(Semicolon); // ? ;

            return new TypeDeclarationSyntax(typeName, genericParameters, baseType, members, flags);
        }

        private PropertyDeclarationSyntax[] ReadTypeDeclarationBody()
        {
            if (ConsumeIf(BraceOpen))  // ? {
            {
                var members = ReadMembers();

                Consume(BraceClose);  // ! }

                return members.ToArray();
            }
            
            return Array.Empty<PropertyDeclarationSyntax>();
        }

        private readonly List<Symbol> names = new List<Symbol>();

        private List<PropertyDeclarationSyntax> ReadMembers()
        {
            var members = new List<PropertyDeclarationSyntax>();

            while (!IsEof && !IsKind(BraceClose))
            {
                // mutable name: Type | Type,

                bool mutable = ConsumeIf(Mutable);

                names.Add(ReadSymbol(SymbolKind.Property));
           
                while (ConsumeIf(Comma))
                {
                    names.Add(ReadSymbol(SymbolKind.Property));
                }
           
                Consume(Colon); // ! :

                var type = ReadSymbol(SymbolKind.Type);

                ConsumeIf(Semicolon); // ? ;

                foreach(var name in names.Extract())
                {
                    members.Add(new PropertyDeclarationSyntax(name, type, mutable));
                }
            }

            return members;
        }

        private readonly List<ISyntax> members = new List<ISyntax>();
        private readonly List<FunctionDeclarationSyntax> methods = new List<FunctionDeclarationSyntax>();

        // Account protocal { }

        public ProtocalDeclarationSyntax ReadProtocal(Symbol name)
        {
            Consume(Protocal);      // ! protocal

            Consume(BraceOpen);     // ! {

            var channelProtocal = reader.Current.Text == "*"
                ? ReadChannelProtocal()
                : null;

            while (!IsEof && !IsKind(BraceClose))
            {
                methods.Add(ReadProtocalMember());
            }
            
            Consume(BraceClose);  // ! }
            ConsumeIf(Semicolon); // ? ;

            return new ProtocalDeclarationSyntax(name, channelProtocal, methods.Extract());
        }

        public List<IMessageDeclaration> ReadChannelProtocal()
        {
            var messages = new List<IMessageDeclaration>();

            while (ConsumeIf("*"))  // ! ∙
            {
                ConsumeIf(Bar);         // ? |  // Optional leading bar in a oneof set

                var message = ReadMessageDeclaration();

                if (message.Fallthrough)
                {
                    var flags = MessageFlags.None;

                    var options = new List<ProtocalMessage>();

                    options.Add(message);

                    while (message.Fallthrough && !IsKind(Repeats) && reader.Current.Text != "*")
                    {
                        options.Add(ReadMessageDeclaration());
                    }

                    if (ConsumeIf(Repeats))
                    {
                        flags |= MessageFlags.Repeats;

                        if (ConsumeIf(Colon))
                        {
                            var label = ReadSymbol(SymbolKind.Label);
                        }
                    }

                    if (ConsumeIf(End)) // ? ∎
                    {
                        flags |= MessageFlags.End;
                    }

                    var oneof = new MessageChoice(options, flags);

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
            var name = ReadSymbol(SymbolKind.Label);
            
            ConsumeIf(Function);                                // ƒ

            var flags = FunctionFlags.Abstract;

            ParameterSyntax[] parameters;

            if (ConsumeIf(ParenthesisOpen))                     // ! (
            {
                parameters = ReadParameters().ToArray();

                Consume(ParenthesisClose);                      // ! )
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
        public ProtocalMessage ReadMessageDeclaration()
        {
            var flags = ConsumeIf(Question)   // ? ?
                ? MessageFlags.Optional 
                : MessageFlags.None;

            var name = ReadSymbol(SymbolKind.Function);

            if (ConsumeIf(End)) // ? ∎
            {
                flags |= MessageFlags.End;
            }

            if (ConsumeIf(Bar)) // ? |
            {
                flags |= MessageFlags.Fallthrough;
            }

            var label = ConsumeIf(Colon)    // ? :
                ? ReadSymbol(SymbolKind.Label).Name
                : null;

            return new ProtocalMessage(name.Name, label, flags);
        }

        #endregion

        #region Class / Implementation

        // Curve implemention for Bezier {

        public ImplementationDeclarationSyntax ReadImplementation(Symbol name)
        {
            Consume(Implementation);            // !implementation  

            Symbol protocal = null;
            Symbol type;

            if (ConsumeIf(For)) // ? for
            {
                protocal = name;
                type = ReadSymbol(SymbolKind.Type);
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

        private ISyntax ReadImplMember()
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

            return Symbol.Local("$" + number.Text);
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

        private Symbol ReadSymbol(SymbolKind kind = SymbolKind.Type)
        {
            if (kind == SymbolKind.Type && ConsumeIf(ParenthesisOpen))
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

                return new Symbol(wasFunction ? "Function" : "Tuple", kind, args.ToArray());
            }


            if ((ConsumeIf(BracketOpen))) // [
            {
                Consume(BracketClose); // ]

                return new Symbol("List", kind, arguments: ReadSymbol(kind));
            }

            if (ConsumeIf("*"))
            {
                return new Symbol("Channel", kind, arguments: ReadSymbol());
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

            var result = new Symbol(domain, name, kind, arguments);

            // Variant      :  A | B 
            // Intersection : A & B

            if (kind == SymbolKind.Type && IsKind(Bar))
            {
                var list = new List<Symbol>();

                list.Add(result);

                while (ConsumeIf(Bar))
                {
                    list.Add(ReadSymbol());
                }

                return new Symbol("Variant", kind, list.ToArray());
            }
            else if (kind == SymbolKind.Type && reader.Current == "&")
            {
                var list = new List<Symbol>();

                list.Add(result);

                while (ConsumeIf("&"))
                {
                    list.Add(ReadSymbol());
                }

                return new Symbol("Intersection", kind, list.ToArray());
            }

            else if (kind == SymbolKind.Type && IsKind(ReturnArrow))
            {
                var list = new List<Symbol>();

                list.Add(result);

                Consume(ReturnArrow);

                list.Add(ReadSymbol());

                return new Symbol("Function", kind, list.ToArray());
            }

            // Optional ?
            if (name.Trailing == null && ConsumeIf("?")) // ? 
            {
                return new Symbol("Optional", kind, arguments: result);
            }

            return result;
        }

        #endregion

        #region Literals

        // { a: 1, b: 2 }
        // { a, b }
        public TypeInitializerSyntax ReadTypeInitializer(Symbol type)
        {
            var members = new List<RecordMemberSyntax>();

            // EnterMode(Mode.Block); // ! {

            Consume(BraceOpen);

            while (!IsEof && !IsKind(BraceClose))
            {
                var name = ReadSymbol(SymbolKind.Member);

                var member = ConsumeIf(Colon)
                    ? new RecordMemberSyntax(name, value: ReadExpression())
                    : new RecordMemberSyntax(name);

                members.Add(member);

                ConsumeIf(Comma); // Allow trailing comma
            }

            Consume(BraceClose); // ! }

            // LeaveMode(Mode.Block);

            return new TypeInitializerSyntax(type, members.ToArray());
        }
        
        // 1
        // 1_000
        // 1e100
        // 1.1
        public ISyntax ReadNumeric()
        {
            // precision & scale...

            // _ support
            var wholeText = ReadNumberText();

            // right hand side of the decimal
            var mantissaText = ConsumeIf(DecimalPoint)
                ? ReadNumberText()
                : null;

            INumber number;

            if (mantissaText == null)
            {
                if (wholeText.Contains("e"))
                {
                    var parts = wholeText.Split('e');

                    var a = double.Parse(parts[0]);
                    var b = double.Parse(parts[1]);

                    var result = a * Math.Pow(10, b);

                    number = b > 0 ? (INumber)new Integer((long)result) : new Float(result); 
                }
                else
                {
                    long i64;

                    if (!long.TryParse(wholeText, out i64))
                    {
                        throw new Exception(wholeText);
                    }

                    number = long.TryParse(wholeText, out i64)
                        ? (INumber) new Integer(i64)
                        : new HugeInteger(BigInteger.Parse(wholeText));
                }
            }
            else
            {
                number = new Float(double.Parse(wholeText + "." + mantissaText));
            }

            // Read any immediately preceding unit prefixes, types, and expondents
            // do we need to scope to line?
            Unit<int> unit;

            // move units to environment

            if (IsKind(Identifier) && Unit<int>.TryParse(reader.Current.Text, out unit)) 
            {
               
                Consume(Identifier); // read the unit identifier

                int pow = 1;

                if (IsKind(Superscript))
                {
                    pow = D.Superscript.Parse(reader.Consume().Text);
                }
                
                return new UnitLiteral(unit.With(number.Real, power: pow));
            }

            // return number;

            return new NumberLiteral(number.ToString());
        }

        private string ReadNumberText()
        {
            var text = Consume(Number);

            if (IsKind(Underscore))
            {
                var sb = new StringBuilder();

                sb.Append(text);

                while (ConsumeIf(Underscore))
                {
                    if (IsKind(Underscore)) continue; // Read subsequent underscores

                    sb.Append(reader.Consume(Number));
                }

                return sb.ToString();
            }

            return text;
        }

        public readonly List<ISyntax> children = new List<ISyntax>();

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

        public ISyntax ReadInterpolatedExpression()
        {
            Consume(BraceOpen); // ! {

            var expression = ReadExpression();

            Consume(BraceClose); // }

            return expression;
        }

        public StringLiteral ReadInterpolatedSpan()
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

        public StringLiteral ReadStringLiteral()
        {
            Consume(Quote); // "

            var text = Consume(String);

            Consume(Quote); // "

            return new StringLiteral(text);
        }

        public CharacterLiteral ReadCharacterLiteral()
        {
            Consume(Apostrophe); // '

            var text = Consume(Character);

            Consume(Apostrophe); // '

            return new CharacterLiteral(text.Text[0]);
        }

        public ISyntax ReadArrayOrMatrix()
        {
            Consume(BracketOpen); // [

            // Maybe symbol?
            if (ConsumeIf(BracketClose))
            {
                return Symbol.Type("List", ReadSymbol());
            }

            var rows = 0;
            var stride = 0;

            var elements = new List<ISyntax>();
            var type = Kind.None;
            var uniform = true;

            while (!IsEof && !IsKind(BracketClose))
            {
                var el = ReadPrimary();

                #region Check for uniformity

                if (uniform && el is ArrayLiteralSyntax)
                {
                    var array = (ArrayLiteralSyntax)el;

                    if (rows == 0)
                    {
                        type = array.Elements[0].Kind;
                        stride = array.Elements.Count;
                    }

                    if (uniform)
                    {
                        if (array.Elements.Count != stride)
                        {
                            uniform = false; // jagged
                        }
                        else
                        {
                            foreach (var a in array.Elements)
                            {
                                if (a.Kind != type)
                                {
                                    uniform = false;

                                    break;
                                }
                            }
                        }
                    }

                    rows++;
                }

                #endregion

                elements.Add(el);

                ConsumeIf(Comma);
            }

            Consume(BracketClose); // ]

            if (uniform && rows > 0)
            {
                // Flatten
                var i = 0;

                var items = new ISyntax[elements.Count * stride];

                foreach (var row in elements)
                {
                    foreach (var column in ((ArrayLiteralSyntax)row).Elements)
                    {
                        items[i] = column;

                        i++;
                    }
                }

                return new MatrixLiteralSyntax(items, stride);
            }

            // [5] Type -> new List(capacity: 5)

            if (elements.Count == 1 && elements[0] is NumberLiteral && IsKind(Identifier) && char.IsUpper(reader.Current.Text[0]))
            {
                var name = Symbol.Type("List", ReadSymbol(SymbolKind.Type));

                return new CallExpressionSyntax(null, name, new[] { new ArgumentSyntax(elements[0]) });
            }

            return new ArrayLiteralSyntax(elements);
        }

        // (a, b, c)
        // (a: Integer, b: String)
        public TupleExpression ReadTuple()
        {
            Consume(ParenthesisOpen);       // ! (

            EnterMode(Mode.Parenthesis);

            return FinishReadingTuple(ReadTupleElement());
        }

        private readonly List<ISyntax> elements = new List<ISyntax>();

        public TupleExpression FinishReadingTuple(ISyntax first)
        {
            elements.Add(first);

            while (ConsumeIf(Comma)) // ? ,
            {
                elements.Add(ReadTupleElement());
            }

            Consume(ParenthesisClose); // ! )

            LeaveMode(Mode.Parenthesis);

            return new TupleExpression(elements.Extract());
        }

        public ISyntax ReadTupleElement()
        {
            var first = ReadPrimary();

            if (ConsumeIf(Colon))
            {
                if (first is Symbol)
                {
                    var type = ReadSymbol(SymbolKind.Type);

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

                ISyntax when = null;

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

            return new MatchExpressionSyntax(expression, cases);
        }

        #endregion

        #region Patterns

        // record  : { a, b, c }
        // tuple   : (a, b, c)
        // type    : (alias: Type)
        // variant : A | B 
        // any     : _
        // range   : 0..10

        public ISyntax ReadPattern()
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

                        return new TypePatternSyntax(element.Type, Symbol.Local(element.Name));
                    }

                    return new TuplePattern(tuple);

              

                default:
                    var value = MaybeTuple();
                    
                    if (value is RangeExpression)
                    {
                        var range = (RangeExpression)value;

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

        private ISyntax TopExpression(int minPrecedence = 0)
        {
            var left = MaybeTuple();

            return MaybeBinary(left, minPrecedence);
        }

        // https://en.wikipedia.org/wiki/Operator-precedence_parser

        Operator op;

        // 1 + 5 ** 3 + 8

        // *=

        private ISyntax MaybeBinary(ISyntax left, int minPrecedence)
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

        public TernaryExpressionSyntax ReadTernaryExpression(ISyntax condition)
        {
            Consume(Question); // ! ?

            var left = ReadExpression();

            Consume(Colon); // !:

            var right = ReadExpression();

            return new TernaryExpressionSyntax(condition, left, right);
        }

        #endregion

        #region Primary Expressions

        public ISyntax MaybeTuple()
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

        // a...z
        // A...z
        // 1...3
        // 1...100
        // i..<10
        // i..i32.max
        public ISyntax MaybeRange()
        {
            var left = MaybeType();

            switch (reader.Current.Kind)
            {
                case DotDotDot:                         // ? ...
                    Consume(DotDotDot);

                    return new RangeExpression(left, ReadExpression());

                case HalfOpenRange:                     // ? ..<
                    Consume(HalfOpenRange);

                    return new HalfOpenRangeExpression(left, ReadExpression());
            }


            return left;
        }

        private List<Symbol> symbolList = new List<Symbol>(20);

        // {Symbol} (type|event|record|protocal)
        public ISyntax MaybeType()
        {
            var left = MaybeMemberAccess();

            if (left is Symbol)
            {
                var name = (Symbol)left;

                if (IsKind(Comma) && InMode(Mode.Root))                     // ? ,
                {
                    symbolList.Add(name);

                    while (ConsumeIf(Comma))
                    {
                        symbolList.Add(ReadSymbol(SymbolKind.Type));
                    }                 
                }
              
                switch (Current.Kind)
                {
                    case BraceOpen:
                        if (InMode(Mode.Root) || InMode(Mode.Block))
                        {
                            return ReadTypeInitializer(name);
                        }

                        break;
                    case Colon:
                        if (InMode(Mode.Root))
                        {
                            return symbolList.Count > 0
                                ? (ISyntax)ReadCompoundTypeDeclaration(symbolList.Extract())
                                : ReadTypeDeclaration(name);
                        }

                        break;
                    case Primitive: return ReadPrimitiveDeclaration(name); // Int32 primitive
                    case Type:
                    case Event:
                    case Record:
                        return symbolList.Count > 0
                            ? (ISyntax)ReadCompoundTypeDeclaration(symbolList.Extract())
                            : ReadTypeDeclaration(name);  // type : hello

                    case Implementation : return ReadImplementation(name);

                    case Protocal       : return ReadProtocal(name);
                    case Function       : return ReadFunctionDeclaration(name);
                }
            }

            return left;
        }

        int depth = 0;
        int count = 0;

        public ISyntax MaybeMemberAccess()
        {
            var left = ReadPrimary();

            // Maybe member access
            while (IsOneOf(Dot, BracketOpen, ParenthesisOpen)                         // . | [
                || (IsKind(PipeForward) && !InMode(Mode.Arguments))) // |> 
            {
                if (IsKind(PipeForward))
                {
                    left = ReadPipe(left);
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
                    var name = ReadSymbol(SymbolKind.Member);

                    left = IsKind(ParenthesisOpen)  // ? (
                        ? (ISyntax)new CallExpressionSyntax(left, name, arguments: ReadArguments())
                        : new MemberAccessExpressionSyntax(left, name);
                }
            }


            return left;
        }

        public ISyntax ReadPrimary()
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

                    var symbol = ReadSymbol(SymbolKind.Member);

                    if (InMode(Mode.Arguments) && IsKind(LambdaOperator))  // ? =>
                    {
                        return ReadAnonymousFunctionDeclaration(symbol);
                    }

                    return symbol;

                case Number          : depth = 0; return ReadNumeric();
                case BracketOpen     : depth = 0; return ReadArrayOrMatrix();

                case Dollar          : depth = 0; return ReadDollarSymbol();
            }

            depth++;

            return ReadExpression();
        }

        #endregion

        #region Pipes & Calls

        private PipeStatementSyntax ReadPipe(ISyntax callee)
        {
            Consume(PipeForward); // |>

            ISyntax body;

            switch (reader.Current.Kind)
            {
                case Match : body = ReadMatch();    break; // match
                default    : body = ReadCall(null); break; // otherwise call
            }

            return new PipeStatementSyntax(callee, body);
        }

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
            ISyntax value;

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

        public CallExpressionSyntax ReadCall(ISyntax callee)
        {
            return ReadCall(callee, ReadSymbol(SymbolKind.Function));
        }

        // Question: Scope read if arg count is fixed ?

        public CallExpressionSyntax ReadCall(ISyntax callee, Symbol functionName)
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
