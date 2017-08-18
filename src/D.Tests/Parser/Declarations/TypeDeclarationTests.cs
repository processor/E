using System.Linq;

using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class TypeDeclarationTests : TestBase
    { 
        [Theory]
        [InlineData("Vehicle struct             { name: String     }", TypeFlags.Struct)]
        [InlineData("Vehicle record             { name: String     }", TypeFlags.Record)]
        [InlineData("Vehicle`Crash event        { vehicle: Vehicle }", TypeFlags.Event)]
        [InlineData("Vehicle`Crash event record { vehicle: Vehicle }", TypeFlags.Event | TypeFlags.Record)]
        public void Subtypes(string text, TypeFlags flags)
        {
            var type = Parse<TypeDeclarationSyntax>(text);

            Assert.Equal(flags, type.Flags);
        }

        [Theory]
        [InlineData("SVG struct : Graphic")]
        [InlineData("SVG : Graphic")]         // Optional type
        [InlineData("SVG : Graphic;")]        // Optional semicolon
        public void ExtendsTest(string text)
        {
            var type = Parse<TypeDeclarationSyntax>(text);

            Assert.Equal("SVG", type.Name.ToString());
            Assert.Equal("Graphic", type.BaseType.ToString());
        }

        [Fact]
        public void ReallyLongTerm()
        {
            var type = Parse<TypeDeclarationSyntax>("Type `III `Autoimmune `Polyglandular `Syndrome : Syndrome");

            Assert.Equal("TypeIIIAutoimmunePolyglandularSyndrome", type.Name);
            Assert.Equal("Syndrome", type.BaseType.ToString());
        }

        [Fact]
        public void ShorthandFields()
        {
            var a = Parse<TypeDeclarationSyntax>(@"
Point struct {
  x, y, z: T
}
");
            Assert.Equal(1, a.Members.Length);

            var members = ((CompoundPropertyDeclaration)a.Members[0]).Members;

            Assert.Equal("x", members[0].Name);
            Assert.Equal("y", members[1].Name);
            Assert.Equal("z", members[2].Name);

            Assert.Equal("T", members[0].Type);
            Assert.Equal("T", members[1].Type);
            Assert.Equal("T", members[2].Type);
        }

        [Fact]
        public void ConstrainedGeneric()
        {
            var type = Parse<TypeDeclarationSyntax>(@"
Size<T:Number> struct {
  width  : Number
  height : Number
}
");
            Assert.Equal(2, type.Members.Length);

            var members = type.Members.OfType<PropertyDeclarationSyntax>().ToArray();

            Assert.Equal("T", type.Name.Arguments[0].Name);
            Assert.Equal("width", members[0].Name);
            Assert.Equal("height", members[1].Name);

            Assert.Equal("Number", members[0].Type);
            Assert.Equal("Number", members[1].Type);
        }

        [Fact]
        public void Multidefination()
        {
            var type = Parse<CompoundTypeDeclarationSyntax>(@"
A, B, C : D type {
  id : Identity
}");

            Assert.Equal("A", type.Names[0].ToString());
            Assert.Equal("B", type.Names[1].ToString());
            Assert.Equal("C", type.Names[2].ToString());
            Assert.Equal("D", type.BaseType);
        }

        [Fact]
        public void TypeDefination()
        {
            var type = Parse<TypeDeclarationSyntax>(@"
Graphic class {
  text : String
  id   : Identity
}");

            var members = type.Members.OfType<PropertyDeclarationSyntax>().ToArray();

            Assert.Equal("Graphic",  type.Name.ToString());
            Assert.Equal("text",     members[0].Name);
            Assert.Equal("String",   members[0].Type.Name);
            Assert.Equal("id",       members[1].Name);
            Assert.Equal("Identity", members[1].Type.ToString());

            Assert.Equal(0, type.GenericParameters.Length);
        }

        [Fact]
        public void GenericParams()
        {
            var declaration = Parse<TypeDeclarationSyntax>(@"
Point struct <T: Number> : Vector3<T> {
  x: T
  y: T
  z: T
};");
            Assert.Equal("Point", declaration.Name);
            Assert.Equal("Vector3", declaration.BaseType.Name);
            Assert.Equal("T", declaration.BaseType.Arguments[0].Name);

            Assert.Equal(1, declaration.GenericParameters.Length);

            Assert.Equal("T",      declaration.GenericParameters[0].Name);
            Assert.Equal("Number", declaration.GenericParameters[0].Type);

            Assert.Equal(3, declaration.Members.Length);
        }

        /*

        [Fact]
        public void Generic()
        {
            var declaration = Parse<TypeDeclaration>(@"
T record {
  f: ('a, 'a, 'a)
};");

        }
        */

        [Fact]
        public void Q()
        {
            var declaration = Parse<TypeDeclarationSyntax>(@"
T record {
  a: Set<String>
  b: Function<A, B>
  c: * Integer
  d: A | B
  e: (A, B, C)
  f: (A, B, C) -> D
  g: [ physics::Momentum<T> ]
  h: Integer?
  i : [ Collision `Course? ]
};");


            var members = declaration.Members.Cast<PropertyDeclarationSyntax>().ToArray();

            Assert.Equal("T",                               declaration.Name);

            Assert.Equal("Set",                             members[0].Type.Name);
            Assert.Equal("String",                          members[0].Type.Arguments[0].Name);

            Assert.Equal("Function",                        members[1].Type.Name);
            Assert.Equal("A",                               members[1].Type.Arguments[0].Name);
            Assert.Equal("B",                               members[1].Type.Arguments[1].Name);

            Assert.Equal("Channel",                         members[2].Type.Name);
            Assert.Equal("Integer",                         members[2].Type.Arguments[0].Name);

            Assert.Equal("Variant<A,B>",                    members[3].Type.ToString());
            Assert.Equal("Tuple<A,B,C>",                    members[4].Type.ToString());
            Assert.Equal("Function<A,B,C,D>",               members[5].Type.ToString());
            Assert.Equal("List<physics::Momentum<T>>",      members[6].Type.ToString());
            Assert.Equal("Optional<Integer>",               members[7].Type.ToString());
            Assert.Equal("List<Optional<CollisionCourse>>", members[8].Type.ToString());
        }

        [Fact]
        public void DeclaratedIndexerWithLambdaBody()
        {
            // The indexer syntax is ambigious and limits our 'map' syntax...

            // swift uses subscript
            // c# prefixes with this

            // operators should only be able to chain one line...

            var type = Parse<TypeDeclarationSyntax>(@"
Vector3 struct { 
  x, y, z: Number

  from (value: T) => Vector3(x: value, y: value, z: value); // ambigious without ;

  [ index: i64 ] => match index { 
    0 => x
    1 => y
    2 => z
  };
}");

            
        }


        [Fact]
        public void CommentsAndCasing()
        {
            var type = Parse<TypeDeclarationSyntax>(@"
Arc struct { 
  x            : Number            // circle center x
  y            : Number            // circle center y
  x `Radius    : Number            // circle radius x
  y `Radius    : Number            // circle radius y
  start `Angle : Number<Angle>
  end   `Angle : Number<Angle>
  clockwise    : Boolean
};");

            var members = type.Members.OfType<PropertyDeclarationSyntax>().ToArray();

            Assert.Equal(7, members.Length);
        }

        [Fact]
        public void Complicated()
        {
            var type = Parse<TypeDeclarationSyntax>(@"
Account record {
   mutable balance :   Decimal
   owner           :   Entity
   provider        :   Organization
   transactions    : [ Transaction ]
   currencyCode    :   List<Character>
};");

            // var members = type.Members;

            var members = type.Members.OfType<PropertyDeclarationSyntax>().ToArray();

            
            Assert.Equal("Account", type.Name);
            Assert.True(type.IsRecord);
            Assert.True(members[0].Flags.HasFlag(ObjectFlags.Mutable));
            Assert.Equal("balance", members[0].Name);
            Assert.Equal("Decimal", (members[0] as PropertyDeclarationSyntax).Type.ToString());

            Assert.Equal("owner",   members[1].Name);
            Assert.Equal("Entity",  members[1].Type);

            Assert.Equal("Organization", members[2].Type);
                                         
            Assert.Equal("List",         members[3].Type.Name);
            Assert.Equal("Transaction",  members[3].Type.Arguments[0].Name);
                                         
            Assert.Equal("List",         members[4].Type.Name);
            Assert.Equal("Character",    members[4].Type.Arguments[0].Name);
        }
    }
}