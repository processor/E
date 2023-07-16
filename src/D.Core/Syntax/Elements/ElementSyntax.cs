using System.Collections.Generic;

namespace E.Syntax;

public sealed class ElementSyntax(
    string? ns,
    string name,
    IReadOnlyList<ArgumentSyntax> arguments,
    ISyntaxNode[] children,
    bool selfClosed) : ISyntaxNode
{
    public string? Namespace { get; } = ns;

    public string Name { get; } = name;

    public IReadOnlyList<ArgumentSyntax> Arguments { get; } = arguments;

    // TextContent | Element | Expression
    public ISyntaxNode[] Children { get; } = children;

    public ISyntaxNode this[int index] => Children[index];

    public bool SelfClosed { get; } = selfClosed;

    // IsClosed???

    public SyntaxKind Kind => SyntaxKind.Element;
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
