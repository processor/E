namespace D.Syntax
{
    public class ElementSyntax : ISyntaxNode
    {
        public ElementSyntax(
            string module,
            string name, 
            ArgumentSyntax[] arguments,
            ISyntaxNode[] children,
            bool selfClosed)
        {
            Namespace = module;
            Name      = name;
            Arguments = arguments;
            Children  = children;
            SelfClosed = selfClosed;
        }

        public string Namespace { get; }

        public string Name { get; }
        
        public ArgumentSyntax[] Arguments { get; }

        // TextContent | Element | Expression
        public ISyntaxNode[] Children { get; }
        
        public ISyntaxNode this[int index] => Children[index];

        public bool SelfClosed { get; }

        public SyntaxKind Kind => SyntaxKind.Element;
    }
}

/*

<Gallery>
  {#for piece in pieces}
    <Piece />
  {/for}

  {#with piece}
  <h1>HELLO</h1>
  {/with}
  
</Gallery>

for assets {
  <Asset />
}

<Image (source: "/images/a.gif", width: 800, height: 600) />
<Video (source: "/videos/video.mp4") />

<Document>
  <Brains />
  
  <Main>
    <Form (action: $"/repositories/{id}/update")>
        <Field (name: "name", required: true) />
    </Form>
  </Main>
</Document>

<Carbon::Gallery />
<Carbon::Field />

from Carbon import Form, Field

<Form>
  <Field (name: name, required: true) />
</Form>

*/
