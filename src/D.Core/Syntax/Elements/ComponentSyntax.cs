namespace D.Core
{
    internal class ComponentSyntax
    {
        public ComponentSyntax(string name)
        {
            Name = name;
        }

        public string Name { get; }

        // Functions

        // Styles

        // ...
    }
}

// Look at Mint for Style Syntax
// https://www.mint-lang.com/

/*
 
/components/Button

text: String

render ƒ () {
  <button>{text}</button>
}

style base {
  font     : ("Helvetica", 20px, 24px)
  padding  : (20px, 20px, 20px, 20px)
  position : Absolute
  right    : 0px

  & :after { 
    content: "oranges"
  }
}


Heading component {
  name: String
  level: Int32
  color = "red"

  background => level == 1 ? Color.red : Color.blue

  render ƒ() {
   
  }
}

<Template>
  <div>
    <h1>{name}</h1>
  </div>

  <Button (onClick: ...) />
</Template>

<Style (scoped: true)>
div {
  h1 { 
    background: $color;
    color: red;
    transition: 100ms;
  }
}
</Style>

*/
